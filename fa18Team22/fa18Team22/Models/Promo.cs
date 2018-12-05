using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace fa18Team22.Models
{
    public enum CouponType { FreeShipping, Percent }
    public class Promo
    {
    	public Int32 PromoID { get; set; }

    	[Display(Name = "Promo Code")]
        [StringLength(20, ErrorMessage = "20 characters max")]
        public String PromoCode { get; set; }

        [Display(Name = "Discount Amount")]
    	public Decimal DiscountAmount{ get; set; }

        [Display(Name = "Shipping Waived")]
        public Boolean ShippingWaiver{ get; set; }        

        [Display(Name = "Enabled?")]
        public Boolean Status { get; set; }

        [Display(Name = "Minimum Spend")]
        public Decimal MinimumSpend { get; set; }

    	//navigational properties
    	public List<Order> Orders { get; set; }

    }
}