﻿using System;
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
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;



//TODO: Change this namespace to match your project
namespace fa18Team22.Controllers
{
    public enum UserStatusEnum { Active, Inactive}
    public enum CCType { Visa, MasterCard, Discover, AmericanExpress }

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
                String username = model.Email;
                AppUser currentUser = _context.Users.FirstOrDefault(r => r.UserName == username);
                //String userId = User.Identity.Name;
                //AppUser currentUser = _context.Users.FirstOrDefault(u => u.UserName == userId);
                if (currentUser.UserStatus == "Inactive")
                {
                    await _signInManager.SignOutAsync();
                    return View("Error", new string[] { "Account is Disabled." });
                }
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
        public async Task<ActionResult> Register(RegisterViewModel model, LoginViewModel LoginModel)
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

                user.UserStatus = "Active";
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //TODO: Add user to desired role
                    //This will not work until you have seeded Identity OR added the "Customer" role 
                    //by navigating to the RoleAdmin controller and manually added the "Customer" role
                
                    await _userManager.AddToRoleAsync(user, "Customer");
                    string emailsubject = "Team 22: New Account";
                    string emailbody = "Congratulations! You just created a new account with Bevo Books!";
                    SendEmail(model.Email, model.FirstName, emailbody, emailsubject);





                    //another example
                    //await _userManager.AddToRoleAsync(user, "Manager");

                    Microsoft.AspNetCore.Identity.SignInResult result1 = await _signInManager.PasswordSignInAsync(LoginModel.Email, LoginModel.Password, LoginModel.RememberMe, lockoutOnFailure: false);
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

