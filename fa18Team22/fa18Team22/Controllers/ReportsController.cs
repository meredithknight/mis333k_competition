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
        [Authorize(Roles = "Manager, Employee")]
        public IActionResult ChooseReport()
        {
            return View();
        }

        // GET: /<controller>/
        [Authorize(Roles = "Manager, Employee")]
        public IActionResult SortSelectionA()
        {
            return View();
        }

        [Authorize(Roles = "Manager, Employee")]
        public ActionResult ReviewReportA(SortReport SelectedSort, String ProfitMin, String ProfitMax, String PriceMin, String PriceMax)
        {
            //initialize booksreport viewmodel
            List<AllBooksReportViewModel> allBooksReports = new List<AllBooksReportViewModel>();

            List<OrderDetail> BooksReport = new List<OrderDetail>();             var query = from r in _db.OrderDetails select r;              BooksReport = query.ToList();              if(!string.IsNullOrEmpty(ProfitMin))             {                 decimal decProfitMin;                  try                 {                     decProfitMin = Convert.ToDecimal(ProfitMin);                 }                 catch                 {                     //adding error message for viewbag                     @ViewBag.Message = ProfitMin + "is not a valid number. Please try again.";                  }                 decProfitMin = Convert.ToDecimal(ProfitMin);                 //query = query.Where(r => r.Price >= decProfitMin);                 query = query.Where(r => (r.Book.AvgSalesPrice - r.Book.AvgBookCost) >= decProfitMin);             }              if (!string.IsNullOrEmpty(ProfitMax))             {                 decimal decPMax;                  try                 {                     decPMax = Convert.ToDecimal(ProfitMax);                 }                 catch                 {                     //adding error message for viewbag                     @ViewBag.Message = ProfitMin + "is not a valid number. Please try again.";                  }                 //query = query.Where(r => r.Price >= decPMax);                 decPMax = Convert.ToDecimal(ProfitMax);                 query = query.Where(r => (r.Book.AvgSalesPrice - r.Book.AvgBookCost) <= decPMax);              }              if (!string.IsNullOrEmpty(PriceMin))             {                 decimal decProfitMin;                  try                 {                     decProfitMin = Convert.ToDecimal(PriceMin);                 }                 catch                 {                     //adding error message for viewbag                     @ViewBag.Message = ProfitMin + "is not a valid number. Please try again.";                  }                 //query = query.Where(r => r.Price >= decProfitMin);                 decProfitMin = Convert.ToDecimal(PriceMin);                 query = query.Where(r => r.Price >= decProfitMin);              }              if (!string.IsNullOrEmpty(PriceMax))             {                 decimal decPMax;                  try                 {                     decPMax = Convert.ToDecimal(PriceMax);                 }                 catch                 {                     //adding error message for viewbag                     @ViewBag.Message = ProfitMin + "is not a valid number. Please try again.";                  }                 //query = query.Where(r => r.Price >= decPMax);                 decPMax = Convert.ToDecimal(PriceMax);                 query = query.Where(r => r.Price <= decPMax);              } 
            BooksReport = query.Include(z => z.Book).ThenInclude(z => z.Procurements).Include(z => z.Order).ThenInclude(z => z.Customer).ToList();

            foreach (OrderDetail od in BooksReport)
            {
                AllBooksReportViewModel brvm = new AllBooksReportViewModel();

                brvm.Title = od.Book.Title;
                brvm.Quantity = od.Quantity;
                brvm.OrderNumber = od.Order.OrderNumber;
                brvm.CustomerName = od.Order.Customer.FirstName + ' '+ od.Order.Customer.LastName;
                brvm.SellingPrice = od.Price;
                //TODO: YOU NEED to add to books model class weghted
                brvm.WeightedAvgCost = (decimal)od.Book.AvgBookCost;
                brvm.ProfitMargin = ((decimal)(brvm.SellingPrice - od.Book.AvgBookCost));
                brvm.OrderDate = od.Order.OrderDate;
                allBooksReports.Add(brvm);
            }

            ViewBag.SelectedRecords = allBooksReports.Count();

            switch (SelectedSort)             {
                case SortReport.MostPopular: return View("ReviewReportA", allBooksReports.OrderByDescending(r => r.Quantity));                 case SortReport.MostRecent: return View("ReviewReportA", allBooksReports.OrderByDescending(r => r.OrderNumber));                 case SortReport.PriceAsc: return View("ReviewReportA", allBooksReports.OrderBy(r => r.SellingPrice));                 case SortReport.PriceDesc: return View("ReviewReportA", allBooksReports.OrderByDescending(r => r.SellingPrice));                 case SortReport.ProfitMarginAsc: return View("ReviewReportA", allBooksReports.OrderBy(r => r.ProfitMargin));                 case SortReport.ProfitMarginDesc: return View("ReviewReportA", allBooksReports.OrderByDescending(r => r.ProfitMargin));              }             return View("ReviewReportA", allBooksReports); 

        }

        [Authorize(Roles = "Manager, Employee")]
        public IActionResult SortSelectionB()
        {
            return View();
        }

        [Authorize(Roles = "Manager, Employee")]
        public ActionResult ReviewReportB(SortReport SelectedSort, String ProfitMin, String ProfitMax, String PriceMin, String PriceMax)
        {

            //initialize booksreport viewmodel
            List<OrderReportVM> allBooksReports = new List<OrderReportVM>();

            List<OrderDetail> OrdersReport = new List<OrderDetail>();
            var query = from r in _db.OrderDetails select r;


            if (!string.IsNullOrEmpty(ProfitMin))
            {
                decimal decProfitMin;

                try
                {
                    decProfitMin = Convert.ToDecimal(ProfitMin);
                }
                catch
                {
                    //adding error message for viewbag
                    @ViewBag.Message = ProfitMin + "is not a valid number. Please try again.";

                }
                decProfitMin = Convert.ToDecimal(ProfitMin);
                //query = query.Where(r => r.Price >= decProfitMin);
                query = query.Where(r => (r.Book.AvgSalesPrice - r.Book.AvgBookCost) >= decProfitMin);
            }

            if (!string.IsNullOrEmpty(ProfitMax))
            {
                decimal decPMax;

                try
                {
                    decPMax = Convert.ToDecimal(ProfitMax);
                }
                catch
                {
                    //adding error message for viewbag
                    @ViewBag.Message = ProfitMin + "is not a valid number. Please try again.";

                }
                //query = query.Where(r => r.Price >= decPMax);
                decPMax = Convert.ToDecimal(ProfitMax);
                query = query.Where(r => (r.Book.AvgSalesPrice - r.Book.AvgBookCost) <= decPMax);

            }

            if (!string.IsNullOrEmpty(PriceMin))
            {
                decimal decProfitMin;

                try
                {
                    decProfitMin = Convert.ToDecimal(PriceMin);
                }
                catch
                {
                    //adding error message for viewbag
                    @ViewBag.Message = ProfitMin + "is not a valid number. Please try again.";

                }
                //query = query.Where(r => r.Price >= decProfitMin);
                decProfitMin = Convert.ToDecimal(PriceMin);
                query = query.Where(r => r.Price >= decProfitMin);

            }

            if (!string.IsNullOrEmpty(PriceMax))
            {
                decimal decPMax;

                try
                {
                    decPMax = Convert.ToDecimal(PriceMax);
                }
                catch
                {
                    //adding error message for viewbag
                    @ViewBag.Message = ProfitMin + "is not a valid number. Please try again.";

                }
                //query = query.Where(r => r.Price >= decPMax);
                decPMax = Convert.ToDecimal(PriceMax);
                query = query.Where(r => r.Price <= decPMax);

            }
            //query = query.Include(o => o.Book).ThenInclude(o => o.Procurements).Include(o => o.Order).ThenInclude(o => o.Customer);
            OrdersReport = query.ToList();

            List<Order> allOrders = new List<Order>();
            var orderquery = _db.Orders.Include(o => o.Customer).Include(o => o.OrderDetails).ThenInclude(o => o.Book).ThenInclude(o => o.Procurements);
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
                        OrderCost += (Decimal)od.Book.AvgBookCost;
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
                if (orvm.OrderTotal != 0)
                {
                    allBooksReports.Add(orvm);
                }

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

        [Authorize(Roles = "Manager, Employee")]
        public IActionResult SortSelectionC()
        {
            return View();
        }

        [Authorize(Roles = "Manager, Employee")]
        public async Task<ActionResult> ReviewReportC(SortReport SelectedSort)
        {
            List<CustomerReportVM> customerReportVMs = new List<CustomerReportVM>();

            List<OrderDetail> CustomersReports = new List<OrderDetail>();
            var query = _db.OrderDetails.Include(o => o.Book).ThenInclude(o => o.Procurements).Include(o => o.Order).ThenInclude(o => o.Customer);
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

                foreach (OrderDetail od in CustomersReports)
                {
                    if (od.Order.Customer.Id == user.Id)
                    {
                        CustomerProfit += od.ExtendedPrice;
                        CustomerCost += (Decimal)od.Book.AvgBookCost;
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
        [Authorize(Roles = "Manager, Employee")]
        public ActionResult ReviewReportD()
        {
            List<OrderDetail> SelectedOrders = new List<OrderDetail>();
            var query = from o in _db.OrderDetails select o;
            query = query.Include(o => o.Order).Include(o => o.Book).ThenInclude(o => o.Procurements);
            SelectedOrders = query.ToList();

            List<Order> allOrders = new List<Order>();
            var oquery = from o in _db.Orders select o;
            oquery = oquery.Include(o => o.OrderDetails).ThenInclude(o => o.Book).ThenInclude(o => o.Procurements);
            allOrders = oquery.ToList();

            List<Procurement> ProcurementsReport = new List<Procurement>();
            var pquery = _db.Procurements.Include(o => o.Book).ThenInclude(o => o.OrderDetails).ThenInclude(o => o.Order).ThenInclude(o => o.Customer);
            ProcurementsReport = pquery.ToList();

            decimal TotalCost = 0;
            decimal TotalProfit = 0;
            decimal TotalRevenue = 0;
            int TotalQuant = 0;

            foreach (Order order in allOrders)
            {
                foreach (OrderDetail od in order.OrderDetails)
                    {
                        TotalRevenue += (od.Quantity*od.Price);
                        TotalQuant += od.Quantity;
                        TotalCost += ((Decimal)od.Book.AvgBookCost*od.Quantity);
                        
                    }
            }
            TotalProfit = TotalRevenue - TotalCost;



            ReportDVM rdvm = new ReportDVM();
            rdvm.TC = TotalCost;
            rdvm.TP = TotalProfit;
            rdvm.TR = TotalRevenue;

            return View(rdvm);
        }

        //Get Report E (Current Inventory)
        [Authorize(Roles = "Manager, Employee")]
        public ActionResult ReviewReportE()
        {
            List<Book> InventoryList = new List<Book>();
            var query = from b in _db.Books select b;
            query = query.Where(b => b.Inventory > 0);
            query = query.Include(b => b.Procurements);
            InventoryList = query.ToList();
            ViewBag.SelectedRecords = InventoryList.Count();


            List<InventoryReportVM> inventoryReportVMs = new List<InventoryReportVM>();
            decimal TotalValueSold = 0;
            decimal TotalValueCost = 0;
            foreach(Book book in InventoryList)
            {
                InventoryReportVM irvm = new InventoryReportVM();
                irvm.Title = book.Title;
                irvm.BooksInInventory = book.Inventory;
                irvm.WeightedAvgCost = (decimal)book.AvgBookCost;
                TotalValueSold += (book.Inventory * (Decimal)book.AvgSalesPrice);
                TotalValueCost += (book.Inventory * (Decimal)book.AvgBookCost);
                inventoryReportVMs.Add(irvm);
            }

            ViewBag.TotalCost = TotalValueCost;
            ViewBag.TotalValue = TotalValueSold;

            return View("ReviewReportE",inventoryReportVMs);
        }

        //GET Report F (Reviews)
        [Authorize(Roles = "Manager, Employee")]
        public ActionResult ReviewReport()
        {
            ViewBag.ConvertError = "";
            return View("ReviewReportSort");
        }

        //POST report F (reviews)
        [Authorize(Roles = "Manager, Employee")]
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
                foreach (AppUser user in _userManager.Users.Include(User => User.ReviewsApproved).Include(User => User.ReviewsRejected))
                {
                    var emplist = await _userManager.IsInRoleAsync(user, "Manager") ? members : nonMembers;
                    emplist.Add(user);
                }
                RoleEditModel re = new RoleEditModel();
                re.Members = members;

                foreach (var emps in re.Members)
                {
                    employees.Add(emps);
                }


                var empsort = employees.OrderBy(User => User.Email);

            //if (!string.IsNullOrEmpty(ReviewsMin))
            //{
            //    decimal decRevMin;

            //    try
            //    {
            //        decRevMin = Convert.ToDecimal(ReviewsMin);
            //    }
            //    catch
            //    {
            //        //adding error message for viewbag
            //        @ViewBag.Message = ProfitMin + "is not a valid number. Please try again.";

            //    }
            //    //query = query.Where(r => r.Price >= decProfitMin);
            //    decRevMin = Convert.ToDecimal(PriceMin);
            //    empsort = empsort.Where(r => r.Price >= decRevMin);

            //}

            switch (ReviewOption)
            {
                case ReviewOptions.EmpNum:
                    switch (SortBy)
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
                    switch (SortBy)
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
                    switch (SortBy)
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