using fa18Team22.DAL;
using System;
using System.Linq;


namespace fa18Team22.Utilities
{
    public static class GenerateNextOrderNumber
    {
        public static Int32 GetNextOrderNumber(AppDbContext db)
        {
            Int32 intMaxOrderNumber; //the current maximum order number
            Int32 intNextOrderNumber; //the order number for the next order

            if (db.Orders.Count() == 0) //there are no orders in the database yet
            {
                intMaxOrderNumber = 10000; //registration numbers start at 10001
            }
            else
            {
                intMaxOrderNumber = db.Orders.Max(c => c.OrderNumber); //this is the highest number in the database right now
            }

            //add one to the current max to find the next one
            intNextOrderNumber = intMaxOrderNumber + 1;

            //return the value
            return intNextOrderNumber;
        }

    }
}