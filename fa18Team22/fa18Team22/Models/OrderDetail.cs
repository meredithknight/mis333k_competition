using System;
using System.ComponentModel.DataAnnotations;
namespace fa18Team22.Models
{
    public class OrderDetail
    {
    	public Int32 OrderDetailID { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Display(Name = "Quantity")]
        [Range(1, 10000000000, ErrorMessage = "Number of products cannot be negative")]
        public Int32 Quantity { get; set; }

    	[Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
    	public Decimal Price{ get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal ExtendedPrice
        {
            get { return Quantity * Price; }
        }


    	//navigational properties
    	public Order Order { get; set; }
    	public Book Book { get; set; }

    }
}