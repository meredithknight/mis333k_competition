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
        public List<string> BookTandQ { get; set; }

        public Int32 OrderNumber { get; set; }

        public String CustomerName { get; set; }

        public Decimal OrderTotal { get; set; }

        public Decimal OrderCost { get; set; }
        public Decimal OrderCostAvg { get; set; }

        public Decimal ProfitMargin { get; set; }
        public Decimal ProfitMarginMinusAvg { get; set; }

        public DateTime OrderDate { get; set; }

        public String Payment { get; set; }

        public IEnumerable<AppUser> Customers { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }

    public class CustomerReportVM
    {
        public String CustomerName { get; set; }

        public List<string> BookTandQ { get; set; }

        public List<string> OrderNumbers { get; set; }

        public Decimal CustomerTotal { get; set; }

        public Decimal CustomerBooksCost { get; set; }

        public Decimal ProfitMargin { get; set; }
        
        public IEnumerable<AppUser> Customers { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }

    public class ReportDVM
    {

        public Decimal TR { get; set; }

        public Decimal TC { get; set; }

        public Decimal TP { get; set; }

        public IEnumerable<AppUser> Customers { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }

    public class InventoryReportVM
    {

        public String Title { get; set; }

        public Int32 BooksInInventory { get; set; }

        public Decimal WeightedAvgCost { get; set; }


        public IEnumerable<AppUser> Customers { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}