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




        //GET Report F (Reviews)
        public ActionResult ReviewReport()
        {
            return View("ReturnResultSort");
        }


    }
}