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
            return View();
        }
        public IActionResult SearchResults(string SearchTitleAuthor, string SearchTitle, string SearchAuthor, string SearchUniqueNumber, int SearchGenre, DisplayBooks SelectedBooks, int SortBy)
        {
            var query = from r in _db.Books select r;

            //title or author
            if (!string.IsNullOrEmpty(SearchTitleAuthor))
            {
                query = query.Where(r => r.Title.Contains(SearchTitleAuthor) || r.Author.Contains(SearchTitleAuthor));
            }

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
            if (!string.IsNullOrEmpty(SearchUniqueNumber))
            {
                int intUniqueNumber;
                try
                {
                    intUniqueNumber = Convert.ToInt16(SearchUniqueNumber);
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
            if (SearchGenre != 0) // they chose "all language from the drop-down
            {
                Genre GenreToDisplay = _db.Genres.Find(SearchGenre);
                query = query.Where(r => r.Genre == GenreToDisplay);
            }


            //selected books - all or in stock only
            switch (SelectedBooks)
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

    }
}