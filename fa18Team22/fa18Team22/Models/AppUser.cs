using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace fa18Team22.Models
{
    public class AppUser : IdentityUser

    {
        //[Key]
        //[DataType(DataType.EmailAddress)]
        //[Required(ErrorMessage = "Email address is required")]
        //[Display(Name = "Email Address")]
        //public String EmailAddress { get; set; }

        //[DataType(DataType.PhoneNumber)]
        //[Required(ErrorMessage = "Phone number is required")]
        //[Display(Name = "Phone Number")]
        //public String PhoneNumber { get; set; }


        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
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

        [Display(Name = "User Status")]
        public String UserStatus { get; set; }
        //Active / Inactive

        //[Required(ErrorMessage = "Email Address is required")]
        //[StringLength(15, ErrorMessage = "Max of 15 characters for password")]
        //public String Password { get; set; }

        [StringLength(16, ErrorMessage = "Credit card number must be 16 digits")]
        [DataType(DataType.CreditCard)]
        public String CreditCard1 { get; set; }

        [StringLength(16, ErrorMessage = "Credit card number must be 16 digits")]
        [DataType(DataType.CreditCard)]
        public String CreditCard2 { get; set; }

        [StringLength(16, ErrorMessage = "Credit card number must be 16 digits")]
        [DataType(DataType.CreditCard)]
        public String CreditCard3 { get; set; }

        public Int32? NumofApprove
        {
            get { return ReviewsApproved.Count; }
        }

        public Int32? NumofRejected
        {
            get { return ReviewsRejected.Count; }
        }



        //navigational properties
        public List<Order> Orders { get; set; }
        [InverseProperty("Author")]
        public List<Review> ReviewsWritten { get; set; }
        [InverseProperty("Approver")]
        public List<Review> ReviewsApproved { get; set; }
        [InverseProperty("Rejecter")]
        public List<Review> ReviewsRejected { get; set; }


    }
}
