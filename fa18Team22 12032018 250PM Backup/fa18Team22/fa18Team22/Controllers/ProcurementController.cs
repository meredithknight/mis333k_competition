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
        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Procurements.ToListAsync());
        }


        // GET: Procurement/Details/5
        public async Task<IActionResult> Details(int? id)
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


        //GET: Automatic Order
        public IActionResult AutomaticOrder()
        {
            var query = from r in _context.Books select r;
            query = query.Where(r => r.Inventory <= r.ReplenishMinimum);
            List<Book> ProcurementBookSearch = query.ToList();
            return View("AutomaticOrder", ProcurementBookSearch);

        }

        public IActionResult DeleteFromProcurementQuery(int id, List<Book> ProcurementBookSearch)
        {
            ProcurementBookSearch.RemoveAll(r => r.BookID == id);
            return View("AutomaticOrder", ProcurementBookSearch);
        }

        //POST: Automatic Order
        [HttpPost]
        public IActionResult AutomaticOrder(int Quantity, Decimal Cost, PagedList.PagedList<Book> ListofBookOrder) 
        {
            return View("Index");
        }


        // GET: Procurement/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Procurement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
    }
}
