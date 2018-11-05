using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

=======
using System.ComponentModel.DataAnnotations;
>>>>>>> afd9985a6cc2b0bf82c0eced24d03d6e9c393421
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
        public virtual User Customer { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual Promo Promo {get; set; }
      
    }
}
