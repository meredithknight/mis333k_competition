using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using fa18Team22.DAL;
using fa18Team22.Models;

namespace fa18Team22.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Orders - list of all previous orders
        public async Task<IActionResult> Index()
        {
            List<Order> Orders = new List<Order>();
            if (User.IsInRole("Customer"))
            {
                //REMINDER: fix this to include only the customer's orders
                //Orders = _context.Orders.Include(c => c.OrderDetails).Where(c => c.Customer.UserName == User.Identity.Name).Where(c => c.IsComplete != false).ToList();
                Orders = _context.Orders.Include(c => c.OrderDetails).Where(c => c.IsComplete != false).ToList();
            }
            else
            {
                Orders = _context.Orders.Include(c => c.OrderDetails).ToList();
            }
            return View(Orders);
            //return View(await _context.Orders.Include(r => r.OrderDetails).ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                                      .Include(m => m.OrderDetails)
                                      .ThenInclude(m => m.Book)
                                      .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        //REMINDER: should this even be possible? -- or should it redirect you to shopping cart?
        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,OrderDate,ShippingCost")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        //SHOULD ONLY BE ABLE TO EDIT CURRENT SHOPPING CART, NOT AN OLD ORDER
        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            //only let them edit if the order is not complete
            //return View(order);
            if (order.IsComplete == false)
            {
                return View(order);
            }
            else
            {
                return NotFound();
                //REMINDER: may want to change this error to say something like 
                // "this order has been placed, you cannot change this order"
            }

        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,OrderDate,ShippingCost")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
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
            return View(order);
        }

        //NO ONE SHOULD BE ABLE TO DELETE ORDERS
        //// GET: Orders/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders
        //        .FirstOrDefaultAsync(m => m.OrderID == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        //// POST: Orders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var order = await _context.Orders.FindAsync(id);
        //    _context.Orders.Remove(order);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }

        //new actions MK added 11/14

        //GET
        public IActionResult ShoppingCart()
        {
            var order = _context.Orders.Include(m => m.OrderDetails);
            //Order order = _context.Orders.Include(m => m.OrderDetails).Where(c => c.IsComplete == false);
            if (order == null)
            {
                return View();
                //REMINDER: return an empty shopping cart
            }
            else //return a view of the current shopping cart
            {
                return View(order);
            }

        }

        //GET
        public IActionResult Checkout(int? id)
        {
            if (id == null)
            {
                return View("Error", new string[] { "You must specify an order to place!" });
            }

            Order ord = _context.Orders.Find(id);

            if (ord == null)
            {
                return View("Error", new string[] { "Order not found!" });
            }

            OrderDetail od = new OrderDetail() { Order = ord };

            //ViewBag.AllProducts = GetAllProducts();
            return View("Checkout", od);
        }

        //GET
        public IActionResult PlacedOrder(int? id)
        {
            if (id == null)
            {
                return View("Error", new string[] { "You must specify an order to place!" });
            }

            Order ord = _context.Orders.Find(id);

            if (ord == null)
            {
                return View("Error", new string[] { "Order not found!" });
            }

            OrderDetail od = new OrderDetail() { Order = ord };

            //ViewBag.AllProducts = GetAllProducts();
            return View("PlaceOrder", od);
        }

    }
}
