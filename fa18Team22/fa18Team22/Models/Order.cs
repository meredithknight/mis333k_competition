using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace fa18Team22.Models
{
    public class Order
    {
        [Display(Name = "Order ID")]
        public Int32 OrderID { get; set; }

        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:MM.dd.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        //navigational properties
        public virtual AppUser Customer { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual Promo Promo {get; set; }
      
    }
}
