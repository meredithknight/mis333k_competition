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
    public enum SortOrderOpt {DontSort,Title,Author,MostPopular,Newest,Oldest,HighestRating}
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

            return View();
        }
        public IActionResult SearchResults(string SearchTitle, string SearchAuthor, string SearchUniqueID, int SearchGenre, DisplayBooks SelectedStock,SortOrderOpt SortButton)
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

            SelectedBooks = query.ToList();
            ViewBag.SelectedBooksCount = SelectedBooks.Count();
            ViewBag.TotalBooks = _db.Books.Count();

            switch (SortButton)
            {
                case SortOrderOpt.DontSort:break;
                case SortOrderOpt.Title:
                    return View("Index", SelectedBooks.OrderBy(r => r.Title));
                case SortOrderOpt.Author:
                    return View("Index", SelectedBooks.OrderBy(r => r.Author));
                case SortOrderOpt.MostPopular:
                    return View("Index", SelectedBooks.OrderBy(r => r.BookID));
                case SortOrderOpt.Newest:
                    return View("Index", SelectedBooks.OrderByDescending(r => r.PublishDate));
                case SortOrderOpt.Oldest:
                    return View("Index", SelectedBooks.OrderBy(r => r.PublishDate));
                case SortOrderOpt.HighestRating:
                    return View("Index", SelectedBooks.OrderByDescending(r => r.AvgRating));
            }


            SelectedBooks = query.ToList();
            ViewBag.SelectedBooksCount = SelectedBooks.Count();
            ViewBag.TotalBooks = _db.Books.Count();
            //ViewBag.SelectedBooksSearch = SelectedBooksSearch.Count();
            return View("Index", SelectedBooks);
            
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