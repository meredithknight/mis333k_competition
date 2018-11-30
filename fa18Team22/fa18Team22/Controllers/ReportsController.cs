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
    public enum SortReport { MostRecent, ProfitMarginAsc, ProfitMarginDesc, PriceAsc, PriceDesc, MostPopular }

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
        public IActionResult SortSelectionA()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult AllBooksSold(SortReport SelectedSort)
        {
            ViewBag.SelectedRecords = _db.Books.Count();
            List<Book> SelectedBooks = new List<Book>();

            var query = from b in _db.Books select b;

            decimal totalcost = 0;
            decimal totalprice = 0;
            decimal avgcost;
            decimal avgprice;
            int countervariable = 0;

            foreach(Book b in SelectedBooks)
            {
                countervariable += countervariable;
                totalcost += b.BookCost;
                totalprice += b.SalesPrice;
            }

            avgcost = (totalcost / countervariable);
            avgprice = (totalprice / countervariable);

            foreach (Book b in SelectedBooks)
            {
                b.AvgBookCost = avgcost;

                //TODO: Should we calculate Avg Sales price here. Intial Reaction no, because in the order details is where coupons and promos will be added and this will just take the latest price
            }

            

            if (SelectedSort == SortReport.MostRecent)
            {
                return View("AllBooksIndex", SelectedBooks.OrderBy(b => b.PublishDate));
            }

            if (SelectedSort == SortReport.ProfitMarginAsc)
            {
                return View("AllBooksIndex", SelectedBooks.OrderBy(b => b.))
            }
        }

        // GET: /<controller>/
        public IActionResult AllBooksIndex()
        {
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
     
    }
}
