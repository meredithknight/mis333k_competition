using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

//TODO: Change this using statement to match your project
using fa18Team22.DAL;
using fa18Team22.Models;
using Microsoft.EntityFrameworkCore;



//TODO: Change this namespace to match your project
namespace fa18Team22.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;
        private PasswordValidator<AppUser> _passwordValidator;
        private AppDbContext _context;

        public AccountController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signIn)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signIn;
           
            //user manager only has one password validator
            _passwordValidator = (PasswordValidator<AppUser>)userManager.PasswordValidators.FirstOrDefault();
        }

        
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated) //user has been redirected here from a page they're not authorized to see
            {
                return View("Error", new string[] { "Access Denied" });
            }
            _signInManager.SignOutAsync(); //this removes any old cookies hanging around
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl ?? "/");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }

       
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //TODO: This is the method where you create a new user
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,

                    //TODO: You will need to add all of the properties for your User model here
                    //Make sure that you have included ALL of the properties and that they match
                    //the model class EXACTLY!!
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    Zip = model.Zip,



                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //TODO: Add user to desired role
                    //This will not work until you have seeded Identity OR added the "Customer" role 
                    //by navigating to the RoleAdmin controller and manually added the "Customer" role
                
                    await _userManager.AddToRoleAsync(user, "Customer");
                    SendEmailNewAccount(model.Email, model.FirstName);


                  


                    //another example
                    //await _userManager.AddToRoleAsync(user, "Manager");


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        //GET: Account/Index
        public ActionResult Index()
        {
            IndexViewModel ivm = new IndexViewModel();

            //get user info
            String id = User.Identity.Name;
            AppUser user = _context.Users.FirstOrDefault(u => u.UserName == id);

            //populate the view model
            ivm.Email = user.Email;
            ivm.HasPassword = true;
            ivm.Id = user.Id;
            ivm.UserName = user.UserName;
            ivm.FirstName = user.FirstName;
            ivm.LastName = user.LastName;
            ivm.Address = user.Address;
            ivm.City = user.City;
            ivm.State = user.State;
            ivm.Zip = user.Zip;
            ivm.PhoneNumber = user.PhoneNumber;


            return View(ivm);
        }

        //GET: /Account/Edit
        public ActionResult ModifyAccount(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = _context.Users.FirstOrDefault(c => c.Id == id);
            if (account == null)
            {
                return NotFound();
            }
            ModifyAccountViewModel mvm = new ModifyAccountViewModel();
            mvm.Email = account.Email;
            mvm.FirstName = account.FirstName;
            mvm.LastName = account.LastName;
            mvm.Address = account.Address;
            mvm.City = account.City;
            mvm.State = account.State;
            mvm.Zip = account.Zip;
            mvm.PhoneNumber = account.PhoneNumber;
            mvm.CreditCard1 = account.CreditCard1;
            mvm.CreditCard2 = account.CreditCard2;
            mvm.CreditCard3 = account.CreditCard3;
            return View(mvm);
        }

       

        //POST: /Account/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ModifyAccount(AppUser user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser dbAccount = _context.Users
                        .FirstOrDefault(c => c.Id == user.Id);

                    dbAccount.FirstName = user.FirstName;
                    dbAccount.LastName = user.LastName;
                    dbAccount.Email = user.Email;
                    dbAccount.UserName = user.Email;
                    dbAccount.Address = user.Address;
                    dbAccount.City = user.City;
                    dbAccount.State = user.State;
                    dbAccount.Zip = user.Zip;
                    dbAccount.PhoneNumber = user.PhoneNumber;
                    dbAccount.CreditCard1 = user.CreditCard1;
                    dbAccount.CreditCard2 = user.CreditCard2;
                    dbAccount.CreditCard3 = user.CreditCard3;

                    _context.Update(dbAccount);
                    _context.SaveChanges();

                    //edit department/course relationships


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(user.Id))
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
            return View(user);
        }



        //Logic for change password
        // GET: /Account/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _userManager.ChangePasswordAsync(userLoggedIn, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(userLoggedIn, isPersistent: false); 
                return RedirectToAction("Index", "Home");
            }
            AddErrors(result);
            return View(model);
        }

        //GET:/Account/AccessDenied
        public ActionResult AccessDenied(String ReturnURL)
        {
            return View("Error", new string[] { "Access is denied" });
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
           

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        private bool AccountExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        private void SendEmailNewAccount (string ToAddress, string ToName)
        {
            var fromAddress = new MailAddress("bevobooks@gmail.com", "From Bevo Books");
            var toAddress = new MailAddress(ToAddress, "To "+ToName);
            const string fromPassword = "fa18team22";
            const string subject = "Bevo Books New Account";
            const string body = "Welcome to Bevo Books! You just created a new account!";

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
    }
}