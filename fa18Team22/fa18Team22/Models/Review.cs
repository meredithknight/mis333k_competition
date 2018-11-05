using System;
namespace Nguyen_Tommy_HW5.Models
{
    public class Review
    {


        public Int32 ReviewID { get; set; }
        public Decimal Rating { get; set; }
        public String ReviewText { get; set; }
        public enum ApprovalStatus { Approved, Pending, Denied }


        public virtual Book Book { get; set; }
        public virtual User Author { get; set; }
        public virtual User Approver { get; set; }


    }
}
