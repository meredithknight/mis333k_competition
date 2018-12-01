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
        //[Authorize(Roles = "Manager, Customer")]
        public IActionResult Index()
        {
            List<Order> Orders = new List<Order>();

            if (User.IsInRole("Customer"))
            {
                //REMINDER: fix this to include only the customer's orders
                Orders = _context.Orders.Include(c => c.OrderDetails).Where(c => c.Customer.UserName == User.Identity.Name).Where(c => c.IsComplete).ToList();
                //Orders = _context.Orders.Include(c => c.OrderDetails).Where(c => c.IsComplete != false).ToList();
            }
            else //for employees and managers to see all completed orders
            {
                Orders = _context.Orders.Include(c => c.OrderDetails).Where(c => c.IsComplete).ToList();
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
        //public async Task<IActionResult> Edit(int? id)
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var order = await _context.Orders.FindAsync(id);

            var order =  _context.Orders.Include(c => c.OrderDetails).ThenInclude(c => c.Book).FirstOrDefault(c => c.OrderID == id);

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
            //REMINDER: make it to find the open order for a 
            //var orderList = _context.Orders.Include(m => m.OrderDetails).ThenInclude(m => m.Book).Where(c => c.IsComplete == false).Where(c => c.Customer == User.Identity).ToList();
            Order order = _context.Orders.Include(m => m.OrderDetails).ThenInclude(m => m.Book).Where(c => c.IsComplete == false).Where(c => c.Customer.UserName == User.Identity.Name).FirstOrDefault();


            //see if it's a fuzzy id


            //Order order = _context.Orders.Include(m => m.OrderDetails).Where(c => c.IsComplete == false);
            if (order == null)
            {
                //Order NewOrder = new Order{}; //REMINDER: check for existing order (and create new one if needed) when a book is added to order
                //NewOrder.IsComplete = false;

                return View("EmptyShoppingCart");
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

            //OrderDetail od = new OrderDetail() { Order = ord };

            //ViewBag.AllProducts = GetAllProducts();
            return View("Checkout", ord);
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

            //once order is placed, change "IsComplete" property to true
            ord.IsComplete = true;


            //I don't know what this is
            OrderDetail od = new OrderDetail() { Order = ord };

            //ViewBag.AllProducts = GetAllProducts();
            return View("PlaceOrder", od);
        }


        public IActionResult AddToOrder(int? id) //book id
        {
            //find the book being added to the order
            Book book = _context.Books.Find(id);

            //if the book is out of stock, cannot add to order
            if(book.Inventory == 0)
            {
                return RedirectToAction("Index", "Book");
                //this book is out of stock, return user to error page saying it cannot be ordered.
            }
            //when a user adds a book to an order, do they go to a page to choose how many???? (elif)
            else
            {
                //create a new order detail for the book for the shopping cart order
                OrderDetail od = new OrderDetail { };

                //add values for all other fields for orderDetail

                od.Book = book;
                od.Price = od.Book.SalesPrice;
                od.Quantity = 1; //automatically add 1 book to the order

                //this actually saves all the data just entered, into the actual database
                _context.OrderDetails.Add(od);
                _context.SaveChanges();

                //WORKS UP TO HERE

                //connect to the shopping cart order
                od.Order = _context.Orders.Where(c => c.IsComplete == false).Where(c => c.Customer.UserName == User.Identity.Name).FirstOrDefault();

                //if a shopping cart doesn't exist, 
                if (od.Order == null) //no current shopping cart --> add all the fields that need to be put in to create an order
                {
                    Order newOrder = od.Order;

                    od.Order = new Order { };

                    od.Order.OrderDate = System.DateTime.Today;

                    od.Order.ShippingCost = 3.50m; //because this is the first book being added to order

                    od.Order.IsComplete = false; //makes this the shopping cart

                    //od.Order.OrderNumber = GenerateNextOrderNumber.GetNextOrderNumber(_context);


                    String userId = User.Identity.Name;
                    AppUser user = _context.Users.FirstOrDefault(u => u.UserName == userId);
                    od.Order.Customer = user; //THIS IS THROWING ERROR WITH IDENTITY_INSERT

                    //adds this shopping cart ORDER to the orders table in database
                    _context.Orders.Add(od.Order);
                    _context.SaveChanges();

                }

                //what to change for the order if it does already exist
                else
                {
                    //_context.Orders.Add(od.Order);
                    _context.SaveChanges();

                    Order existingCart = od.Order;

                    od.Order.OrderDate = System.DateTime.Today;
                    existingCart.OrderDetails.Count();
                    //check if there's another book in the order already
                    if (existingCart.OrderDetails.Count() > 1) //there is another order detail connected to the existing open order
                    {
                        od.Order.ShippingCost = 1.50m + od.Order.ShippingCost;
                    }
                    else 
                    {
                        od.Order.ShippingCost = 3.50m; //add 1.50 each additional book if one is already in cart
                    }

                    _context.SaveChanges();

                }





                //Order order = _context.Orders.Find(od.Order.OrderID);



                return RedirectToAction("ShoppingCart", "Orders", new {id = od.Book.BookID });
            }
        }
    }
}
