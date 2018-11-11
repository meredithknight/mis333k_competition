using System;
using System.ComponentModel.DataAnnotations;

namespace fa18Team22.Models
{
    public enum ApprovalStatus { Approved, Pending, Denied }
    public class Review
    {
        [Display(Name = "Review ID")]
        public Int32 ReviewID { get; set; }

        [Display(Name = "Rating")]
        public Decimal Rating { get; set; }

        [Display(Name = "Review")]
        public String ReviewText { get; set; }



        //navigational properties
        public Book Book { get; set; }
        //public User Author { get; set; }
        //public User Approver { get; set; }


    }
}