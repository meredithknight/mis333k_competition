using System;
using System.ComponentModel.DataAnnotations;
namespace fa18Team22.Models
{
    public class OrderDetail
    {
    	public Int32 OrderDetailID { get; set; }

    	[Display(Name = "Quantity")]
    	public Int32 Quantity { get; set; }

    	[Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
    	public Decimal Price{ get; set; }

        public Decimal ExtendedPrice
        {
            get
            {
                return Quantity * Price;
            }

        }


    	//navigational properties
    	public virtual Order Order { get; set; }
    	public virtual Book Book { get; set; }

    }
}