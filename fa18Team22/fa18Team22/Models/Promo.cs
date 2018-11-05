using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace fa18Team22.Models
{
    public class Promo
    {
    	public Int32 PromoID { get; set; }

    	[Display(Name = "Promo Code")]
    	public String PromoCode { get; set; }

    	[Display(Name = "Discount Amount")]
    	public Decimal DiscountAmount{ get; set; }

        [Display(Name = "Shipping Waived")]
        public Boolean ShippingWaiver{ get; set; }        


    	//navigational properties
    	public List<Order> Orders { get; set; }

    }
}