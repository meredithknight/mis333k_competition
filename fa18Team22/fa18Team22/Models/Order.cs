using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace fa18Team22.Models
{
    public class Order
    {
        [Display(Name = "Order ID")]
        public Int32 OrderID { get; set; }

        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:MM.dd.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Order Subtotal")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal OrderSubtotal
        {
            get { return OrderDetails.Sum(od => od.ExtendedPrice); }
        }

        //calculate shipping cost in controller 
        [Display(Name = "Shipping Cost")]
        public Decimal ShippingCost { get; set; }

        [Display(Name = "Order Total")]
        public Decimal OrderTotal
        {
            get
            {
                return OrderSubtotal + ShippingCost;
            }
        }

        public Boolean IsComplete { get; set; }

        //navigational properties
        public AppUser Customer { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public Promo Promo {get; set; }
      
    }
}
