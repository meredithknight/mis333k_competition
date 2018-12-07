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

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal SellingPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal WeightedAvgCost { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
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

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal OrderTotal { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal OrderCost { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal OrderCostAvg { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal ProfitMargin { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
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

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal CustomerBooksCost { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal ProfitMargin { get; set; }
        
        public IEnumerable<AppUser> Customers { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }

    public class ReportDVM
    {
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal TR { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal TC { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
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

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal WeightedAvgCost { get; set; }


        public IEnumerable<AppUser> Customers { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}