using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fa18Team22.DAL;
using fa18Team22.Models;
using fa18Team22.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace fa18Team22.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }
        private SelectList GetAllGenres()
        {
            List<Genre> allGenres = _context.Genres.ToList();

            Genre NewGenre = new Genre() { GenreID = 0, GenreName = "New Genre" };
            allGenres.Add(NewGenre);

            SelectList GenreList = new SelectList(allGenres, "GenreID", "GenreName");
            return GenreList;
        }
        private SelectList GetAllGenres(Book book)
        {
            //create a list of all the suppliers
            List<Genre> allGenres = _context.Genres.ToList();

            //create the multiselect with the overload for currently selected items
            SelectList GenreList = new SelectList(allGenres, "GenreID", "GenreName", book.Genre.GenreID);

            //return the list
            return GenreList;
        }

        // GET: Books
        [Authorize(Roles ="Manager")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.Include(m => m.Genre).Include(m=>m.Reviews).ToListAsync());
        }

        // GET: Books/Details/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.Include(m => m.Genre).Include(r => r.Reviews)
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            //String userid = User.Identity.Name;
            //AppUser currentuser = _context.Users.FirstOrDefault(r => r.UserName == userid);

            //var orderquery = from r in _context.OrderDetails.Include(r => r.Book).Include(r => r.Order).ThenInclude(r => r.Customer) select r;
            //orderquery = orderquery.Where(r => r.Order.Customer.UserName == currentuser.UserName && r.Order.IsComplete == false);
            //List<OrderDetail> currentorddetails = orderquery.ToList();
            //List<Book> booksinorder = new List<Book>();


            //if (currentorddetails.Count() != 0)
            //{
            //    foreach (OrderDetail orddetail in currentorddetails)
            //    {
            //        booksinorder.Add(orddetail.Book);
            //    }

            //    foreach (Book bk in booksinorder)
            //    {
            //        if (bk.Title == book.Title)
            //        {
            //            ViewBag.BookInCart = "This book is already in your cart.";
            //        }
            //        else
            //        {
            //            ViewBag.BookInCart = "";
            //        }
            //    }
            //}
            //else
            //{
            //    ViewBag.BookInCart = "";
            //}



            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            ViewBag.AllGenres = GetAllGenres();
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult Create(int SelectedGenre, string NewGenre, [Bind("BookID,UniqueID,Title,Author,PublishDate,BookDetail,SalesPrice,Inventory,ReplenishMinimum")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.UniqueID = GenerateBUN.GetNextBUN(_context);
                _context.Add(book);
                _context.SaveChanges();

                if (SelectedGenre == 0)
                {
                    Genre newgenre = new Genre();
                    newgenre.GenreName = NewGenre;
                    _context.Add(newgenre);
                    _context.SaveChanges();


                    Genre dbGenre = _context.Genres.Include(c => c.Books).FirstOrDefault(c => c.GenreID == newgenre.GenreID);
                    Book dbBook = _context.Books.Include(c => c.Genre).FirstOrDefault(c => c.UniqueID == book.UniqueID);
                    dbGenre.Books.Add(dbBook);
                    dbBook.Genre = dbGenre;
                    //lets user pick genre the book belongs to, then add the genre to the book instance and the book instance to the genre                    _context.Add(newgenre);
                    _context.Update(dbGenre);
                    _context.Update(dbBook);
                    _context.SaveChanges();
                }
                if (SelectedGenre != 0)
                {
                    if (ModelState.IsValid)
                    {

                        Genre dbGenre = _context.Genres.Include(c => c.Books).FirstOrDefault(c => c.GenreID == SelectedGenre);
                        Book dbBook = _context.Books.Include(c => c.Genre).FirstOrDefault(c => c.UniqueID == book.UniqueID);
                        dbGenre.Books.Add(dbBook);
                        //lets user pick genre the book belongs to, then add the genre to the book instance and the book instance to the genre
                        _context.Update(dbGenre);
                        _context.SaveChanges();
                    }
                }
                //Generate next course number
                return RedirectToAction(nameof(Index));

            }
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Manager")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _context.Books.Include(c => c.Genre)
                                          .FirstOrDefault(c => c.BookID == id);
            if (book == null)
            {
                return NotFound();
            }
            ViewBag.AllGenres = GetAllGenres(book);
            return View(book);
        }

         //POST: Books/Edit/5
         //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public IActionResult Edit(int SelectedGenre, Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Book dbBook = _context.Books
                        .Include(c => c.Genre)
                        .FirstOrDefault(c => c.BookID == book.BookID);

                    dbBook.Title = book.Title;
                    dbBook.Author = book.Author;
                    dbBook.PublishDate = book.PublishDate;
                    dbBook.BookDetail = book.BookDetail;
                    dbBook.SalesPrice = book.SalesPrice;
                    dbBook.Inventory = book.Inventory;
                    dbBook.IsDiscontinued = book.IsDiscontinued;
                    dbBook.ReplenishMinimum = book.ReplenishMinimum;

                    _context.Update(dbBook);
                    _context.SaveChanges();

                    //edit department/course relationships

                    //loop through selected departments and find ones that need to be removed
                    Genre dbGenre = _context.Genres.Include(c => c.Books).FirstOrDefault(c => c.GenreID == SelectedGenre);
                    dbGenre.Books.Remove(book);
                    dbGenre.Books.Add(dbBook);
                    _context.Update(dbGenre);
                    _context.Update(dbBook);
                    //_context.SaveChanges();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            ViewBag.AllGenres = GetAllGenres(book);
            return View(book);
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }

    }
}
