using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using fa18Team22.Models;
using fa18Team22.DAL;
using Microsoft.EntityFrameworkCore;

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
            Order order = _context.Orders.Include(r => r.OrderDetails).ThenInclude(OrderDetails => OrderDetails.Book).ThenInclude(Book => Book.Genre).FirstOrDefault(r => r.OrderID == id);
            List<Book> listofrecbooks = RecommendedBooks(order);
            return View("Recommend", listofrecbooks);
        }


        public List<Book> RecommendedBooks(Order order)
        {
            List<Book> bookstorecommend = new List<Book>();
            OrderDetail orderDetails = order.OrderDetails.FirstOrDefault();
            String userId = User.Identity.Name;
            List<Book> booksboughtbyuser = new List<Book>();
            AppUser currentuser = _context.Users.FirstOrDefault(u => u.UserName == userId);


            var booksboughtquery = from r in _context.OrderDetails.Include(r => r.Order) select r;
            booksboughtquery = booksboughtquery.Where(r => r.Order.Customer == currentuser);
            foreach (OrderDetail orddlt in booksboughtquery)
            {
                booksboughtbyuser.Add(orddlt.Book);
            }

            string author = orderDetails.Book.Author;
            string genre = orderDetails.Book.Genre.GenreName;
            var query = from r in _context.Books select r;
            query = query.Where(r => r.Author == author && r.Genre.GenreName == genre);
            if (query.ToList().Count() > 1)
            {
                List<Book> querylist = query.ToList();
                Book highestratedbook = querylist.OrderByDescending(r => r.Author).FirstOrDefault();
                if (highestratedbook != null)
                {
                    bookstorecommend.Add(highestratedbook);
                    if (booksboughtbyuser.Any(r => r == highestratedbook))
                    {
                        bookstorecommend.Remove(highestratedbook);
                    }
                }

            }

            var query2 = from r in _context.Books select r;
            query2 = query2.Where(r => r.Genre.GenreName == genre && r.AvgRating <= 4).OrderByDescending(r => r.AvgRating);
            Book secondbook = query2.FirstOrDefault();
            if (secondbook != null)
            {
                bookstorecommend.Add(secondbook);
                if (booksboughtbyuser.Any(r => r == secondbook))
                {
                    bookstorecommend.Remove(secondbook);
                }
            }

            Book thirdbook = query2.Skip(1).FirstOrDefault();
            if (thirdbook != null)
            {
                bookstorecommend.Add(thirdbook);
                if (booksboughtbyuser.Any(r => r == thirdbook))
                {
                    bookstorecommend.Remove(thirdbook);
                }
            }
            if (bookstorecommend.Count() < 3)
            {
                var lessthan4query = from r in _context.Books select r;
                Book extrabooktoadd = lessthan4query.Where(r => r.AvgRating < 4).OrderByDescending(r => r.AvgRating).FirstOrDefault();
                if (extrabooktoadd != null)
                {
                    bookstorecommend.Add(extrabooktoadd);
                    if (booksboughtbyuser.Any(r => r == extrabooktoadd))
                    {
                        bookstorecommend.Remove(extrabooktoadd);
                    }
                }

            }
            Int16 skipcounter = 1;
            while (bookstorecommend.Count() < 3)
            {

                var queryhighestoverall = from r in _context.Books select r;
                Book nexttoadd = queryhighestoverall.OrderByDescending(r => r.AvgRating).Skip(skipcounter).FirstOrDefault();
                if (nexttoadd != null)
                {
                    bookstorecommend.Add(nexttoadd);
                    if (booksboughtbyuser.Any(r => r == nexttoadd))
                    {
                        bookstorecommend.Remove(nexttoadd);
                    }
                }
                skipcounter += 1;

            }
            return bookstorecommend;

        }
    }
}
