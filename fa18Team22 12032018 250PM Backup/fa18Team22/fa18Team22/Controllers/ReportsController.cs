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