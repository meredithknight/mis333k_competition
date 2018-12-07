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
using fa18Team22.Utilities;
using System.Net.Mail;
using System.Net;

namespace fa18Team22.Controllers
{

    public class ShippingCostsController : Controller
    {
        private readonly AppDbContext _context;

        public ShippingCostsController(AppDbContext context)
        {
            _context = context;
        }

        //add method for manager changing shipping costs
        [Authorize(Roles = "Manager")]
        public ActionResult ChangeShippingCosts()
        {
            ShippingCosts currentShipCosts = _context.ShippingCosts.LastOrDefault();

            //return View();
            return View(currentShipCosts);
        }

        [Authorize(Roles ="Manager")]
        [HttpPost]
        public ActionResult ChangeShippingCosts(string FirstBookShipCost, string AddBookShipCost)
        {
            if (FirstBookShipCost == null || AddBookShipCost == null)//if the manager doesn't enter a value for both
            {
                ViewBag.InvalidCost = "You must enter values for both shipping costs";
                return View();
            }
            else //check if text entered is a decimal
            {
                Decimal decFirstBookShipCost;
                Decimal decAddBookShipCost;

                ShippingCosts newShipCosts = new ShippingCosts();

                try
                {
                    decFirstBookShipCost = Convert.ToDecimal(FirstBookShipCost);
                    newShipCosts.FirstBookShipCost = decFirstBookShipCost;
                }
                catch
                {
                    ViewBag.InvalidFirstBook = "You must enter a valid decimal for the cost";
                    return View();
                }
                try
                {
                    decAddBookShipCost = Convert.ToDecimal(AddBookShipCost);
                    newShipCosts.AddBookShipCost = decAddBookShipCost;
                }
                catch
                {
                    ViewBag.InvalidAddBook = "You must enter a valid decimal for the cost";
                    return View();
                }

                _context.Update(newShipCosts);
                _context.SaveChanges();
                return View(newShipCosts);

                //add the dec values to the model class properties 
                //ShipFirstBook
                //ShipAddBook

            }
        }
    }
}