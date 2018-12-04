using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace fa18Team22.Models
{
    public class Book
    {
        [Display(Name = "Book ID")]
        [Key]
        public Int32 BookID { get; set; }

        [Display(Name = "Unique ID")]
        public Int32 UniqueID { get; set; }

        [Display(Name = "Title")]
        public String Title { get; set; }

        [Display(Name = "Author")]
        public String Author { get; set; }

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

        [Display(Name = "Book Cost")]
        public Decimal BookCost { get; set; }

        [Display(Name = "Average Sales Price")]
        public Decimal AvgSalesPrice
        {
            get { return OrderDetails.Average(od => od.ExtendedPrice); }
        }

        //navigational properties
        public List<Procurement> Procurements { get; set; }
        public Genre Genre { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Review> Reviews { get; set; }
        //public List<Order> BooksInRecommend { get; set; }
    }
}
