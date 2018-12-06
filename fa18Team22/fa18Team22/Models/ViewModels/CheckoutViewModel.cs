using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace fa18Team22.Models
{
    public class AddDiscountODVM
    {

        public String BookTitle { get; set; }

        public Decimal BookPrice { get; set; }

        public Int32 Quantity { get; set; }

        public Decimal ExtendedPrice { get; set; }

        public Decimal Subtotal { get; set; }

        public Decimal ShippingCost { get; set; }

        public String SavedPromoCode { get; set; }

        public Decimal OrderTotal { get; set; }

        public IEnumerable<AppUser> Customers { get; set; }
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }


}
