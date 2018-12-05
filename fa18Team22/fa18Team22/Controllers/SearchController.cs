using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using fa18Team22.Models;
using fa18Team22.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fa18Team22.Utilities;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fa18Team22.Controllers
{
    public enum SortOrderOpt { DontSort, Title, Author, MostPopular, Newest, Oldest, HighestRating }
    public enum DisplayBooks { AllBooks, InStock }
    public enum SortOrder { Ascending, Descending }

    public class SearchController : Controller
    {

        private AppDbContext _db;
        public SearchController(AppDbContext context)
        {
            _db = context;
        }

        // GET: /<Search Controller>/
        public IActionResult Index()
        {
            ViewBag.OutofStock = "Out of Stock. Check Back Soon!";
            ViewBag.InStock = "In Stock";
            ViewBag.SelectedBooksCount = _db.Books.Count();
            ViewBag.TotalBooks = _db.Books.Count();
            return View(_db.Books.Include(r => r.Genre).Include(r => r.Reviews).ToList());
        }

        public ActionResult DetailedSearch()
        {
            ViewBag.AllGenres = GetAllGenres();

            return View();
        }
        public IActionResult SearchResults(string SearchTitle, string SearchAuthor, string SearchUniqueID, int SearchGenre, DisplayBooks SelectedStock, SortOrderOpt SortButton)
        {
            List<OrderDetail> OrderDetailList = new List<OrderDetail>();
            var odquery = _db.OrderDetails.Include(o => o.Book).ThenInclude(o=>o.Reviews).Include(o => o.Order).ThenInclude(o => o.Customer);
            OrderDetailList = odquery.ToList();


            List<Book> SelectedBooks = new List<Book>();

            var query = from r in _db.Books select r;

            //title
            if (!string.IsNullOrEmpty(SearchTitle))
            {
                query = query.Where(r => r.Title.Contains(SearchTitle));
            }

            //author
            if (!string.IsNullOrEmpty(SearchAuthor))
            {
                query = query.Where(r => r.Author.Contains(SearchAuthor));
            }

            //unique number
            if (!string.IsNullOrEmpty(SearchUniqueID))
            {
                int intUniqueNumber;
                try
                {
                    intUniqueNumber = Convert.ToInt32(SearchUniqueID);
                }
                catch
                {
                    //Add a message for the viewbag
                    ViewBag.Message = "You must enter a valid unique ID";

                    //re-populate drop down
                    //ViewBag.AllGenres .....

                    //Send user back to home page
                    return View("DetailedSearch"); //may need to change what goes in ""
                }

                query = query.Where(r => r.UniqueID == intUniqueNumber);

            }



            //genre
            if (SearchGenre != 0) // 0 = they chose "all genres" from the drop-down
            {
                Genre GenreToDisplay = _db.Genres.Find(SearchGenre);
                query = query.Where(r => r.Genre == GenreToDisplay);
            }


            //selected books - all or in stock only
            switch (SelectedStock)
            {
                case DisplayBooks.AllBooks:
                    break;
                case DisplayBooks.InStock:
                    query = query.Where(r => r.Inventory > 0);
                    break;
                default:
                    break;
            }

            query = query.Include(r => r.Reviews);
            SelectedBooks = query.ToList();

            List<SearchVM> searchVms = new List<SearchVM>();

            //MAKING THE ORDER DETAIL and checking which is most popular
            foreach(Book book in SelectedBooks)
            {
                List<Review> reviewslist = new List<Review>();

                var revquery = from r in _db.Reviews select r;
                revquery = revquery.Where(r => r.ApprovalStatus == true);
                revquery = revquery.Include(r => r.Book);
                reviewslist = revquery.ToList();

                SearchVM svm = new SearchVM();
                svm.BookID = book.BookID;
                svm.Title = book.Title;
                svm.Author = book.Author;
                svm.AvgRating = book.AvgRating;
                svm.SalesPrice = book.SalesPrice;
                if (book.Inventory > 0) { svm.InStock = true; }
                if (book.Inventory <= 0) { svm.InStock = false; }
                svm.UniqueNumber = book.UniqueID;
                svm.BookDetail = book.BookDetail;
                svm.AvgRating = book.AvgRating;
             
                Int32 intCountOrdered = 0;
                foreach(OrderDetail od in OrderDetailList)
                {
                    if(od.Book.BookID == book.BookID)
                    {
                        intCountOrdered += od.Quantity;
                    }
                }
                svm.QuantityOrdered = intCountOrdered;
                svm.PublishDate = book.PublishDate;
                searchVms.Add(svm);

            }

            //populate viewbags
            ViewBag.SelectedBooksCount = searchVms.Count();
            ViewBag.TotalBooks = _db.Books.Count();
            ViewBag.OutofStock = "Out of Stock. Check Back Soon!";
            ViewBag.InStock = "In Stock";

            switch (SortButton)
            {
                case SortOrderOpt.DontSort: break;
                case SortOrderOpt.Title:
                    return View("ViewModelIndex", searchVms.OrderBy(r => r.Title));
                case SortOrderOpt.Author:
                    return View("ViewModelIndex", searchVms.OrderBy(r => r.Author));
                case SortOrderOpt.MostPopular:
                    return View("ViewModelIndex", searchVms.OrderByDescending(r => r.QuantityOrdered));
                case SortOrderOpt.Newest:
                    return View("ViewModelIndex", searchVms.OrderByDescending(r => r.PublishDate));
                case SortOrderOpt.Oldest:
                    return View("ViewModelIndex", searchVms.OrderBy(r => r.PublishDate));
                case SortOrderOpt.HighestRating:
                    return View("ViewModelIndex", searchVms.OrderByDescending(r => r.AvgRating));
            }

            ViewBag.OutofStock = "Out of Stock. Check Back Soon!";
            ViewBag.InStock = "In Stock";
            SelectedBooks = query.ToList();
            ViewBag.SelectedBooksCount = searchVms.Count();
            ViewBag.TotalBooks = _db.Books.Count();
            //ViewBag.SelectedBooksSearch = SelectedBooksSearch.Count();
            return View("ViewModelIndex", searchVms);

        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _db.Books.Include(m => m.Genre).Include(r=>r.Reviews)
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }







        public SelectList GetAllGenres()
        {
            List<Genre> Genres = _db.Genres.ToList();

            Genre SelectNone = new Genre() { GenreID = 0, GenreName = "All Genres" };
            Genres.Add(SelectNone);


            //convert list to select list
            SelectList AllGenres = new SelectList(Genres.OrderBy(g => g.GenreID), "GenreID", "GenreName");

            //return the select list
            return AllGenres;
        }

     

    }
}