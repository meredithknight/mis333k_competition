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

        [Range(1, 1000,
            ErrorMessage = "Must Order a Positive Number of Products.")]
        public Int16 QuantityToOrder { get; set; }


        public Int32 Inventory { get; set; }

        public Int32 InventoryMinimum { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal SellingPrice { get; set; }

        public Decimal AvgRatingProc { get; set; }

        public Decimal Cost { get; set; }

        public String userID { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
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
