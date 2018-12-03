using System;
using System.Linq;
using fa18Team22.DAL;
using fa18Team22.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fa18Team22.Controllers
{
    public enum SortReport { MostRecent, ProfitMarginAsc, ProfitMarginDesc, PriceAsc, PriceDesc, MostPopular }
    public enum ReviewSort {Ascending, Desending}

    public class ReportsController : Controller
    {
        private AppDbContext _db;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public ReportsController(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

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


        public ActionResult AllBooksSold()
        {
            //initialize booksreport viewmodel
            List<AllBooksReportViewModel> allBooksReports = new List<AllBooksReportViewModel>();

            List<OrderDetail> BooksReport = new List<OrderDetail>();
            var query = _db.OrderDetails.Include(o => o.Book).Include(o => o.Order).ThenInclude(o => o.Customer);
            BooksReport = query.ToList();

            foreach(OrderDetail od in BooksReport)
            {
                AllBooksReportViewModel brvm = new AllBooksReportViewModel();

                brvm.Title = od.Book.Title;
                brvm.Quantity = od.Quantity;
                brvm.OrderNumber = od.Order.OrderNumber;
                brvm.CustomerName = od.Order.Customer.FirstName + ' '+ od.Order.Customer.LastName;
                brvm.SellingPrice = od.Price;
                //TODO: YOU NEED to add to books model class weghted
                brvm.WeightedAvgCost = od.Book.BookCost;
                brvm.ProfitMargin = (od.Price - od.Book.BookCost);
                allBooksReports.Add(brvm);
            }
            return View(allBooksReports);

        }

        //GET:Report D (totals)
        //TODO: Build the View for Report D
        public ActionResult ReviewReportD()
        {
            List<Order> SelectedOrders = new List<Order>();
            var query = from o in _db.Orders select o;
            SelectedOrders = query.ToList();

            decimal TotalCost = 0;
            decimal TotalProfit = 0;
            decimal TotalRevenue = 0;

            foreach (Order so in SelectedOrders)
            {
                TotalProfit += so.OrderSubtotal;
                //TODO: PROCUREMENT TOTAL COST CALCULATION
                //TotalCost += so;
                TotalRevenue += (TotalProfit - TotalCost);

            }

            ViewBag.TotalP = TotalProfit;
            ViewBag.TotalC = TotalCost;
            ViewBag.TotalR = TotalRevenue;


            return View();
        }

        //Get Report E (Current Inventory)
        public ActionResult ReviewReportE()
        {
            List<Book> InventoryList = new List<Book>();
            var query = from b in _db.Books select b;
            //InventoryList = query.Include(b => b.Procurement).ToList();
            ViewBag.SelectedRecords = InventoryList.Count();

            return View("ReviewReportE",InventoryList);
        }

        //GET Report F (Reviews)
        public ActionResult ReviewReport()
        {
            return View("ReturnResultSort");
        }


    }
}