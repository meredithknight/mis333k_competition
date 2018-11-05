using System;
using System.Collections.Generic;

namespace fa18Team22.Models
{
    public enum ProcurementStatus { Ordered, Delivered }

    public class Procurement
    {
        public Int32 ProcurementID { get; set; }
        public DateTime ProcurementDate { get; set; }
        public Decimal Price { get; set; }
        public Int16 Quantity { get; set; }

        public List<Book> Books { get; set; }
    }
}
