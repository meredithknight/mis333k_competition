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
    public enum CCReplace { CC1, CC2, CC3 }

    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Orders - list of all previous orders
        [Authorize]
        public IActionResult Index()
        {
            List<Order> Orders = new List<Order>();

            if (User.IsInRole("Customer"))
            {
                //REMINDER: fix this to include only the customer's orders
                Orders = _context.Orders.Include(c => c.OrderDetails).Where(c => c.Customer.UserName == User.Identity.Name).Where(c => c.IsComplete).OrderByDescending(c => c.OrderNumber).ToList();
                //Orders = _context.Orders.Include(c => c.OrderDetails).Where(c => c.IsComplete != false).ToList();
            }
            else //for employees and managers to see all completed orders
            {
                Orders = _context.Orders.Include(c => c.OrderDetails).Where(c => c.IsComplete).OrderByDescending(c => c.OrderNumber).ToList();
            }
            return View(Orders);
            //return View(await _context.Orders.Include(r => r.OrderDetails).ToListAsync());
        }

        // GET: Orders/Details/5
        [Authorize]
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
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    //var order = await _context.Orders.FindAsync(id);

        //    var order =  _context.Orders.Include(c => c.OrderDetails).ThenInclude(c => c.Book).FirstOrDefault(c => c.OrderID == id);

        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    //only let them edit if the order is not complete
        //    //return View(order);
        //    if (order.IsComplete == false)
        //    {
        //        return View(order);
        //    }
        //    else
        //    {
        //        return NotFound();
        //        //REMINDER: may want to change this error to say something like 
        //        // "this order has been placed, you cannot change this order"
        //    }

        //}

        //// POST: Orders/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("OrderID,OrderDate,ShippingCost")] Order order)
        //{
        //    if (id != order.OrderID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(order);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrderExists(order.OrderID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(order);
        //}

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
        [Authorize]
        public IActionResult ShoppingCart()
        {
            //REMINDER: make it to find the open order for a 
            //var orderList = _context.Orders.Include(m => m.OrderDetails).ThenInclude(m => m.Book).Where(c => c.IsComplete == false).Where(c => c.Customer == User.Identity).ToList();
            Order order = _context.Orders.Include(c => c.Promo).Include(m => m.OrderDetails).ThenInclude(m => m.Book).Where(c => c.IsComplete == false).Where(c => c.Customer.UserName == User.Identity.Name).FirstOrDefault();


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

                if (order.OrderDetails.Count() == 0 )
                {
                    return View("EmptyShoppingCart");
                }
                else
                {
                
                    //check for out of stock books --> show error
                    //check for discontinued books
                    foreach(OrderDetail od in order.OrderDetails.ToList())
                    {
                        od.Price = od.Book.SalesPrice;

                        //this actually saves all the data just entered, into the actual database
                        _context.OrderDetails.Update(od);
                        _context.SaveChanges();

                        if (od.Book.Inventory == 0)
                        {
                            //update the order to remove this order detail
                            _context.OrderDetails.Remove(od);
                            _context.SaveChanges();
                            ViewBag.OutOfStock = od.Book.Title + " is currently out of stock. It has been removed from your cart.";
                        }
                        if(od.Book.IsDiscontinued) //if it's true
                        {
                            Order orderWithCustomer = _context.Orders.Include(c => c.Customer).FirstOrDefault(c => c.OrderID == order.OrderID);

                            //send email//send email
                            String bookTitle = od.Book.Title;
                            String bookAuthor = od.Book.Author;
                            AppUser customer = orderWithCustomer.Customer;
                            String emailsubject = "Team 22: Item in Cart is Discontinued";
                            String emailbody = "We apologize for the inconvenience, but the following book you have in your cart has been discontinued." + "\nBook: " + bookTitle + "\nAuthor: " + bookAuthor;

                            OrdersController.SendEmail(customer.Email, customer.FirstName, emailbody, emailsubject);

                            //update the order to remove this order detail
                            _context.OrderDetails.Remove(od);
                            _context.SaveChanges();
                            ViewBag.BookDiscontinued = od.Book.Title + " has been discontinued. It has been removed from your cart";

                        }
                    }

                    //recalculate the shipping costs when they come here???
                    ShippingCosts currentShipCosts = _context.ShippingCosts.FirstOrDefault();

                    //reset shipping cost
                    order.ShippingCost = 0;

                    int count = 0;

                    foreach (OrderDetail od in order.OrderDetails.ToList())
                    {
                        if (count == 0) //if this is the first order detail
                        {
                            if (od.Quantity == 1)
                            {
                                order.ShippingCost = currentShipCosts.FirstBookShipCost;
                            }
                            else //more than 1 book in the orderdetail
                            {
                                order.ShippingCost = currentShipCosts.FirstBookShipCost + ((od.Quantity - 1)*currentShipCosts.AddBookShipCost);
                            }
                        }
                        else //all other orderdetails
                        {
                            order.ShippingCost += currentShipCosts.AddBookShipCost * od.Quantity;
                        }
                        count += 1;
                    }





                    return View(order);
                }

            }

        }

        //GET
        [Authorize]
        public IActionResult Checkout(int? id) //OrderID
        {
            if (id == null)
            {
                return View("Error", new string[] { "You must specify an order to place!" });
            }

            //Order order = _context.Orders.Include(m => m.OrderDetails).ThenInclude(m => m.Book).Where(c => c.IsComplete == false).Where(c => c.Customer.UserName == User.Identity.Name).FirstOrDefault();

            //include promo attached
            Order order = _context.Orders.Include(c=>c.Promo).Include(m => m.OrderDetails).ThenInclude(m => m.Book).Where(c => c.IsComplete == false).Where(c => c.Customer.UserName == User.Identity.Name).FirstOrDefault();

            int odCount = order.OrderDetails.Count();

            if (order == null)
            {
                return View("Error", new string[] { "Order not found!" });
            }
            if (odCount == 0)
            {
                return View("Error", new string[] { "No items in cart!" });
            }


            String userid = User.Identity.Name;

            ViewBag.creditcards = GetAllCreditCards(userid);

            return View("Checkout", order);
        }

        //order
        //[HttpPost]

        //GET
        [Authorize]
        //12/5 broke it --> trying to pass promoCode from view to this controller method
        //public IActionResult PlacedOrder(int? id, Order order) //this is an orderID //add in string for credit card and make nullable to return an error
        public IActionResult PlacedOrder(int? id) //this is an orderID
        {
            if (id == null)
            {
                return View("Error", new string[] { "You must specify an order to place!" });
            }

            //int ordId = givenOrder.OrderID;

            //logic to change shopping cart to completed order
                //add payment card
                //add promo code (if there is one)
                //change IsComplete to True

            //decrease the inventory for books that are ordered
            List<OrderDetail> allorderdetails = new List<OrderDetail>();
            var query = _context.OrderDetails.Include(r => r.Order).ThenInclude(m => m.Customer).Include(m => m.Book);
            allorderdetails = query.ToList();


            //foreach (OrderDetail odd in allorderdetails)
            foreach(OrderDetail odd in allorderdetails)
            {
                if(odd.Order.OrderID == id)
                //if(odd.Order.OrderID == order.OrderID)
                {
                    odd.Book.Inventory -= odd.Quantity;
                    _context.SaveChanges();
                }
            }

            var order = _context.Orders.Include(r => r.Promo).Include(r => r.OrderDetails).ThenInclude(r => r.Book).Include(r => r.Customer).FirstOrDefault(r =>r.OrderID == id);

            //order.Promo = promoCode;

            if (order == null)
            {
                return View("Error", new string[] { "Order not found!" });
            }
            Promo isPromoSaved = order.Promo;

            //once order is placed, change "IsComplete" property to true
            order.IsComplete = true;
            order.OrderDate = System.DateTime.Today;
            order.OrderNumber = GenerateNextOrderNumber.GetNextOrderNumber(_context);



            //does this save all changes made to "order"?????
            _context.Orders.Update(order);
            _context.SaveChanges();

            //send order confirmation email
            //SendEmailConfirmOrder(model.Email, model.FirstName);

            //REMINDER: where we'll update inventory

            //ViewBag.AllProducts = GetAllProducts();

            //get current user 
            var query1 = from r in _context.OrderDetails.Include(r => r.Order).Include(r => r.Book) select r;
            List<OrderDetail> orderdetailsoforder = query1.Where(r => r.Order.OrderID == order.OrderID).ToList(); 

            string userName = User.Identity.Name;
            AppUser currentuser = _context.Users.FirstOrDefault(r => r.UserName == userName);


            String strOrderDetail = "";

            foreach(OrderDetail ord in orderdetailsoforder)
            {
                strOrderDetail += ord.Book.Title + " | " + ord.Quantity + " | $" + ord.Price + " | $" + ord.ExtendedPrice + "\n";
            }






            String emailsubject = "Team 22: New Order";
            String emailbody = "Thank you for your order! Below is your order summary. " + "\n" + 
                "Title         " + "Quantity      " + "Price     " + "Book Total       " + "\n" + 
                strOrderDetail + "\n" +
                "Order Summary:" + "\n" + 
                "Subtotal: $" + order.OrderSubtotal + "\n" + 
                                     "Shipping Cost: $" + order.ShippingCost + "\n" + 
                                     "Total: $" + order.OrderTotal;
            SendEmail(currentuser.Email, currentuser.FirstName, emailbody, emailsubject);


            return View("PlacedOrder", order);
        }

        [Authorize]
        public IActionResult AddToOrder(int? id) //book id
        {
            //find the book being added to the order
            Book book = _context.Books.Find(id);

            book.Inventory = GetBookInventory(book);

            //if the book is out of stock, cannot add to order
            if (book.Inventory == 0)
            {

                return View("BookOutOfStock");
                //this book is out of stock, return user to error page saying it cannot be ordered.
            }
            //when a user adds a book to an order, do they go to a page to choose how many???? (elif)
            else
            {
                String userid = User.Identity.Name;
                AppUser currentuser = _context.Users.FirstOrDefault(r => r.UserName == userid);

                var orderquery = from r in _context.OrderDetails.Include(r => r.Book).Include(r => r.Order).ThenInclude(r => r.Customer) select r;
                orderquery = orderquery.Where(r => r.Order.Customer.UserName == currentuser.UserName && r.Order.IsComplete == false);
                List<OrderDetail> currentorddetails = orderquery.ToList();
                List<Book> booksinorder = new List<Book>();

                if (currentorddetails.Count() != 0)
                {
                    foreach (OrderDetail orddetail in currentorddetails)
                    {
                        booksinorder.Add(orddetail.Book);
                    }

                    foreach (Book bk in booksinorder)
                    {
                        if (bk.Title == book.Title)
                        {
                            ViewBag.CannotReAdd = "You cannot add this book to your order. It is already in your cart.";
                            return RedirectToAction("Details", "Search", new { id = id }); //book id
                        }
                //        else
                //        {
                //            ViewBag.CannotReAdd = "";
                //        }
                    }
                }
                //else
                //{
                //    ViewBag.CannotReAdd = "";
                //}




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
                //od.Order = _context.Orders.Where(c => c.IsComplete == false).Where(c => c.Customer.UserName == User.Identity.Name).FirstOrDefault();
                Order ShoppingCartOrder = _context.Orders.Include(c => c.OrderDetails).Where(c => c.IsComplete == false).Where(c => c.Customer.UserName == User.Identity.Name).FirstOrDefault();

                ShippingCosts currentShipCosts = _context.ShippingCosts.FirstOrDefault();

                //if a shopping cart doesn't exist, 
                if (ShoppingCartOrder == null) //no current shopping cart --> add all the fields that need to be put in to create an order
                {

                    ShoppingCartOrder = new Order { };

                    ShoppingCartOrder.OrderNumber = GenerateNextOrderNumber.GetNextOrderNumber(_context);

                    ShoppingCartOrder.OrderDate = System.DateTime.Today;

                    //ShoppingCartOrder.ShippingCost = 3.50m; //because this is the first book being added to order
                    ShoppingCartOrder.ShippingCost = currentShipCosts.FirstBookShipCost;

                    ShoppingCartOrder.IsComplete = false; //makes this the shopping cart

                    //od.Order.OrderNumber = GenerateNextOrderNumber.GetNextOrderNumber(_context);

                    od.Order = ShoppingCartOrder;
                    //ShoppingCartOrder.OrderDetails.Add(od);

                    //how to add customer to an order
                    String userId = User.Identity.Name;
                    AppUser user = _context.Users.FirstOrDefault(u => u.UserName == userId);
                    ShoppingCartOrder.Customer = user; //THIS IS THROWING ERROR WITH IDENTITY_INSERT

                    //adds this shopping cart ORDER to the orders table in database
                    _context.Orders.Add(ShoppingCartOrder);
                    _context.SaveChanges();

                }

                //what to change for the order if it does already exist
                else
                {
                    //_context.Orders.Add(od.Order);
                    _context.SaveChanges();

                    //Order existingCart = od.Order;

                    ShoppingCartOrder.OrderDate = System.DateTime.Today;

                    //int orderDetailCount = 0;
                    ////the count is not increasing!!!!
                    //foreach(OrderDetail ordDet in existingCart.OrderDetails.ToList())
                    //{
                    //    orderDetailCount = 1 + orderDetailCount;
                    //}
                    //od.Order = ShoppingCartOrder;
                    ShoppingCartOrder.OrderDetails.Add(od);

                    int orderDetailCount = ShoppingCartOrder.OrderDetails.Count();
                    //check if there's another book in the order already
                    //if (existingCart.OrderDetails.Count() > 1) //there is another order detail connected to the existing open order
                    if (orderDetailCount > 1)
                    {
                        //ShoppingCartOrder.ShippingCost = 1.50m + ShoppingCartOrder.ShippingCost;
                        ShoppingCartOrder.ShippingCost = currentShipCosts.AddBookShipCost + ShoppingCartOrder.ShippingCost;
                    }
                    else 
                    {
                        //ShoppingCartOrder.ShippingCost = 3.50m; //add 1.50 each additional book if one is already in cart
                        ShoppingCartOrder.ShippingCost = currentShipCosts.FirstBookShipCost;
                    }

                    _context.SaveChanges();

                }





                //Order order = _context.Orders.Find(od.Order.OrderID);



                return RedirectToAction("ShoppingCart", "Orders", new {id = od.Book.BookID });
            }
        }

        [HttpPost]
        public IActionResult EnterPromo(string promoCode, int orderId) //get the coupon code that the customer enters
        {

            //getting the order that this promo is being applied to
            //Order order = _context.Orders.Include(c => c.OrderDetails).FirstOrDefault(c => c.OrderID == orderId);
            Order order = _context.Orders.Include(c => c.Promo).Include(c => c.OrderDetails).ThenInclude(c=>c.Book).FirstOrDefault(c => c.OrderID == orderId);
            //Order order = _context.Orders.Include(c => c.Promo).Include(c => c.OrderDetails).ThenInclude(c => c.Book).ThenInclude(c => c.Genre).FirstOrDefault(c => c.OrderID == orderId);

            //AddDiscountVM afterDiscountVM = new AddDiscountVM();

            String userid = User.Identity.Name;
            //ViewBag.creditcards = GetAllCreditCards(userid);
            var allOrders = _context.Orders.Include(c => c.Promo).Include(c => c.Customer).Where(c => c.Customer.UserName == userid).ToList();

            List<Promo> promosUsed = new List<Promo>();

            foreach (Order ord in allOrders) //list of all orders connected to a user
            {
                if(ord.Promo != null) //if they used a promo on a previous order
                {
                    promosUsed.Add(ord.Promo); //add promo to a list of all promos used
                }
            }



            if (order.Promo == null) //have not used ANY coupon //I THINK THIS ACTUALLY CHECKS IF THERE'S A PROMO ON THIS ORDER
            {

                var promos = _context.Promos.ToList();
                //REMINDER: figure out error if no promos exist
                foreach (Promo item in promos)
                {
                    if (item.PromoCode == promoCode) //if the promoCode exists
                    {
                        if (item.Status) //if the status is True for enabled
                        {

                            //check if promo has been used on previous order
                            if(promosUsed.Count() != 0)
                            {
                                foreach(Promo promo in promosUsed)
                                {
                                    if(promo.PromoCode == promoCode) //promosUsed is equal to promoCode entered
                                    {
                                        ViewBag.creditcards = GetAllCreditCards(userid);
                                        ViewBag.PromoError = "You have already used this coupon.";
                                        return View("Checkout", order);
                                    }
                                }
                            }


                            if (order.OrderSubtotal > item.MinimumSpend) //if the customer spent enough to use this promo
                            {
                                if (item.ShippingWaiver) //promo applies to shippingCost
                                {
                                    //set shipping cost to 0
                                    order.ShippingCost = 0m;
                                    order.Promo = item;
                                    _context.Orders.Update(order);
                                    _context.SaveChanges();
                                    ViewBag.creditcards = GetAllCreditCards(userid);
                                    return View("Checkout", order);
                                }
                                else //should be a discount coupon
                                {
                                    //apply percentage discount to each individual book price
                                    foreach (OrderDetail od in order.OrderDetails)
                                    {
                                        //od.Price = Math.Round(od.Price * (item.DiscountAmount / 100), 2);
                                        od.Price = od.Price - (od.Price * (item.DiscountAmount / 100));

                                        //_context.OrderDetails.Update(od);
                                        _context.SaveChanges();
                                        //do I need to attach this updated orderDetail to this order??/save it



                                    }
                                    order.Promo = item;

                                    _context.Orders.Update(order);
                                    _context.SaveChanges();
                                    //return RedirectToAction("Checkout", new { id = order.OrderID });
                                    ViewBag.creditcards = GetAllCreditCards(userid);
                                    return View("Checkout", order); //od is not staying connected to order through the pass back to view
                                        //when passing this order, some value is null that goes into calculating the subtotal (od.ExtendedPrice???)
                                }
                            }
                            else //if they didnt meet the minimum spending amount
                            {
                                ViewBag.creditcards = GetAllCreditCards(userid);
                                ViewBag.PromoError = "You did not meet the minimum spending requirement to use this coupon.";
                                return View("Checkout", order);
                            }

                        }
                        else //if coupon is not enabled
                        {
                            ViewBag.creditcards = GetAllCreditCards(userid);
                            ViewBag.PromoError = "This coupon is not available for use at this time.";
                            return View("Checkout", order);

                        }

                    }
                    //else //coupon code doesn't exist
                    //{
                    //    ViewBag.PromoError = "Invalid coupon code.";
                    //    return RedirectToAction("ShoppingCart", order);
                    //}
                }
                //gets to end of list and no promos match
                ViewBag.creditcards = GetAllCreditCards(userid);
                ViewBag.PromoError = "Invalid coupon code.";
                return View("Checkout", order);

            }
            //already used coupon
            ViewBag.creditcards = GetAllCreditCards(userid);
            ViewBag.PromoError = "You have already applied a coupon to this order.";
            return View("Checkout", order);
        }


        [HttpPost]
        public IActionResult PaymentInformation(string CreditCard, string NewCreditCard, int orderId, CCReplace SelectedReplaceCard) //ger CC user enters 
        {

            //getting the order that this CC is being used for
            Order order = _context.Orders.Include(c => c.Promo).Include(c => c.OrderDetails).ThenInclude(c => c.Book).FirstOrDefault(c => c.OrderID == orderId);
            //Order order = _context.Orders.FirstOrDefault(c => c.OrderID == orderId);
            //include OD and Boooks so if there's an error, it still shows all the needed info


            String userid = User.Identity.Name;


            AppUser customer = _context.Users.FirstOrDefault(c => c.UserName == userid);

            //check if user entered both or no payment methods
            if ((CreditCard == "Enter a new credit card" && NewCreditCard == null) || (CreditCard != "Enter a new credit card" && NewCreditCard != null))
            {
                ViewBag.PaymentError = "You must enter one payment type.";
                ViewBag.creditcards = GetAllCreditCards(userid);
                return View("Checkout", order);
            }
            else
            {
                if (CreditCard != "Enter a new credit card")
                {
                    //order.Payment = CreditCard;
                    order.Payment = CreditCard.Substring(CreditCard.Length-4); //only save last 4 digits in payment
                    //order.Payment = null;


                    //probs need to save changes
                    _context.Orders.Update(order);
                    _context.SaveChanges();

                    ViewBag.creditcards = GetAllCreditCards(userid, CreditCard);
                    ViewBag.PaymentError = "";
                    //return View("Checkout", order);
                    return RedirectToAction("PlacedOrder", new { id = orderId });

                }
                else //NewCreditCard != null
                {
                    if (NewCreditCard.Length == 16) //check that CC is
                    {
                        foreach (char num in NewCreditCard)
                        {
                            if (!Char.IsDigit(num))
                            {

                                ViewBag.creditcards = GetAllCreditCards(userid);
                                ViewBag.PaymentError = "You entered an invalid credit card number";
                                return View("Checkout", order);
                            }

                        }

                        //data annotation for payment should catch if the CC# is not valid
                        //order.Payment = NewCreditCard;

                        if(customer.CreditCard1 == null)
                        {
                            customer.CreditCard1 = NewCreditCard;
                        }
                        else if (customer.CreditCard2 == null)
                        {
                            customer.CreditCard2 = NewCreditCard;
                        }
                        else if (customer.CreditCard3 == null)
                        {
                            customer.CreditCard3 = NewCreditCard;
                        }
                        else //either CC3 is null, or we will override
                        {
                            //giving them option to choose which to replace
                            if (SelectedReplaceCard == CCReplace.CC1)
                            {
                                customer.CreditCard1 = NewCreditCard;
                            }
                            else if (SelectedReplaceCard == CCReplace.CC2)
                            {
                                customer.CreditCard2 = NewCreditCard;
                            }
                            else //(SelectedReplaceCard == CCReplace.CC3)
                            {
                                customer.CreditCard3 = NewCreditCard;
                            }

                        }
                        //save changes to the CC being added to the customer
                        //_context.SaveChanges();





                        order.Payment = NewCreditCard.Substring(NewCreditCard.Length - 4); ; //only save last 4 digits in payment

                        //probs need to save changes
                        _context.Orders.Update(order);
                        _context.Users.Update(customer);
                        _context.SaveChanges();

                        ViewBag.creditcards = GetAllCreditCards(userid);
                        ViewBag.PaymentError = "";
                        //return View("Checkout", order);
                        return RedirectToAction("PlacedOrder", new { id = orderId });


                    }
                    else
                    {
                        ViewBag.creditcards = GetAllCreditCards(userid);
                        ViewBag.PaymentError = "You entered an invalid credit card number";
                        return View("Checkout", order);
                    }

                }
           
            }

        }





            public int GetBookInventory(Book BookID)
        {
            int bookInventory = BookID.Inventory;
            return bookInventory;
        }

        private void SendEmailConfirmOrder(string ToAddress, string ToName)
        {
            var fromAddress = new MailAddress("bevobooks@gmail.com", "From Bevo Books");
            var toAddress = new MailAddress(ToAddress, "To " + ToName);
            const string fromPassword = "fa18team22";
            const string subject = "Team 22: Order Confirmed!";
            const string body = "Your Bevo Books order is on the way! Here are some other books you might like:";

            //REMINDER: add book recommendations

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
          }
        }

        private SelectList GetAllCreditCards(string userid)
        {
            AppUser user = _context.Users.FirstOrDefault(u => u.UserName == userid);

            if (user.CreditCard1 != null)
            {
                user.CreditCard1 = String.Format("{0}{1}", "**** - **** - **** - ", (user.CreditCard1.Substring(user.CreditCard1.Length - 4, 4)));
            }
            if (user.CreditCard2 != null)
            {
                user.CreditCard2 = String.Format("{0}{1}", "**** - **** - **** - ", (user.CreditCard2.Substring(user.CreditCard2.Length - 4, 4)));
            }
            if (user.CreditCard3 != null)
            {
                user.CreditCard3 = String.Format("{0}{1}", "**** - **** - **** - ", (user.CreditCard3.Substring(user.CreditCard3.Length - 4, 4)));
            }

            List <String> creditcards = new List<string>();

            creditcards.Add("Enter a new credit card");

            if (user.CreditCard1 != null)
            {
                creditcards.Add(user.CreditCard1);
            }
            if (user.CreditCard2 != null)
            {
                creditcards.Add(user.CreditCard2);
            }
            if (user.CreditCard3 != null)
            {
                creditcards.Add(user.CreditCard3);
            }
            SelectList allCreditCards = new SelectList(creditcards, "CreditCards");
            return allCreditCards;
        }
        private SelectList GetAllCreditCards(string userid, string CreditCard)
        {
            AppUser user = _context.Users.FirstOrDefault(u => u.UserName == userid);

            if (user.CreditCard1 != null)
            {
                user.CreditCard1 = String.Format("{0}{1}", "**** - **** - **** - ", (user.CreditCard1.Substring(user.CreditCard1.Length - 4, 4)));
            }
            if (user.CreditCard2 != null)
            {
                user.CreditCard2 = String.Format("{0}{1}", "**** - **** - **** - ", (user.CreditCard2.Substring(user.CreditCard2.Length - 4, 4)));
            }
            if (user.CreditCard3 != null)
            {
                user.CreditCard3 = String.Format("{0}{1}", "**** - **** - **** - ", (user.CreditCard3.Substring(user.CreditCard3.Length - 4, 4)));
            }

            List<String> creditcards = new List<string>();

            creditcards.Add("Enter a new credit card");

            if (user.CreditCard1 != null)
            {
                creditcards.Add(user.CreditCard1);
            }
            if (user.CreditCard2 != null)
            {
                creditcards.Add(user.CreditCard2);
            }
            if (user.CreditCard3 != null)
            {
                creditcards.Add(user.CreditCard3);
            }

            //get the selected credit card
            string selectedCard = CreditCard;

            SelectList allCreditCards = new SelectList(creditcards, "CreditCards", selectedCard);
            return allCreditCards;
        }


        public static void SendEmail(string ToAddress, string ToName, string emailBody, string emailSubject)
        {
            var fromAddress = new MailAddress("bevobooks@gmail.com", "From Bevo Books");
            var toAddress = new MailAddress(ToAddress, "To " + ToName);
            const string fromPassword = "fa18team22";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = emailSubject,
                Body = emailBody
            })
            {
                smtp.Send(message);
            }

        }



    }
}
