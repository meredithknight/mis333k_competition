using System;
using System.Linq;
using fa18Team22.DAL;
using fa18Team22.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fa18Team22.Controllers
{
    public class ReportsController : Controller
    {
        private AppDbContext _db;
        public ReportsController(AppDbContext context) { _db = context; }

        // GET: /<controller>/
        public IActionResult ChooseReport()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult AllBooksSold()
        {
            ViewBag.SelectedRecords = _db.Books.Count();
            return View();
        }

        // GET: /<controller>/
        public IActionResult AllCustomers()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult Totals()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult CurrentInventory()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult ApprovedRejectedReviews()
        {
            return View();
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }


    }
}
