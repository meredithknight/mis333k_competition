using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using fa18Team22.Models;
using fa18Team22.DAL;
using Microsoft.EntityFrameworkCore;
using fa18Team22.Controllers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fa18Team22.Controllers
{
    public class RecommendBooksController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        private readonly AppDbContext _context;
        public RecommendBooksController(AppDbContext context)
        {
            _context = context;
        }

        public ActionResult Recommend(int? id)
        {
            Order order = _context.Orders.Include(r => r.Customer).Include(r => r.OrderDetails).ThenInclude(OrderDetails => OrderDetails.Book).ThenInclude(Book => Book.Genre).FirstOrDefault(r => r.OrderID == id);
            List<Book> listofrecbooks = RecommendedBooks(order);

            AppUser customer = _context.Users.FirstOrDefault(r => r.UserName == order.Customer.UserName);


            //send email
            Book book1 = listofrecbooks[0];
            Book book2 = listofrecbooks[1];
            Book book3 = listofrecbooks[2];
            String emailsubject = "Team 22: Recommended Books";
            String emailbody = "Thank you for your order with Bevo Books. Here are three other books you should check out!" + "\n" + book1.Title + " by " + book1.Author + "\n" + book2.Title + " by " + book2.Author + "\n" + book3.Title + " by " + book3.Author;
            AccountController.SendEmail(customer.Email, customer.FirstName, emailbody, emailsubject);


            return View("Recommend", listofrecbooks);
        }


        public List<Book> RecommendedBooks(Order order)
        {
            List<Book> bookstorecommend = new List<Book>();
            OrderDetail orderDetails = order.OrderDetails.FirstOrDefault();
            String userId = User.Identity.Name;
            List<Book> booksboughtbyuser = new List<Book>();
            AppUser currentuser = _context.Users.FirstOrDefault(u => u.UserName == userId);


            var booksboughtquery = from r in _context.OrderDetails.Include(r => r.Order).Include(r =>r.Book) select r;
            booksboughtquery = booksboughtquery.Where(r => r.Order.Customer.UserName == currentuser.UserName);
            foreach (OrderDetail orddlt in booksboughtquery)
            {
                booksboughtbyuser.Add(orddlt.Book);
            }
            Int16 skipcounterhighestrated = 0;
            Int32 querycounterhighestrated = 0;
            Int16 skipcounter = 1;
            Int16 skipcounter3 = 1;
            Boolean sameauthor = false;
            string author = orderDetails.Book.Author;
            string genre = orderDetails.Book.Genre.GenreName;
            var query = from r in _context.Books select r;
            query = query.Where(r => r.Author == author && r.Genre.GenreName == genre);
            if (query.ToList().Count() > 1)
            {
                List<Book> querylist = query.ToList();
                querycounterhighestrated = querylist.Count();
                while(querycounterhighestrated != 0)
                {
                    Book highestratedbook = querylist.OrderByDescending(r => r.AvgRating).Skip(skipcounterhighestrated).FirstOrDefault();
                    if (highestratedbook != null)
                    {
                        if (highestratedbook.Title == orderDetails.Book.Title)
                        {
                            highestratedbook = querylist.Skip(1).FirstOrDefault();
                        }
                        bookstorecommend.Add(highestratedbook);
                        if (booksboughtbyuser.Any(r => r.BookID == highestratedbook.BookID))
                        {
                            bookstorecommend.Remove(highestratedbook);
                            skipcounterhighestrated += 1;
                            querycounterhighestrated -= 0;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        skipcounterhighestrated += 1;
                        querycounterhighestrated -= 0;
                    }
                }

            }
            else
            {

                //pick three (greater than 4 stars)
                var queryforthree = from r in _context.Books select r;
                queryforthree = queryforthree.Where(r => r.Genre.GenreName == genre && r.AvgRating > 4).OrderByDescending(r => r.AvgRating);

                if(queryforthree.Count() > 3)
                {
                    Book firstbookforthree = queryforthree.FirstOrDefault();
                    if (firstbookforthree != null)
                    {
                        if (bookstorecommend.Any())
                        {
                            foreach (Book bktorec in bookstorecommend)
                            {
                                if (bktorec.Author != firstbookforthree.Author)
                                {
                                    sameauthor = true;

                                }
                                else
                                {
                                    sameauthor = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            sameauthor = true;
                        }


                        if (sameauthor == true)
                        {
                            bookstorecommend.Add(firstbookforthree);
                            if (booksboughtbyuser.Any(r => r.BookID == firstbookforthree.BookID))
                            {
                                bookstorecommend.Remove(firstbookforthree);
                            }
                        }
                    }

                    while (bookstorecommend.Count() < 3)
                    {
                        Book booktoadd = queryforthree.Skip(skipcounter).FirstOrDefault();
                        if (booktoadd != null)
                        {
                            if (bookstorecommend.Count() > 0)
                            {
                                foreach (Book bktorec in bookstorecommend)
                                {
                                    if (bktorec.Author != booktoadd.Author)
                                    {
                                        sameauthor = true;

                                    }
                                    else
                                    {
                                        sameauthor = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                sameauthor = true;
                            }

                            if (sameauthor == true)
                            {
                                bookstorecommend.Add(booktoadd);
                                if (booksboughtbyuser.Any(r => r.BookID == booktoadd.BookID))
                                {
                                    bookstorecommend.Remove(booktoadd);
                                }
                            }

                        }
                        skipcounter += 1;
                    }
                }


                //not enough for stars???
                if(queryforthree.Count() <= 3 || bookstorecommend.Count() < 3)
                {
                    var queryforthreev2 = from r in _context.Books select r;
                    queryforthreev2 = queryforthreev2.Where(r => r.Genre.GenreName == genre).OrderByDescending(r => r.AvgRating);
                    var querycounter = queryforthreev2.Count();

                    Book firstbookforthree = queryforthreev2.FirstOrDefault();
                    if (firstbookforthree != null)
                    {
                        bookstorecommend.Add(firstbookforthree);
                        if (booksboughtbyuser.Any(r => r.BookID == firstbookforthree.BookID))
                        {
                            bookstorecommend.Remove(firstbookforthree);
                        }
                    }

                    while (bookstorecommend.Count() < 3 && querycounter > 0)
                    {
                        Book booktoadd = queryforthreev2.Skip(skipcounter3).FirstOrDefault();
                        if (booktoadd != null)
                        {
                            if(bookstorecommend.Count() > 0)
                            {
                                foreach (Book bktorec in bookstorecommend)
                                {
                                    if (bktorec.Author != booktoadd.Author)
                                    {
                                        sameauthor = true;

                                    }
                                    else
                                    {
                                        sameauthor = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                sameauthor = true;
                            }


                            if (sameauthor == true)
                            {
                                bookstorecommend.Add(booktoadd);
                                if (booksboughtbyuser.Any(r => r.BookID == booktoadd.BookID))
                                {
                                    bookstorecommend.Remove(booktoadd);
                                }
                            }

                        }
                        skipcounter += 1;
                        querycounter -=1;
                    }

                    var queryforthreev3 = from r in _context.Books select r;
                    queryforthreev3 = queryforthreev3.OrderByDescending(r => r.AvgRating);
                    while (bookstorecommend.Count < 3)
                    {
                        Book booktoadd = queryforthreev3.Skip(skipcounter3).FirstOrDefault();
                        if (booktoadd != null)
                        {
                            if(bookstorecommend.Count() < 0)
                            {
                                foreach (Book bktorec in bookstorecommend)
                                {
                                    if (bktorec.Author != booktoadd.Author)
                                    {
                                        sameauthor = true;

                                    }
                                    else
                                    {
                                        sameauthor = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                sameauthor = true;
                            }

                            if (sameauthor == true)
                            {
                                bookstorecommend.Add(booktoadd);
                                if (booksboughtbyuser.Any(r => r.BookID == booktoadd.BookID))
                                {
                                    bookstorecommend.Remove(booktoadd);
                                }
                            }

                        }
                        skipcounter3 += 1;
                    }


                }

                return bookstorecommend;


            }
            //get two if got same author and genre


            var queryfortwo = from r in _context.Books select r;
            queryfortwo = queryfortwo.Where(r => r.Genre.GenreName == genre && r.AvgRating > 4).OrderByDescending(r => r.AvgRating);

            if (queryfortwo.Count() > 2)
            {
                Book firstbookfortwo = queryfortwo.FirstOrDefault();
                if (firstbookfortwo != null)
                {
                    if (bookstorecommend.Any())
                    {
                        foreach (Book bktorec in bookstorecommend)
                        {
                            if (bktorec.Author != firstbookfortwo.Author)
                            {
                                sameauthor = true;

                            }
                            else
                            {
                                sameauthor = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        sameauthor = true;
                    }


                    if (sameauthor == true)
                    {
                        bookstorecommend.Add(firstbookfortwo);
                        if (booksboughtbyuser.Any(r => r.BookID == firstbookfortwo.BookID))
                        {
                            bookstorecommend.Remove(firstbookfortwo);
                        }
                    }
                }

                while (bookstorecommend.Count() < 3)
                {
                    Book booktoadd = queryfortwo.Skip(skipcounter).FirstOrDefault();
                    if (booktoadd != null)
                    {
                        if (bookstorecommend.Count() > 0)
                        {
                            foreach (Book bktorec in bookstorecommend)
                            {
                                if (bktorec.Author != booktoadd.Author)
                                {
                                    sameauthor = true;

                                }
                                else
                                {
                                    sameauthor = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            sameauthor = true;
                        }

                        if (sameauthor == true)
                        {
                            bookstorecommend.Add(booktoadd);
                            if (booksboughtbyuser.Any(r => r.BookID == booktoadd.BookID))
                            {
                                bookstorecommend.Remove(booktoadd);
                            }
                        }

                    }
                    skipcounter += 1;
                }
            }


            //not enough for stars???
            if (queryfortwo.Count() <= 2 || bookstorecommend.Count() < 3)
            {
                var queryforttwov2 = from r in _context.Books select r;
                queryforttwov2 = queryforttwov2.Where(r => r.Genre.GenreName == genre).OrderByDescending(r => r.AvgRating);
                var querycounter = queryforttwov2.Count();

                Book firstbookfortwo = queryforttwov2.FirstOrDefault();
                if (firstbookfortwo != null)
                {
                    if (bookstorecommend.Any())
                    {
                        foreach (Book bktorec in bookstorecommend)
                        {
                            if (bktorec.Author != firstbookfortwo.Author)
                            {
                                sameauthor = true;

                            }
                            else
                            {
                                sameauthor = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        sameauthor = true;
                    }


                    if (sameauthor == true)
                    {
                        bookstorecommend.Add(firstbookfortwo);
                        if (booksboughtbyuser.Any(r => r.BookID == firstbookfortwo.BookID))
                        {
                            bookstorecommend.Remove(firstbookfortwo);
                        }
                    }
                }

                while (bookstorecommend.Count() < 3 && querycounter > 0)
                {
                    Book booktoadd = queryforttwov2.Skip(skipcounter).FirstOrDefault();
                    if (booktoadd != null)
                    {
                        if (bookstorecommend.Any())
                        {
                            foreach (Book bktorec in bookstorecommend)
                            {
                                if (bktorec.Author != booktoadd.Author)
                                {
                                    sameauthor = true;

                                }
                                else
                                {
                                    sameauthor = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            sameauthor = true;
                        }


                        if (sameauthor == true)
                        {
                            bookstorecommend.Add(booktoadd);
                            if (booksboughtbyuser.Any(r => r.BookID == booktoadd.BookID))
                            {
                                bookstorecommend.Remove(booktoadd);
                            }
                        }

                    }
                    skipcounter += 1;
                    querycounter -= 1;
                }

                var queryfortwov3 = from r in _context.Books select r;
                queryfortwov3 = queryfortwov3.OrderByDescending(r => r.AvgRating);
                while (bookstorecommend.Count < 3)
                {
                    Book booktoadd = queryfortwov3.Skip(skipcounter3).FirstOrDefault();
                    if (booktoadd != null)
                    {
                        if (bookstorecommend.Count() < 0)
                        {
                            foreach (Book bktorec in bookstorecommend)
                            {
                                if (bktorec.Author != booktoadd.Author)
                                {
                                    sameauthor = true;

                                }
                                else
                                {
                                    sameauthor = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            sameauthor = true;
                        }

                        if (sameauthor == true)
                        {
                            bookstorecommend.Add(booktoadd);
                            if (booksboughtbyuser.Any(r => r.BookID == booktoadd.BookID))
                            {
                                bookstorecommend.Remove(booktoadd);
                            }
                        }

                    }
                    skipcounter3 += 1;
                }



            }

            return bookstorecommend;

        }
    }
}
