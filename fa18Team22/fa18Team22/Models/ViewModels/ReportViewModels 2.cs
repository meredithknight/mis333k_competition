using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace fa18Team22.Models
{
    public class AllBooksReportViewModel
    {
        public String Title { get; set; }

        public Int32 Quantity { get; set; }

        public Int32 OrderNumber { get; set; }

        public String CustomerName { get; set; }

        public Decimal SellingPrice { get; set; }

        public Decimal WeightedAvgCost { get; set; }

        public Decimal ProfitMargin { get; set; }

        public DateTime OrderDate { get; set; }

        public IEnumerable<AppUser> Customers { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }

    public class OrderReportVM
    {
        public String Title { get; set; }

        public Int32 Quantity { get; set; }

        public Int32 OrderNumber { get; set; }

        public String CustomerName { get; set; }

        public Decimal SellingPrice { get; set; }

        public Decimal WeightedAvgCost { get; set; }

        public Decimal ProfitMargin { get; set; }

        public DateTime OrderDate { get; set; }

        public IEnumerable<AppUser> Customers { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}