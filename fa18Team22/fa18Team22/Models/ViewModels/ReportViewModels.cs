using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace fa18Team22.Models
{
    public class AllBooksReportViewModel
    {
        public string Title { get; set; }

        public int Quantity { get; set; }

        public int OrderNumber { get; set; }

        public string CustomerName { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal WeightedAvgCost { get; set; }

        public decimal ProfitMargin { get; set; }

        public IEnumerable<AppUser> Customers { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }


}