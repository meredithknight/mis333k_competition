using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace fa18Team22.Models
{
    public class SearchVM
    {

        [Display(Name = "Book ID")]
        public Int32 BookID { get; set; }

        [Display(Name = "Title")]
        public String Title { get; set; }

        [Display(Name = "Author")]
        public String Author { get; set; }

        [Display(Name = "Unique Number")]
        public Int32 UniqueNumber { get; set; }

        [Display(Name = "Description")]
        public String BookDetail { get; set; }

        [Display(Name = "Average Rating")]
        public Decimal AvgRating { get; set; }

        [Display(Name = "Price")]
        public Decimal SalesPrice { get; set; }

        [Display(Name = "In Stock?")]
        public Boolean InStock { get; set; }

        [Display(Name = "Discontinued?")]
        public Boolean IsDiscontinued { get; set; }

        [Display(Name = "Total Times Ordered")]
        public Int32 QuantityOrdered { get; set; }

        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        public IEnumerable<AppUser> Customers { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
