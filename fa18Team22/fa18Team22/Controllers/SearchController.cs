using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using fa18Team22.Models;
using fa18Team22.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult DetailedSearch()
        {
            ViewBag.AllGenres = GetAllGenres();
            ViewBag.AllSortObjects = GetAllSortByOptions();
            return View();
        }
        public IActionResult SearchResults(string SearchTitle, string SearchAuthor, string SearchUniqueID, int SearchGenre, DisplayBooks SelectedStock, String SortBy, SortOrder SelectedSortOrder)
        {
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

            //sort by option
            if (!string.IsNullOrEmpty(SortBy)) //should this be nullable???
            {
                if(SortBy == "Title")
                {
                    SelectedBooksSearch = (System.Collections.Generic.List<fa18Team22.Models.Book>)SelectedBooksSearch.OrderByDescending(r => r.Title);
                }
                if(SortBy == "Author")
                {
                    SelectedBooksSearch = (System.Collections.Generic.List<fa18Team22.Models.Book>)SelectedBooksSearch.OrderByDescending(r => r.Author);
                }
                //NO clue how to do this one
                if (SortBy == "Most Popular")
                {
                    SelectedBooksSearch = (System.Collections.Generic.List<fa18Team22.Models.Book>)SelectedBooksSearch.OrderByDescending(r => r.BookID);
                }

                if (SortBy == "Newest First")
                {
                    SelectedBooksSearch = (System.Collections.Generic.List<fa18Team22.Models.Book>)SelectedBooksSearch.OrderByDescending(r => r.PublishDate);
                }
                if (SortBy == "OldestFirst")
                {
                    SelectedBooksSearch = (System.Collections.Generic.List<fa18Team22.Models.Book>)SelectedBooksSearch.OrderByDescending(r => r.PublishDate);
                }
                if (SortBy == "Highest Rated")
                {
                    SelectedBooksSearch = (System.Collections.Generic.List<fa18Team22.Models.Book>)SelectedBooksSearch.OrderByDescending(r => r.AvgRating);
                }


                //re-populate drop down
                ViewBag.AllSortObjects = GetAllSortByOptions();

                //Send user back to home page
                return View("DetailedSearch");
            }

            //if (StarAmount == StarList.greaterThan)
            //{
            //    query = query.Where(x => x.StarCount >= decStarValue);
            //}

            //if (StarAmount == StarList.lessThan)
            //{
            //    query = query.Where(x => x.StarCount <= decStarValue);
            //}
            ViewBag.TotalRepositories = _db.Books.Count();
            ViewBag.SelectedBooksSearch = SelectedBooksSearch.Count();
            return View("Index", SelectedBooksSearch);

        }







        public SelectList GetAllGenres()
        {
            List<Genre> Genre = _db.Genres.ToList();

            Genre SelectNone = new Genre() { GenreID = 0, GenreName = "All Genres" };
            Genre.Add(SelectNone);


            //convert list to select list
            SelectList AllGenre = new SelectList(Genre.OrderBy(m => m.GenreID), "GenreID", "Name");

            //return the select list
            return AllGenre;
        }

        public SelectList GetAllSortByOptions()
        {
            List<String> SortBy = new List<string>();

            SortBy.Add("Don't Sort");
            SortBy.Add("Title");
            SortBy.Add("Author");
            SortBy.Add("Most Popular");
            SortBy.Add("Newest First"); 
            SortBy.Add("Oldest First");
            SortBy.Add("Highest Rated");


            //convert list to select list
            SelectList SortByOptions = new SelectList(SortBy);


            //return the select list
            return SortByOptions;
        }


    }
}