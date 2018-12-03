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
    public class EditCustomerAccountController
    {
        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;
        private PasswordValidator<AppUser> _passwordValidator;
        private AppDbContext _context;

        public EditCustomerAccountController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signIn)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signIn;

            //user manager only has one password validator
            _passwordValidator = (PasswordValidator<AppUser>)userManager.PasswordValidators.FirstOrDefault();
        }


        //CREATE A CUSTOMER ACCOUNT


        //EDIT A CUSTOMER'S ACCOUNT
            
    }
}
