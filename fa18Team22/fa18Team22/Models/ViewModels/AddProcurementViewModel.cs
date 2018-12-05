using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace fa18Team22.Models
{
    public class AddProcurementVM
    {
        public Int32 BookID { get; set; }

        public String Title { get; set; }

        public String Author { get; set; }

        public String BookDetail { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Display(Name = "Quantity")]
        [Range(1, 10000000000, ErrorMessage = "Number of products cannot be negative")]
        public Int16 QuantityToOrder { get; set; }


        public Int32 Inventory { get; set; }

        public Int32 InventoryMinimum { get; set; }

        public Decimal SellingPrice { get; set; }

        public Decimal AvgRatingProc { get; set; }

        public Decimal Cost { get; set; }

        public String userID { get; set; }

        public Decimal ProfitMargin { get; set; }

        public Boolean IncludeInProcurement { get; set; }

        public DateTime ProcurementDate { get; set; }

        public DateTime PublishDate { get; set; }

        public IEnumerable<AppUser> Customers { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
