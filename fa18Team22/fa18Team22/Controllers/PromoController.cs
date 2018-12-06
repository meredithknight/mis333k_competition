using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fa18Team22.DAL;
using fa18Team22.Models;
using Microsoft.AspNetCore.Authorization;

namespace fa18Team22.Controllers
{
    public class PromoController : Controller
    {
        private readonly AppDbContext _context;

        public PromoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Promo
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Promos.ToListAsync());
        }

        // GET: Promo/Details/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promo = await _context.Promos
                .FirstOrDefaultAsync(m => m.PromoID == id);
            if (promo == null)
            {
                return NotFound();
            }

            return View(promo);
        }

        // GET: Promo/Create
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Promo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([Bind("PromoID,PromoCode,DiscountAmount,ShippingWaiver,Status,MinimumSpend")] Promo promo, CouponType SelectedPromo)
        {
            if (ModelState.IsValid)
            {
                switch (SelectedPromo)                 {                     case CouponType.Percent:                         promo.ShippingWaiver = false;                         break;                     case CouponType.FreeShipping:                         promo.ShippingWaiver = true;                         break;                   }                 promo.Status = true;

                foreach (Promo pro in _context.Promos)
                {
                    if(pro.PromoCode == promo.PromoCode)
                    {
                        ViewBag.PromoError = "Promo name already taken, please choose another";
                        return View("Create");
                    }
                }

                ViewBag.PromoError = "";

 
                _context.Add(promo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(promo);
        }

        // GET: Promo/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promo = _context.Promos.FirstOrDefault(c => c.PromoID == id);
            if (promo == null)
            {
                return NotFound();
            }
            return View(promo);
        }

        // POST: Promo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int id, Promo promo)
        {
            if (id != promo.PromoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromoExists(promo.PromoID))
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
            return View(promo);
        }

        //// GET: Promo/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var promo = await _context.Promos
        //        .FirstOrDefaultAsync(m => m.PromoID == id);
        //    if (promo == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(promo);
        //}

        //// POST: Promo/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var promo = await _context.Promos.FindAsync(id);
        //    _context.Promos.Remove(promo);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool PromoExists(int id)
        {
            return _context.Promos.Any(e => e.PromoID == id);
        }
    }
}
