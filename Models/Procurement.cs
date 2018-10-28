using System;
using System.Collections.Generic;

namespace Nguyen_Tommy_HW5.Models
{
    public class Procurement
    {
        public Int32 ProcurementID { get; set; }
        public DateTime ProcurementDate { get; set; }
        public enum ProcurementStatus {Ordered, Delivered}

        public virtual List<ProcurementDetail> ProcurementDetails { get; set; }
    }
}
