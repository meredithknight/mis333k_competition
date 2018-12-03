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


namespace fa18Team22.Controllers
{
    public class EmployeeAccountController
    {
        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;
        private PasswordValidator<AppUser> _passwordValidator;
        private AppDbContext _context;

        public EmployeeAccountController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signIn)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signIn;

            //user manager only has one password validator
            _passwordValidator = (PasswordValidator<AppUser>)userManager.PasswordValidators.FirstOrDefault();
        }

        // GET: /Account/HireEmployee --> adding a new employee
        [AllowAnonymous]
        public ActionResult HireEmployee()
        {
            //return View();
        }


        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous] //CHANGE TO ONLY ALLOW MANAGERS LATER
        [ValidateAntiForgeryToken]
        //TODO: This is the method where you create a new user
        public async Task<ActionResult> HireEmployee(RegisterViewModel model, LoginViewModel LoginModel)
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

                    UserStatus = model.UserStatus



                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //TODO: Add user to desired role
                    //This will not work until you have seeded Identity OR added the "Customer" role 
                    //by navigating to the RoleAdmin controller and manually added the "Customer" role

                    await _userManager.AddToRoleAsync(user, "Employee");
                    SendEmailNewAccount(model.Email, model.FirstName);





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

    }




}
