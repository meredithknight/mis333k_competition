using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace fa18Team22.Models
{

    public class Procurement
    {
        [Display(Name = "Procurement ID")]
        public Int32 ProcurementID { get; set; }

        [Display(Name = "Procurement Date")]
        [DisplayFormat(DataFormatString = "{0:MM.dd.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ProcurementDate { get; set; }

        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Price { get; set; }

        [Display(Name = "Quantity")]
        public Int16 Quantity { get; set; }

        public Boolean? ProcurementStatus { get; set; }

        public Book Book { get; set; }
        public AppUser Employee { get; set; }
    }
}
