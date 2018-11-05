using System;
namespace Nguyen_Tommy_HW5.Models
{
    public class Order
    {
        public Int32 OrderID { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual User Customer { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual Promo Promo {get; set; }
      
    }
}
