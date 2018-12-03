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
        [Range(1, 5, ErrorMessage = "Betweeen 1 to 5 stars")]
        public Decimal Rating { get; set; }

        [Display(Name = "Review")]
        [StringLength(100, ErrorMessage = "100 characters max")]
        public String ReviewText { get; set; }

        public Boolean? ApprovalStatus { get; set; }



        //navigational properties
        public Book Book { get; set; }
        public AppUser Author { get; set; }
        public AppUser Approver { get; set; }
        public AppUser Rejecter { get; set; }


    }
}