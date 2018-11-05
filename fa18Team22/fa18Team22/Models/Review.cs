using System;
namespace fa18Team22.Models
{
    public class Review
    {


        public Int32 ReviewID { get; set; }
        public Decimal Rating { get; set; }
        public String ReviewText { get; set; }
        public enum ApprovalStatus { Approved, Pending, Denied }


        public  Book Book { get; set; }
        public  User Author { get; set; }
        public  User Approver { get; set; }


    }
}
