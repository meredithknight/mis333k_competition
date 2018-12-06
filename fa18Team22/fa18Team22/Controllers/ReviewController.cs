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
    public class ReviewController : Controller
    {
        private readonly AppDbContext _context;

        public ReviewController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Review
        public IActionResult Index()
        {

            List<Book> books = new List<Book>();
            List<List<OrderDetail>> listorderdetails = new List<List<OrderDetail>>();
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            List<Order> orders = new List<Order>();
            if (User.IsInRole("Customer"))
            {
                //books = _context.Books.Include(c => c.OrderDetails).ThenInclude(c => c.Order).ThenInclude(c => c.Customer).Where(c => c.OrderDetails.Order. == User.Identity.Name).ToList();
                orders = _context.Orders.Include(c => c.OrderDetails).ThenInclude(c => c.Book).Where(c => c.Customer.UserName == User.Identity.Name).Where(c => c.IsComplete).ToList();
                foreach (var or in orders)
                {
                    listorderdetails.Add(or.OrderDetails);
                }
                foreach (var orddetaillist in listorderdetails)
                {
                    foreach(var orddetail in orddetaillist)
                    {
                        orderDetails.Add(orddetail);
                    }
                }
                foreach(var orddlt in orderDetails)
                {
                    books.Add(orddlt.Book);
                }
                List<Book> noduplicatebooks = books.Distinct().ToList();
                return View(noduplicatebooks);


            }
            else
            {
                var query = from r in _context.Reviews.Include(r => r.Author).Include(r => r.Book) select r;
                query = query.Where(r => r.ApprovalStatus == null);
                query = query.Where(r => r.Author != null);
                List<Review> notapprovedlist = query.ToList();
                return View("ManagerEmployeeIndex", notapprovedlist);
            }

        }

        //TODO: 
        // GET: Review/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviewid = 0;
            Book book = _context.Books.Include(r => r.Reviews).Include(r => r.Author).FirstOrDefault(r => r.BookID == id);
            foreach (var bookobject in book.Reviews)
            {
                if (bookobject.Author.UserName == User.Identity.Name)
                {
                    reviewid = bookobject.ReviewID;
                }
            }



            Review review = _context.Reviews.Include(r => r.Author).Include(r => r.Book).FirstOrDefault(r => r.ReviewID == reviewid);

            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Review/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            String userName = User.Identity.Name;

            //check to see if this review has already been created
            AppUser currentuser = _context.Users.FirstOrDefault(r => r.UserName == userName);
            //Book currentbook = _context.Books.FirstOrDefault(r => r.BookID == id);
            var orderquery = from r in _context.OrderDetails.Include(r => r.Book).Include(r => r.Order).ThenInclude(r => r.Customer) select r;
            orderquery = orderquery.Where(r => r.Order.Customer.UserName == currentuser.UserName && r.Order.IsComplete == true);
            List<OrderDetail> currentorddetails = orderquery.ToList();
            List<Book> booksinorder = new List<Book>();
            if (currentorddetails.Count() != 0)
            {
                foreach (OrderDetail orddetail in currentorddetails)
                {
                    booksinorder.Add(orddetail.Book);
                }
            }

            Boolean bookbought = false;
            foreach(Book bk in booksinorder)
            {
                if(bk.BookID == id)
                {
                    bookbought = true;
                }
                else
                {
                    bookbought = false;

                }

            }


            if(bookbought == true )
            {
                var query = from r in _context.Reviews.Include(r => r.Book).Include(r => r.Author) select r;
                query = query.Where(r => r.Book.BookID == id);
                query = query.Where(r => r.Author.UserName == userName);
                List<Review> listreviews = query.ToList();
                if (listreviews.Count == 0)
                {
                    Review review = new Review();
                    Book book = _context.Books.FirstOrDefault(m => m.BookID == id);
                    review.Book = book;
                    _context.Add(review);
                    _context.SaveChangesAsync();


                    ////delete first create to make sure no empty reviews are shown
                    //var querytodelete = from r in _context.Reviews select r;
                    //querytodelete = querytodelete.Where(r => r.Author == null && r.ReviewText == null);
                    //List<Review> reviewstodelete = querytodelete.ToList();
                    //foreach (Review reviewobject in reviewstodelete)
                    //{
                    //    _context.Reviews.Remove(reviewobject);
                    //}


                    return View(review);
                }
                else
                {
                    return View("Error", new string[] { "Review already created. Cannot create more than one review for a book." });

                }

            }
            else
            {
                return View("Error", new string[] { "You have not bought this book yet." });

            }




        }

        // POST: Review/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewID,Rating,ReviewText,ApprovalStatus")] Review review, Int16 Rating, string ReviewText, int id)
        {
            if (ModelState.IsValid)
            {
                String userId = User.Identity.Name;
                AppUser currentuser = _context.Users.FirstOrDefault(u => u.UserName == userId);
                Review newreview = new Review();
                Book book = _context.Books.Include(m => m.Reviews).FirstOrDefault(m => m.BookID == id);
                newreview.Book = book;
                newreview.Author = currentuser;
                newreview.Rating = Rating;
                newreview.ReviewText = ReviewText;
                newreview.ApprovalStatus = null;
                book.Reviews.Add(newreview);
                _context.Update(book);
                _context.Update(newreview);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        
        public IActionResult Approve(int? id)
        {
            String userId = User.Identity.Name;
            AppUser currentuser = _context.Users.Include(r => r.ReviewsApproved).FirstOrDefault(u => u.UserName == userId);
            Review currentreview = _context.Reviews.Include(r => r.Approver).FirstOrDefault(r => r.ReviewID == id);
            currentreview.ApprovalStatus = true;
            currentreview.Approver = currentuser;
            currentuser.ReviewsApproved.Add(currentreview);
            _context.Update(currentuser);
            _context.Update(currentreview);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Reject(int? id)
        {
            String userId = User.Identity.Name;
            AppUser currentuser = _context.Users.Include(r => r.ReviewsRejected).FirstOrDefault(u => u.UserName == userId);
            Review currentreview = _context.Reviews.Include(r => r.Approver).FirstOrDefault(r => r.ReviewID == id);
            currentreview.ApprovalStatus = false;
            currentreview.Rejecter = currentuser;
            currentuser.ReviewsRejected.Add(currentreview);
            _context.Update(currentreview);
            _context.Update(currentuser);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AllReviews()
        {
            var query = from r in _context.Reviews.Include(r => r.Book).Include(r => r.Author).Include(r => r.Approver).Include(r => r.Rejecter) select r;
            query = query.Where(r => r.Author != null);
            List<Review> listofreviews = query.ToList();
            return View("AllReviews", listofreviews);
        }


        // GET: Review/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewID,Rating,ReviewText,ApprovalStatus")] Review review)
        {
            if (id != review.ReviewID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ReviewID))
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
            return View(review);
        }

        // GET: Review/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .FirstOrDefaultAsync(m => m.ReviewID == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewID == id);
        }
    }
}
