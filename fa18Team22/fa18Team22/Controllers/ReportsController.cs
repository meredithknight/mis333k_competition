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
    public enum SortReport { MostRecent, ProfitMarginAsc, ProfitMarginDesc, PriceAsc, PriceDesc, MostPopular, Ascending, Descending }
    public enum ReviewOptions {EmpNum, Accept, Reject }

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


        public ActionResult ReviewReportA(SortReport SelectedSort)
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
                brvm.OrderDate = od.Order.OrderDate;
                allBooksReports.Add(brvm);
            }

            ViewBag.SelectedRecords = allBooksReports.Count();

            switch (SelectedSort)             {
                case SortReport.MostPopular: return View("ReviewReportA", allBooksReports.OrderByDescending(r => r.Quantity));                 case SortReport.MostRecent: return View("ReviewReportA", allBooksReports.OrderByDescending(r => r.OrderNumber));                 case SortReport.PriceAsc: return View("ReviewReportA", allBooksReports.OrderBy(r => r.SellingPrice));                 case SortReport.PriceDesc: return View("ReviewReportA", allBooksReports.OrderByDescending(r => r.SellingPrice));                 case SortReport.ProfitMarginAsc: return View("ReviewReportA", allBooksReports.OrderBy(r => r.ProfitMargin));                 case SortReport.ProfitMarginDesc: return View("ReviewReportA", allBooksReports.OrderByDescending(r => r.ProfitMargin));              }             return View("ReviewReportA", allBooksReports); 

        }

        public IActionResult SortSelectionB()
        {
            return View();
        }

        public ActionResult ReviewReportB(SortReport SelectedSort)
        {
            //initialize booksreport viewmodel
            List<OrderReportVM> allBooksReports = new List<OrderReportVM>();

            List<OrderDetail> OrdersReport = new List<OrderDetail>();
            var query = _db.OrderDetails.Include(o => o.Book).Include(o => o.Order).ThenInclude(o => o.Customer);
            OrdersReport = query.ToList();

            List<Order> allOrders = new List<Order>();
            var orderquery = _db.Orders.Include(o => o.Customer).Include(o => o.OrderDetails).ThenInclude(o => o.Book);
            allOrders = orderquery.ToList();

            foreach (Order order in allOrders)
            {
                OrderReportVM orvm = new OrderReportVM();
                List<string> ListBookTandQ = new List<string>();
                decimal OrderProfit = 0;
                decimal OrderCost = 0;
                string BookTitle;
                int BookQ;
                string TandQ;
                foreach (OrderDetail od in OrdersReport)
                {
                    if (od.Order.OrderID == order.OrderID)
                    {
                        OrderProfit += od.ExtendedPrice;
                        OrderCost += od.Book.BookCost;
                        BookTitle = od.Book.Title;
                        BookQ = od.Quantity;
                        TandQ = BookTitle + " (" + BookQ.ToString() + ") ";
                        ListBookTandQ.Add(TandQ);
                        orvm.CustomerName = od.Order.Customer.FirstName + ' ' + od.Order.Customer.LastName;
                    }

                }

                orvm.Payment = order.Payment;
                orvm.OrderNumber = order.OrderNumber;
                orvm.ProfitMargin = (OrderProfit - OrderCost);
                orvm.BookTandQ = ListBookTandQ;
                orvm.OrderTotal = OrderProfit;
                orvm.OrderCost = OrderCost;

                allBooksReports.Add(orvm);

            }

            ViewBag.SelectedRecords = allBooksReports.Count();

            switch (SelectedSort)
            {
                case SortReport.MostRecent: return View("ReviewReportB", allBooksReports.OrderByDescending(r => r.OrderNumber));
                case SortReport.PriceAsc: return View("ReviewReportB", allBooksReports.OrderBy(r => r.OrderTotal));
                case SortReport.PriceDesc: return View("ReviewReportB", allBooksReports.OrderByDescending(r => r.OrderTotal));
                case SortReport.ProfitMarginAsc: return View("ReviewReportB", allBooksReports.OrderBy(r => r.ProfitMargin));
                case SortReport.ProfitMarginDesc: return View("ReviewReportB", allBooksReports.OrderByDescending(r => r.ProfitMargin));

            }
            return View("ReviewReportB", allBooksReports);


        }

        public IActionResult SortSelectionC()
        {
            return View();
        }

        public async Task<ActionResult> ReviewReportC(SortReport SelectedSort)
        {
            List<CustomerReportVM> customerReportVMs = new List<CustomerReportVM>();

            List<OrderDetail> CustomersReports = new List<OrderDetail>();
            var query = _db.OrderDetails.Include(o => o.Book).Include(o => o.Order).ThenInclude(o => o.Customer);
            CustomersReports = query.ToList();

            List<AppUser> customers = new List<AppUser>();
            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();
            foreach (AppUser user in _userManager.Users.Include(User => User.ReviewsApproved).Include(User => User.ReviewsRejected))
            {
                var cuslist = await _userManager.IsInRoleAsync(user, "Customer") ? members : nonMembers;
                cuslist.Add(user);
            }
            RoleEditModel re = new RoleEditModel();
            re.Members = members;

            foreach (AppUser user in members)
            {
                
                CustomerReportVM crvm = new CustomerReportVM();
                List<string> ListBookTandQ = new List<string>();
                List<string> ListOrderNumbers = new List<string>();
                decimal CustomerProfit = 0;
                decimal CustomerCost = 0;
                string BookTitle;
                int BookQ;
                string TandQ;
                string strOrderNum;

                foreach(OrderDetail od in CustomersReports)
                {
                    if(od.Order.Customer.Id == user.Id)
                    {
                        CustomerProfit += od.ExtendedPrice;
                        CustomerCost += od.Book.BookCost;
                        strOrderNum = "{" + od.Order.OrderNumber.ToString() + "} ";
                        BookTitle = od.Book.Title;
                        BookQ = od.Quantity;
                        TandQ = BookTitle + " (" + BookQ.ToString() + ") ";
                        ListBookTandQ.Add(TandQ);
                        ListOrderNumbers.Add(strOrderNum);
                        crvm.CustomerName = od.Order.Customer.FirstName + ' ' + od.Order.Customer.LastName;
                    }
                }
                if (CustomerCost > 0 && CustomerProfit > 0)
                {
                    crvm.OrderNumbers = ListOrderNumbers;
                    crvm.ProfitMargin = (CustomerProfit - CustomerCost);
                    crvm.BookTandQ = ListBookTandQ;
                    crvm.CustomerTotal = CustomerProfit;
                    crvm.CustomerBooksCost = CustomerCost;

                    customerReportVMs.Add(crvm);
                }
            }

            ViewBag.SelectedRecords = customerReportVMs.Count();

            switch (SelectedSort)
            {
                case SortReport.PriceAsc: return View("ReviewReportC", customerReportVMs.OrderBy(r => r.CustomerTotal));
                case SortReport.PriceDesc: return View("ReviewReportC", customerReportVMs.OrderByDescending(r => r.CustomerTotal));
                case SortReport.ProfitMarginAsc: return View("ReviewReportC", customerReportVMs.OrderBy(r => r.ProfitMargin));
                case SortReport.ProfitMarginDesc: return View("ReviewReportC", customerReportVMs.OrderByDescending(r => r.ProfitMargin));

            }
            return View("ReviewReportC", customerReportVMs);
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
            return View("ReviewReportSort");
        }

        //POST report F (reviews)
        public async Task<ActionResult> DisplayReviewReport(ReviewOptions ReviewOption, SortReport SortBy)
        {
            List<AppUser> employees = new List<AppUser>();
            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();
            foreach (AppUser user in _userManager.Users.Include(User => User.ReviewsApproved).Include(User => User.ReviewsRejected))
            {
                var emplist = await _userManager.IsInRoleAsync(user, "Employee") ? members : nonMembers;
                emplist.Add(user);
            }
            RoleEditModel re = new RoleEditModel();
            re.Members = members;

            foreach(var emps in re.Members)
            {
                employees.Add(emps);
            }


            var empsort = employees.OrderBy(User => User.Email);

            switch (ReviewOption)
            {
                case ReviewOptions.EmpNum:
                    switch(SortBy)
                    {
                        case SortReport.Ascending:
                            empsort = employees.OrderBy(User => User.Email);
                            break;
                        case SortReport.Descending:
                            empsort = employees.OrderByDescending(User => User.Email);
                            break;
                    }
                    break;
                case ReviewOptions.Accept:
                    switch(SortBy)
                    {
                        case SortReport.Ascending:
                            empsort = employees.OrderBy(User => User.NumofApprove);
                            break;
                        case SortReport.Descending:
                            empsort = employees.OrderByDescending(User => User.NumofApprove);
                            break;
                    }
                    break;
                case ReviewOptions.Reject:
                    switch(SortBy)
                    {
                        case SortReport.Ascending:
                            empsort = employees.OrderBy(User => User.NumofRejected);
                            break;
                        case SortReport.Descending:
                            empsort = employees.OrderByDescending(User => User.NumofRejected);
                            break;
                    }
                    break;
                default:
                    break;
            }

            return View("ReviewReport", empsort);  
        }
    }
}