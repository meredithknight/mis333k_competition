using System;
using System.Collections.Generic;
namespace Nguyen_Tommy_HW5.Models
{
    public class ProcurementDetail
    {
        public Int32 ProcurementDetailID { get; set; }
        public Decimal Price { get; set; }
        public Int16 Quantity { get; set; }

        public virtual Book Book { get; set; }
        public virtual Procurement Procurement { get; set; }

    }
}
