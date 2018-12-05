using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace fa18Team22.Models
{
    public class Book
    {


        [Display(Name = "Book ID")]
        [Key]
        public Int32 BookID { get; set; }

        [Display(Name = "Unique ID")]
        public Int32 UniqueID { get; set; }

        [Display(Name = "Title")]
        public String Title { get; set; }

        [Display(Name = "Author")]
        public String Author { get; set; }

        [Display(Name = "Publish Date")]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; }

        [Display(Name = "Book Detail")]
        public String BookDetail { get; set; }

        [Display(Name = "Sales Price")]
        public Decimal SalesPrice { get; set; }

        [Display(Name = "Inventory")]
        public Int32 Inventory { get; set; }

        [Display(Name = "Sales Price")]
        public Decimal InitialSalesPrice { get; set; }

        [Display(Name = "Sales Price")]
        public Decimal InitialCost { get; set; }

        [Display(Name = "Inventory")]
        public Int32 InitialInventory { get; set; }

        [Display(Name = "Average Rating")]
        public Decimal? AvgRating
        {
            get
            {
                if (Reviews.Count() == 0)
                {
                    return 0.0m;
                }
                else
                {
                    List<Review> ApprovedReviews = new List<Review>();
                    foreach (Review item in Reviews)
                    {
                        if (item.ApprovalStatus == true && item.ReviewText != null)
                        {
                            ApprovedReviews.Add(item);
                        }
                    }
                    if (ApprovedReviews.Count() == 0)
                    {
                        return 0.0m;
                    }
                    else
                    {
                        Decimal avgrat = ApprovedReviews.Average(m => m.Rating);
                        //Decimal decAvgrat = Convert.ToDecimal(avgrat);
                        avgrat = Math.Round(avgrat, 1);
                        return avgrat;
                    }
                }
            }
        }

        [Display(Name = "Replenish Minimum")]
        public Int32 ReplenishMinimum { get; set; }

        [Display(Name = "Book Cost")]
        public Decimal BookCost { get; set; }

        [Display(Name = "Average Book Cost")]
        public Decimal? AvgBookCost
        {
            get
            { 
                if(Procurements.Count() == 0)
                {
                    return BookCost;
                }
                else
                {
                    List<Procurement> CheckedInProcurements = new List<Procurement>();
                    foreach (Procurement item in Procurements)
                    {
                        if (item.ProcurementStatus == true)
                        {
                            CheckedInProcurements.Add(item);
                        }
                    }
                    if (CheckedInProcurements.Count() == 0)
                    {
                        return BookCost;
                    }
                    else
                    {
                        decimal decAvgCost = Procurements.Average(p => p.Price);
                        decAvgCost = Math.Round(decAvgCost, 2);
                        return decAvgCost;
                    }
                }
                 
            }
        }

        [Display(Name = "Average Sales Price")]
        public Decimal? AvgSalesPrice
        {
            get { return OrderDetails.Average(od => od.ExtendedPrice); }
        }

        //set to false if book is being carried in the store
        public Boolean IsDiscontinued { get; set; }

        //navigational properties
        public List<Procurement> Procurements { get; set; }
        public Genre Genre { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Review> Reviews { get; set; }
        //public List<Order> BooksInRecommend { get; set; }
        public Book()
        {
            if (Reviews == null)
            {
                Reviews = new List<Review>();
            }
            if (OrderDetails == null)
            {
                OrderDetails = new List<OrderDetail>();
            }
            if (Procurements == null)
            {
                Procurements = new List<Procurement>();
            }
        }
    }
}