        [Authorize]
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                id = User.Identity.Name;
            }


            IndexViewModel ivm = new IndexViewModel();

            //get user info
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
            ivm.CreditCard1 = user.CreditCard1;
            ivm.CreditCard2 = user.CreditCard2;
            ivm.CreditCard3 = user.CreditCard3;

            if (ivm.CreditCard1 != null)
            {
                ViewBag.CreditCard1 = String.Format("{0}{1}", "**** - **** - **** - ", (user.CreditCard1.Substring(user.CreditCard1.Length - 4, 4)));
            }
            else { ViewBag.CreditCard1 = "No credit card on file!"; }

            if (ivm.CreditCard2 != null)
            {
                ViewBag.CreditCard2 = String.Format("{0}{1}", "**** - **** - **** - ", (user.CreditCard2.Substring(user.CreditCard2.Length - 4, 4)));
            }
            else { ViewBag.CreditCard2 = "No credit card on file!"; }

            if (ivm.CreditCard3 != null)
            {
                ViewBag.CreditCard3 = String.Format("{0}{1}", "**** - **** - **** - ", (user.CreditCard3.Substring(user.CreditCard3.Length - 4, 4)));
            }
            else { ViewBag.CreditCard3 = "No credit card on file!"; }

            return View(ivm);
        }




        //GET: /Account/Edit
        [Authorize]
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
        [Authorize]
        public IActionResult ModifyAccount(ModifyAccountViewModel account)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser dbAccount = _context.Users
                        .FirstOrDefault(c => c.Id == account.Id);

                    dbAccount.FirstName = account.FirstName;
                    dbAccount.LastName = account.LastName;
                    dbAccount.Email = account.Email;
                    dbAccount.NormalizedEmail = account.Email.ToUpper();
                    dbAccount.Address = account.Address;
                    dbAccount.City = account.City;
                    dbAccount.State = account.State;
                    dbAccount.Zip = account.Zip;
                    dbAccount.PhoneNumber = account.PhoneNumber;
                    dbAccount.CreditCard1 = account.CreditCard1;
                    dbAccount.CreditCard2 = account.CreditCard2;
                    dbAccount.CreditCard3 = account.CreditCard3;

                    _context.Update(dbAccount);
                    _context.SaveChanges();

                    //edit department/course relationships


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index");
            }
            return View(account);
        }

        //Logic for change password
        // GET: /Account/ChangePassword
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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
                String emailsubject = "Team 22: Password Changed";
                String emailbody = "Your password to Bevo Books has been changed.";
                SendEmail(userLoggedIn.Email, userLoggedIn.FirstName, emailbody, emailsubject);
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


        //CUSTOMER ACCOUNT CONTROLLER /// ////////////////////////////////////////////////////////////
        //Manage Customer accounts (like an index)
        [Authorize(Roles = "Manager, Employee")]
        public async Task<ActionResult> ManageCustomerAccounts()
        {
            //return View(await _context.Books.Include(m => m.Genre).ToListAsync());
            List<AppUser> allCustomers = new List<AppUser>();

            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();
            foreach (AppUser user in _userManager.Users)
            {
                var customerList = await _userManager.IsInRoleAsync(user, "Customer") ? members : nonMembers;
                customerList.Add(user);
            }
            RoleEditModel re = new RoleEditModel();
            re.Members = members;

            foreach (var customer in re.Members)
            {
                allCustomers.Add(customer);
            }

            //return allCustomers;

            return View(allCustomers);
        }

        //GET
        [Authorize(Roles = "Manager, Employee")]
        public ActionResult CreateCustomerAccount()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager, Employee")]
        public async Task<ActionResult> CreateCustomerAccount(RegisterViewModel model, LoginViewModel LoginModel)
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

                    //need to do something to make account active??


                };

                user.UserStatus = "Active";
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //user.UserStatus = "Active";

                    //TODO: Add user to desired role
                    //This will not work until you have seeded Identity OR added the "Customer" role 
                    //by navigating to the RoleAdmin controller and manually added the "Customer" role



                    await _userManager.AddToRoleAsync(user, "Customer");
                    String emailbody = "Thank you for creating a new account with Bevo Books!";
                    String emailsubject = "Team 22: New Account";
                    SendEmail(model.Email, model.FirstName, emailbody, emailsubject);

                    //Do not want to sign this person in
                    //Microsoft.AspNetCore.Identity.SignInResult result1 = await _signInManager.PasswordSignInAsync(LoginModel.Email, LoginModel.Password, LoginModel.RememberMe, lockoutOnFailure: false);

                    return RedirectToAction("ManageCustomerAccounts", "Account"); //this is like the index page
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

        //Edit a selected customer account
        //GET
        //GET: /Account/Edit
        [Authorize(Roles = "Manager, Employee")]
        public ActionResult EditCustomerAccount(string id)
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
        [Authorize(Roles = "Manager, Employee")]
        public IActionResult EditCustomerAccount(ModifyAccountViewModel account)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser dbAccount = _context.Users
                        .FirstOrDefault(c => c.Id == account.Id);

                    dbAccount.FirstName = account.FirstName;
                    dbAccount.LastName = account.LastName;
                    dbAccount.Email = account.Email;
                    dbAccount.NormalizedEmail = account.Email.ToUpper();
                    dbAccount.Address = account.Address;
                    dbAccount.City = account.City;
                    dbAccount.State = account.State;
                    dbAccount.Zip = account.Zip;
                    dbAccount.PhoneNumber = account.PhoneNumber;
                    dbAccount.CreditCard1 = account.CreditCard1;
                    dbAccount.CreditCard2 = account.CreditCard2;
                    dbAccount.CreditCard3 = account.CreditCard3;

                    _context.Update(dbAccount);
                    _context.SaveChanges();

                    //edit department/course relationships


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("ManageCustomerAccounts");
            }
            return View(account);
        }

        //edit a customer or employee user status
        [Authorize(Roles = "Manager, Employee")]
        public ActionResult ChangeCustomerUserStatus(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppUser account = _context.Users.FirstOrDefault(c => c.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            //want to return a whether UserStatus is "Active" or "Inactive" --> done in view

            return View(account);
        }

        //POST: /Account/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager, Employee")]
        public async Task<ActionResult> ChangeCustomerUserStatus(string id, UserStatusEnum SelectedUserStatus)
        {
            AppUser currentUser = _context.Users.FirstOrDefault(c => c.Id == id);

            if (SelectedUserStatus == UserStatusEnum.Active)
            {
                currentUser.UserStatus = "Active";
            }
            else
            {
                currentUser.UserStatus = "Inactive";
            }

            //figure out if this is right
            _context.Update(currentUser);
            _context.SaveChanges();

            
            //repopulate list of all customers
            List<AppUser> allCustomers = new List<AppUser>();

            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();
            foreach (AppUser user in _userManager.Users)
            {
                var customerList = await _userManager.IsInRoleAsync(user, "Customer") ? members : nonMembers;
                customerList.Add(user);
            }
            RoleEditModel re = new RoleEditModel();
            re.Members = members;

            foreach (var customer in re.Members)
            {
                allCustomers.Add(customer);
            }

            //return allCustomers;

            return View("ManageCustomerAccounts", allCustomers);
        }

        [Authorize(Roles="Manager, Employee")]
        public ActionResult ChangeCustomerPassword(string id)
        {
            ChangeOtherPasswordViewModel selectedAccount = new ChangeOtherPasswordViewModel();
            selectedAccount.AccountSelectedID = id;

            return View(selectedAccount);
        }

        //
        // POST: /Account/ChangeCustomerPassword
        [Authorize(Roles = "Manager, Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> ChangeCustomerPassword(ChangeOtherPasswordViewModel model, string id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUser customerSelected = await _userManager.FindByIdAsync(id);
            var result = await _userManager.ChangePasswordAsync(customerSelected, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                //don't want to sign in when password is changed
                //await _signInManager.SignInAsync(customerSelected, isPersistent: false);
                String emailsubject = "Team 22: Password Changed";
                String emailbody = "Your password to Bevo Books has been changed.";
                SendEmail(customerSelected.Email, customerSelected.FirstName, emailbody, emailsubject);
                return RedirectToAction("ManageCustomerAccounts", "Account");
            }
            AddErrors(result);
            return View(model);
        }


        //END OF CUSTOMER CONTROLLER /// /////////////////////////////////////////////////////////////////////

        //START OF EMPLOYEE CONTROLLER /// /////////////////////////////////////////////////////////////////////
        //manage employee accounts (like an index page)
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> ManageEmployeeAccounts()
        {
            //return View(await _context.Books.Include(m => m.Genre).ToListAsync());
            List<AppUser> allEmployees = new List<AppUser>();

            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();
            foreach (AppUser user in _userManager.Users)
            {
                var customerList = await _userManager.IsInRoleAsync(user, "Employee") ? members : nonMembers;
                customerList.Add(user);
            }
            RoleEditModel re = new RoleEditModel();
            re.Members = members;

            foreach (var employee in re.Members)
            {
                allEmployees.Add(employee);
            }

            //return allCustomers;

            return View(allEmployees);
        }

        //GET
        [Authorize(Roles = "Manager")]
        public ActionResult CreateEmployeeAccount()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> CreateEmployeeAccount(RegisterViewModel model, LoginViewModel LoginModel)
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

                    //need to do something to make account active??


                };

                user.UserStatus = "Active";
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //user.UserStatus = "Active";

                    //TODO: Add user to desired role
                    //This will not work until you have seeded Identity OR added the "Customer" role 
                    //by navigating to the RoleAdmin controller and manually added the "Customer" role



                    await _userManager.AddToRoleAsync(user, "Employee");
                    String emailbody = "Congratulations! You are now an employee with Bevo Books!";
                    String emailsubject = "Team 22: New Account";
                    SendEmail(model.Email, model.FirstName, emailbody, emailsubject);

                    //Do not want to sign this person in
                    //Microsoft.AspNetCore.Identity.SignInResult result1 = await _signInManager.PasswordSignInAsync(LoginModel.Email, LoginModel.Password, LoginModel.RememberMe, lockoutOnFailure: false);

                    return RedirectToAction("ManageEmployeeAccounts", "Account"); //this is like the index page
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

        //edit a customer or employee user status
        [Authorize(Roles = "Manager")]
        public ActionResult ChangeEmployeeUserStatus(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppUser account = _context.Users.FirstOrDefault(c => c.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            //want to return a whether UserStatus is "Active" or "Inactive" --> done in view

            return View(account);
        }

        //POST: /Account/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> ChangeEmployeeUserStatus(string id, UserStatusEnum SelectedUserStatus)
        {
            AppUser currentUser = _context.Users.FirstOrDefault(c => c.Id == id);

            if (SelectedUserStatus == UserStatusEnum.Active)
            {
                currentUser.UserStatus = "Active";
            }
            else
            {
                currentUser.UserStatus = "Inactive";
            }

            //figure out if this is right
            _context.Update(currentUser);
            _context.SaveChanges();


            //repopulate list of all customers
            List<AppUser> allEmployees = new List<AppUser>();

            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();
            foreach (AppUser user in _userManager.Users)
            {
                var customerList = await _userManager.IsInRoleAsync(user, "Employee") ? members : nonMembers;
                customerList.Add(user);
            }
            RoleEditModel re = new RoleEditModel();
            re.Members = members;

            foreach (var employee in re.Members)
            {
                allEmployees.Add(employee);
            }

            //return allCustomers;

            return View("ManageEmployeeAccounts", allEmployees);
        }

        //GET: /Account/Edit
        [Authorize(Roles = "Manager")]
        public ActionResult EditEmployeeAccount(string id)
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
        [Authorize(Roles = "Manager")]
        public IActionResult EditEmployeeAccount(ModifyAccountViewModel account)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppUser dbAccount = _context.Users
                        .FirstOrDefault(c => c.Id == account.Id);

                    dbAccount.FirstName = account.FirstName;
                    dbAccount.LastName = account.LastName;
                    dbAccount.Email = account.Email;
                    dbAccount.UserName = account.Email;
                    dbAccount.Address = account.Address;
                    dbAccount.City = account.City;
                    dbAccount.State = account.State;
                    dbAccount.Zip = account.Zip;
                    dbAccount.PhoneNumber = account.PhoneNumber;
                    dbAccount.CreditCard1 = account.CreditCard1;
                    dbAccount.CreditCard2 = account.CreditCard2;
                    dbAccount.CreditCard3 = account.CreditCard3;

                    _context.Update(dbAccount);
                    _context.SaveChanges();

                    //edit department/course relationships


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
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
            return View(account);
        }


        [Authorize(Roles = "Manager")]
        public ActionResult ChangeEmployeePassword(string id)
        {
            ChangeOtherPasswordViewModel selectedAccount = new ChangeOtherPasswordViewModel();
            selectedAccount.AccountSelectedID = id;

            return View(selectedAccount);
        }

        //
        // POST: /Account/ChangeCustomerPassword
        [Authorize(Roles = "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> ChangeEmployeePassword(ChangeOtherPasswordViewModel model, string id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUser employeeSelected = await _userManager.FindByIdAsync(id);
            var result = await _userManager.ChangePasswordAsync(employeeSelected, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                //don't want to sign in when password is changed
                //await _signInManager.SignInAsync(customerSelected, isPersistent: false);
                String emailsubject = "Team 22: Password Changed";
                String emailbody = "Your password to Bevo Books has been changed.";
                SendEmail(employeeSelected.Email, employeeSelected.FirstName, emailbody, emailsubject);
                return RedirectToAction("ManageCustomerAccounts", "Account");
            }
            AddErrors(result);
            return View(model);
        }


        //END OF EMPLOYEE CONTROLLER /// /////////////////////////////////////////////////////////////////////


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
        public static void SendEmail (string ToAddress, string ToName, string emailBody, string emailSubject)
        {
            var fromAddress = new MailAddress("bevobooks@gmail.com", "Bevo Books");
            var toAddress = new MailAddress(ToAddress, "To "+ToName);
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