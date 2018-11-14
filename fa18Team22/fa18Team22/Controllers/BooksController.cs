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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.Include(m => m.Genre).ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
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
                    //lets user pick genre the book belongs to, then add the genre to the book instance and the book instance to the genre
                    _context.Add(newgenre);
                    _context.Update(dbGenre);
                    _context.SaveChangesAsync();
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

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(int SelectedGenre, Book book)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            Book dbBook = _context.Books
        //                .Include(c => c.Genre)
        //                .FirstOrDefault(c => c.BookID == book.BookID);

        //            dbBook.Title = book.Title;
        //            dbBook.Author = book.Author;
        //            dbBook.PublishDate = book.PublishDate;
        //            dbBook.BookDetail = book.BookDetail;
        //            dbBook.SalesPrice = book.SalesPrice;
        //            dbBook.Inventory = book.Inventory;
        //            dbBook.AvgRating = book.AvgRating;
        //            dbBook.ReplenishMinimum = book.ReplenishMinimum;

        //            _context.Update(dbBook);
        //            _context.SaveChanges();

        //            //edit department/course relationships

        //            //loop through selected departments and find ones that need to be removed
        //            Genre dbGenre = _context.Genres.Include(c => c.Books).FirstOrDefault(c => c.GenreID == SelectedGenre);
        //            dbGenre.Books.Remove(book);
        //            dbGenre.Books.Add(dbBook);
        //            _context.Update(dbGenre);
        //            _context.Update(dbBook);

        //        //    //now add the departments that are new
        //        //    foreach (int i in SelectedSuppliers)
        //        //    {
        //        //        if (dbProduct.ProductSuppliers.Any(c => c.Supplier.SupplierID == i) == false)
        //        //        //this supplier has not yet been added
        //        //        {
        //        //            //create a new course department
        //        //            ProductSupplier cd = new ProductSupplier();

        //        //            //connect the new course department to the department and course
        //        //            cd.Supplier = _context.Suppliers.Find(i);
        //        //            cd.Product = dbProduct;

        //        //            //update the database
        //        //            _context.ProductSuppliers.Add(cd);
        //        //            _context.SaveChanges();
        //        //        }
        //        //    }

        //        //}
        //        //catch (DbUpdateConcurrencyException)
        //        //{
        //        //    if (!ProductExists(product.ProductID))
        //        //    {
        //        //        return NotFound();
        //        //    }
        //        //    else
        //        //    {
        //        //        throw;
        //        //    }
        //        }

        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewBag.AllGenres = GetAllGenres(book);
        //    return View(book);

        //    //if (id != book.BookID)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //if (ModelState.IsValid)
        //    //{
        //    //    try
        //    //    {
        //    //        _context.Update(book);
        //    //        await _context.SaveChangesAsync();
        //    //    }
        //    //    catch (DbUpdateConcurrencyException)
        //    //    {
        //    //        if (!BookExists(book.BookID))
        //    //        {
        //    //            return NotFound();
        //    //        }
        //    //        else
        //    //        {
        //    //            throw;
        //    //        }
        //    //    }
        //    //    return RedirectToAction(nameof(Index));
        //    //}
        //    //return View(book);
        //}

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookID == id);
        }

    }
}
