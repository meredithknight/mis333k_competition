using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace fa18Team22.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
