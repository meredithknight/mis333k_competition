using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fa18Team22.DAL;
using fa18Team22.Models;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace fa18Team22.Controllers
{
    public class ProcurementController : Controller
    {
        private readonly AppDbContext _context;

        public ProcurementController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Procurement
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Procurements.Include(p => p.Book).Include(r => r.Employee).Where(r => r.ProcurementStatus == null || r.ProcurementStatus == false).ToListAsync());
        }


        // GET: Procurement/Details/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procurement = await _context.Procurements.Include(m => m.Book).Include(m => m.Employee)
                .FirstOrDefaultAsync(m => m.ProcurementID == id);
            if (procurement == null)
            {
                return NotFound();
            }

            return View(procurement);
        }


        //GET: Automatic Order
        [Authorize(Roles = "Manager")]
        public IActionResult AutomaticOrder()
        {
            var query = from r in _context.Books select r;
            //change to < r.ReplenishMinimum, exclude books on active procurement
            query = query.Where(r => r.Inventory <= r.ReplenishMinimum);
            List<Book> ProcurementBookSearch = query.ToList();
            return View(ProcurementBookSearch);

        }

        [Authorize(Roles = "Manager")]
        public IActionResult AutoOrder1(int[] IdsToAdd)
        {
            var query = from r in _context.Books select r;
            //change to < r.ReplenishMinimum, exclude books on active procurement
            List<Book> allBooks = new List<Book>();
            allBooks = query.ToList();

            List<Book> SelectedBooks = new List<Book>();

            foreach(Book book in allBooks)
            {
                foreach(int id in IdsToAdd)
                {
                    if (id == book.BookID) { SelectedBooks.Add(book); }
                }
            }

            return View("IndexCheck", SelectedBooks);
        }

        //GET
        [Authorize(Roles = "Manager")]
        public IActionResult AddProcurements()
        {
            var query = from r in _context.Books select r;
            //change to < r.ReplenishMinimum, exclude books on active procurement
            List<Book> allBooks = new List<Book>();
            query = query.Where(r => r.Inventory < r.ReplenishMinimum);
            query = query.Include(r => r.Procurements).Include(r => r.Reviews);
            allBooks = query.ToList();

            List<Procurement> allprocs = new List<Procurement>();
            var procquery = from p in _context.Procurements select p;
            procquery = procquery.Include(p => p.Book).Include(p => p.Employee);
            allprocs = procquery.ToList();

            String strUserId = User.Identity.Name;
            AppUser apvmuser = _context.Users.FirstOrDefault(u => u.UserName == strUserId);


            List<AddProcurementVM> BooksToOrder = new List<AddProcurementVM>();
            foreach(Book book in allBooks)
            {
                    AddProcurementVM apvm = new AddProcurementVM();
                    apvm.Title = book.Title;
                    apvm.ProcurementDate = System.DateTime.Today;
                    apvm.BookID = book.BookID;
                    apvm.Author = book.Author;
                    apvm.AvgRatingProc = (decimal)book.AvgRating;
                    apvm.Cost = book.BookCost;
                    apvm.userID = User.Identity.Name;
                    apvm.Inventory = book.Inventory;
                    apvm.InventoryMinimum = book.ReplenishMinimum;
                    apvm.SellingPrice = book.SalesPrice;
                    apvm.ProfitMargin = ((Decimal)book.AvgSalesPrice - (Decimal)book.AvgBookCost);
                    apvm.IncludeInProcurement = true;
                    apvm.QuantityToOrder = 5;
                    BooksToOrder.Add(apvm);

                foreach (Procurement proc in allprocs)
                {
                    if (proc.ProcurementStatus == false)
                    {
                        if (book.BookID == proc.Book.BookID)
                        {
                            BooksToOrder.Remove(apvm);
                        }
                    }
                }

            }
            
            return View(BooksToOrder);
        }

        //POST
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult AddProcurements(List<AddProcurementVM> procurementVMs)
        {

            foreach(AddProcurementVM apvm in procurementVMs)
            {
                if(apvm.IncludeInProcurement == true)
                {
                    if (apvm.Cost <= 0 || apvm.QuantityToOrder <= 0)
                    {
                        var query = from r in _context.Books select r;
                        //change to < r.ReplenishMinimum, exclude books on active procurement
                        List<Book> allBooks = new List<Book>();
                        query = query.Where(r => r.Inventory < r.ReplenishMinimum);
                        query = query.Include(r => r.Procurements).Include(r => r.Reviews);
                        allBooks = query.ToList();

                        List<Procurement> allprocs = new List<Procurement>();
                        var procquery = from p in _context.Procurements select p;
                        procquery = procquery.Include(p => p.Book).Include(p => p.Employee);
                        allprocs = procquery.ToList();

                        String strUserId = User.Identity.Name;
                        AppUser apvmuser = _context.Users.FirstOrDefault(u => u.UserName == strUserId);


                        List<AddProcurementVM> BooksToOrder = new List<AddProcurementVM>();
                        foreach (Book book in allBooks)
                        {
                            AddProcurementVM apvm2 = new AddProcurementVM();
                            apvm2.Title = book.Title;
                            apvm2.ProcurementDate = System.DateTime.Today;
                            apvm2.BookID = book.BookID;
                            apvm2.Author = book.Author;
                            apvm2.AvgRatingProc = (decimal)book.AvgRating;
                            apvm2.Cost = book.BookCost;
                            apvm2.userID = User.Identity.Name;
                            apvm2.Inventory = book.Inventory;
                            apvm2.InventoryMinimum = book.ReplenishMinimum;
                            apvm2.SellingPrice = book.SalesPrice;
                            apvm2.ProfitMargin = ((Decimal)book.AvgSalesPrice - (Decimal)book.AvgBookCost);
                            apvm2.IncludeInProcurement = true;
                            apvm2.QuantityToOrder = 5;
                            BooksToOrder.Add(apvm2);

                            foreach (Procurement proc in allprocs)
                            {
                                if (proc.ProcurementStatus == false)
                                {
                                    if (book.BookID == proc.Book.BookID)
                                    {
                                        BooksToOrder.Remove(apvm2);
                                    }
                                }
                            }


                        }
                        ViewBag.ProcurementError = "Quantity and Cost need to be greater than zero";
                        return View("AddProcurements", BooksToOrder);
                    }
                    else
                    {
                        ViewBag.ProcurementError = "";
                        Book apvmbook = _context.Books.FirstOrDefault(r => r.BookID == apvm.BookID);
                        string strID = apvm.userID;
                        AppUser apvmuser = _context.Users.FirstOrDefault(u => u.UserName == apvm.userID);


                        Procurement procurement = new Procurement() { Book = apvmbook, Employee = apvmuser };
                        procurement.Price = apvm.Cost;
                        procurement.ProcurementDate = apvm.ProcurementDate;
                        procurement.ProcurementStatus = false;
                        procurement.Quantity = apvm.QuantityToOrder;

                        String userId = User.Identity.Name;
                        AppUser user = _context.Users.FirstOrDefault(u => u.UserName == userId);
                        procurement.Employee = user;

                        //update cost to be latest cost paid
                        apvmbook.BookCost = apvm.Cost;

                        _context.Books.Update(apvmbook);
                        _context.Procurements.Add(procurement);
                        _context.SaveChanges();

                    }
                }

                //check to see if cost and quantity is greater than zero
            }

            return RedirectToAction("Index");
        }

        public ActionResult IndexCheck()
        {
            return View();
        }


        //addwinnineb bid controller

        //view adding winning bid 

        //POST: Automatic Order
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult AutomaticOrder(int[] IdsToAdd ,string[] QuantityToAdd, Decimal Cost) 
        {

            var query = from r in _context.Books select r;
            //change to < r.ReplenishMinimum, exclude books on active procurement
            List<Book> allBooks = new List<Book>();
            allBooks = query.ToList();

            List<Book> SelectedBooks = new List<Book>();

            foreach (Book book in allBooks)
            {
                foreach (int id in IdsToAdd)
                {
                    if (id == book.BookID) { SelectedBooks.Add(book); }
                }
            }

            Int16 Qt = Convert.ToInt16(QuantityToAdd);

            foreach(Book b in SelectedBooks)
            {
                Procurement procurement = new Procurement();
                procurement.Price = Cost;
                //procurement.Quantity = Quantity;
            }



            return View("Index");
        }


        // GET: Procurement/Create
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Procurement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([Bind("ProcurementID,ProcurementDate,Price,Quantity")] Procurement procurement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(procurement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(procurement);
        }

        // GET: Procurement/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procurement = await _context.Procurements.FindAsync(id);
            if (procurement == null)
            {
                return NotFound();
            }
            return View(procurement);
        }

        // POST: Procurement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int id, [Bind("ProcurementID,ProcurementDate,Price,Quantity")] Procurement procurement)
        {
            if (id != procurement.ProcurementID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(procurement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcurementExists(procurement.ProcurementID))
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
            return View(procurement);
        }

        // GET: Procurement/Delete/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procurement = await _context.Procurements
                .FirstOrDefaultAsync(m => m.ProcurementID == id);
            if (procurement == null)
            {
                return NotFound();
            }

            return View(procurement);
        }

        // POST: Procurement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var procurement = await _context.Procurements.FindAsync(id);
            _context.Procurements.Remove(procurement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcurementExists(int id)
        {
            return _context.Procurements.Any(e => e.ProcurementID == id);
        }

        //GET: check-in 
        public ActionResult CheckIn(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View();
        }

        //POST: check-in
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public ActionResult CheckIn(int? id, string IncomingQuantity )
        {

            try
            {
                Int16 intIncomingQuantity = Convert.ToInt16(IncomingQuantity);
                if (intIncomingQuantity < 0)
                {
                    ViewBag.quantitycheck = "Quantity must be greater than zero";
                    return View("CheckIn", ViewBag.quantitycheck);
                }
                ViewBag.quantitycheck = "";


                Procurement currentprocurement = _context.Procurements.Include(r => r.Book).FirstOrDefault(r => r.ProcurementID == id);

                if (intIncomingQuantity > currentprocurement.Quantity)
                {
                    intIncomingQuantity = currentprocurement.Quantity;
                }

                Book procurementbook = _context.Books.FirstOrDefault(r => r.BookID == currentprocurement.Book.BookID);

                procurementbook.Inventory += intIncomingQuantity;

                if (intIncomingQuantity == currentprocurement.Quantity)
                {
                    currentprocurement.ProcurementStatus = true;
                }
                else
                {
                    currentprocurement.Quantity -= intIncomingQuantity;
                    currentprocurement.ProcurementStatus = false;
                }

                _context.Update(procurementbook);
                _context.Update(currentprocurement);
                _context.SaveChanges();

                return RedirectToAction("Index");

            }
            catch (FormatException)
            {
                ViewBag.quantitycheck = "Must be an interger";
                return View("CheckIn", ViewBag.quantitycheck);
            }




        }
    }
}
