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
            ViewBag.SelectedBooksCount = _db.Books.Count();
            ViewBag.TotalBooks = _db.Books.Count();
            return View(_db.Books.Include(r => r.Genre).ToList());
        }

        public ActionResult DetailedSearch()
        {
            ViewBag.AllGenres = GetAllGenres();
            ViewBag.AllSortObjects = GetAllSortByOptions();
            return View();
        }
        public IActionResult SearchResults(string SearchTitle, string SearchAuthor, string SearchUniqueID, int SearchGenre, DisplayBooks SelectedStock,int SortBy)
        {
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

            List<Book> SelectedBooksSearch = query.ToList();

            //TODO: sort by option
            if (SortBy != 1) //should this be nullable???
            {
                if (SortBy == 2)
                {
                    SelectedBooks = query.ToList();
                    ViewBag.SelectedBooksCount = SelectedBooks.Count();
                    ViewBag.TotalBooks = _db.Books.Count();
                    return View("Index", SelectedBooksSearch.OrderBy(r => r.Title));
                    //SelectedBooksSearch = (System.Collections.Generic.List<fa18Team22.Models.Book>)SelectedBooksSearch.OrderByDescending(r => r.Title);
                }
                if (SortBy == 3)
                {
                    SelectedBooks = query.ToList();
                    ViewBag.SelectedBooksCount = SelectedBooks.Count();
                    ViewBag.TotalBooks = _db.Books.Count();
                    return View("Index", SelectedBooksSearch.OrderBy(r => r.Author));
                    //SelectedBooksSearch = (System.Collections.Generic.List<fa18Team22.Models.Book>)SelectedBooksSearch.OrderByDescending(r => r.Author);
                }
                //TODO: NO clue how to do this one
                if (SortBy == 4)
                {
                    SelectedBooks = query.ToList();
                    ViewBag.SelectedBooksCount = SelectedBooks.Count();
                    ViewBag.TotalBooks = _db.Books.Count();
                    return View("Index", SelectedBooksSearch.OrderBy(r => r.BookID));
                    //SelectedBooksSearch = (System.Collections.Generic.List<fa18Team22.Models.Book>)SelectedBooksSearch.OrderByDescending(r => r.BookID);
                }

                if (SortBy == 5)
                {
                    SelectedBooks = query.ToList();
                    ViewBag.SelectedBooksCount = SelectedBooks.Count();
                    ViewBag.TotalBooks = _db.Books.Count();
                    return View("Index", SelectedBooksSearch.OrderByDescending(r => r.PublishDate));
                    //SelectedBooksSearch = (System.Collections.Generic.List<fa18Team22.Models.Book>)SelectedBooksSearch.OrderByDescending(r => r.PublishDate);
                }
                if (SortBy == 6)
                {
                    SelectedBooks = query.ToList();
                    ViewBag.SelectedBooksCount = SelectedBooks.Count();
                    ViewBag.TotalBooks = _db.Books.Count();
                    return View("Index", SelectedBooksSearch.OrderBy(r => r.PublishDate));
                    //SelectedBooksSearch = (System.Collections.Generic.List<fa18Team22.Models.Book>)SelectedBooksSearch.OrderByAscending(r => r.PublishDate);
                }
                if (SortBy == 7)
                {
                    //REMINDER: TODO: No Ratings Yet please check
                    SelectedBooks = query.ToList();
                    ViewBag.SelectedBooksCount = SelectedBooks.Count();
                    ViewBag.TotalBooks = _db.Books.Count();
                    return View("Index", SelectedBooksSearch.OrderByDescending(r => r.AvgRating));
                    //SelectedBooksSearch = (System.Collections.Generic.List<fa18Team22.Models.Book>)SelectedBooksSearch.OrderByDescending(r => r.AvgRating);
                }


                ////re-populate drop down
                //ViewBag.AllSortObjects = GetAllSortByOptions();
                //ViewBag.AllGenres = GetAllGenres();

                ////Send user back to home page
                //return View("DetailedSearch");
            }

            //if (StarAmount == StarList.greaterThan)
            //{
            //    query = query.Where(x => x.StarCount >= decStarValue);
            //}

            //if (StarAmount == StarList.lessThan)
            //{
            //    query = query.Where(x => x.StarCount <= decStarValue);
            //}
            SelectedBooks = query.ToList();
            ViewBag.SelectedBooksCount = SelectedBooks.Count();
            ViewBag.TotalBooks = _db.Books.Count();
            //ViewBag.SelectedBooksSearch = SelectedBooksSearch.Count();
            return View("Index", SelectedBooksSearch);
            
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

        public SelectList GetAllSortByOptions()
        {
            List<SortOrderOption> SortOrderOptions = new List<SortOrderOption>();

            SortOrderOption DontSort = new SortOrderOption() { SortOrderOptionID = 1, SortOption = "Don't Sort" };
            SortOrderOptions.Add(DontSort);
            SortOrderOption Title = new SortOrderOption() { SortOrderOptionID = 2, SortOption = "Title" };
            SortOrderOptions.Add(Title);
            SortOrderOption Author = new SortOrderOption() { SortOrderOptionID = 3, SortOption = "Author" };
            SortOrderOptions.Add(Author);
            SortOrderOption MostPopular = new SortOrderOption() { SortOrderOptionID = 4, SortOption = "Most Popular" };
            SortOrderOptions.Add(MostPopular);
            SortOrderOption Newest = new SortOrderOption() { SortOrderOptionID = 5, SortOption = "Newest First" };
            SortOrderOptions.Add(Newest);
            SortOrderOption Oldest = new SortOrderOption() { SortOrderOptionID = 6, SortOption = "Oldest First" };
            SortOrderOptions.Add(Oldest);
            SortOrderOption HighestRating420 = new SortOrderOption() { SortOrderOptionID = 7, SortOption = "Highest Rated" };
            SortOrderOptions.Add(HighestRating420);

            //convert list to select list
            SelectList AllSortOptions = new SelectList(SortOrderOptions.OrderBy(so => so.SortOrderOptionID),"SortOrderOptionID","SortOption");


            //return the select list
            return AllSortOptions;
        }


    }
}