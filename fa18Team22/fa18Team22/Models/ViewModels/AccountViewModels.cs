using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

//TODO: Change this namespace to match your project
namespace fa18Team22.Models
{
   
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        //TODO:  Add any fields that you need for creating a new user

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        //Additional fields go here
        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [Display(Name = "Address")]
        public String Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public String City { get; set; }

        [Required(ErrorMessage = "State is required")]
        [Display(Name = "State (ex: TX)")]
        public String State { get; set; }

        [Required(ErrorMessage = "Zip is required")]
        [StringLength(5, ErrorMessage = "Zip is only 5 numbers")]
        [Display(Name = "Zip")]
        public String Zip { get; set; }

        //NOTE: Here is the property for email
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //NOTE: Here is the property for phone number
        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        //NOTE: Here is the logic for putting in a password
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    } 

    //public class ModifyAccountViewModel
    //{
    //    [Required(ErrorMessage = "First name is required.")]
    //    [Display(Name = "First Name")]
    //    public String FirstName { get; set; }

    //    //Additional fields go here
    //    [Required(ErrorMessage = "Last name is required.")]
    //    [Display(Name = "Last Name")]
    //    public String LastName { get; set; }

    //    [Required(ErrorMessage = "Address is required")]
    //    [Display(Name = "Address")]
    //    public String Address { get; set; }

    //    [Required(ErrorMessage = "City is required")]
    //    [Display(Name = "City")]
    //    public String City { get; set; }

    //    [Required(ErrorMessage = "State is required")]
    //    [Display(Name = "State (ex: TX)")]
    //    public String State { get; set; }

    //    [Required(ErrorMessage = "Zip is required")]
    //    [StringLength(5, ErrorMessage = "Zip is only 5 numbers")]
    //    [Display(Name = "Zip")]
    //    public String Zip { get; set; }

    //    //NOTE: Here is the property for email
    //    [Required]
    //    [EmailAddress]
    //    [Display(Name = "Email")]
    //    public string Email { get; set; }

    //    //NOTE: Here is the property for phone number
    //    [Required(ErrorMessage = "Phone number is required")]
    //    [Phone]
    //    [Display(Name = "Phone Number")]
    //    public string PhoneNumber { get; set; }
         
    //}


    //REMINDER: ONLY USER NAME AND EMAIL SHOWS
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }
        public String Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
        public String PhoneNumber { get; set; }

    }
}
