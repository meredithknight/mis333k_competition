using System;
using System.ComponentModel.DataAnnotations;
namespace Team_22_SystemsProject.Models
{
    public class OrderDetail
    {
    	public Int32 OrderDetailID { get; set; }

    	[Display(Name = "Quantity")]
    	public Int32 Quantity { get; set; }

    	[Display(Name = "Price")]
    	public Decimal Price{ get; set; }


    	//navigational properties
    	public virtual Order Order { get; set; }

    	public virtual Book Book { get; set; }

    }
}