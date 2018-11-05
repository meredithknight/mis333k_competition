using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace fa18Team22.Models
{
    public class Book
    {
        public Int32 BookID { get; set; }

        [Display(Name = "Title")]
        private String _strTitle;
        public String Title
        {
            get { return _strTitle; }
        }

        [Display(Name = "Author")]
        private String _strAuthor;
        public String Author
        {
            get { return _strAuthor; }
        }

        [Display(Name = "Publish Date")]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [Display(Name = "Book Detail")]
        public String BookDetail { get; set; }

        [Display(Name = "Sales Price")]
        public Decimal SalesPrice { get; set; }

        [Display(Name = "Inventory")]
        public Int32 Inventory { get; set; }

        [Display(Name = "Average Rating")]
        public Decimal AvgRating { get; set; }

        [Display(Name = "Replenish Minimum")]
        public Int32 ReplenishMinimum { get; set; }

<<<<<<< HEAD
        //navigational property
=======
        //navigational properties
>>>>>>> afd9985a6cc2b0bf82c0eced24d03d6e9c393421
        public virtual Genre Genre { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
