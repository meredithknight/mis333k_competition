using fa18Team22.DAL;
using System;
using System.Linq;

namespace fa18Team22.Utilities
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public static class GenerateBUN
    {
        //generating BOOK UNIQUE NUMBER -- property is UniqueID
        public static Int32 GetNextBUN(AppDbContext db)
        {


            Int32 intMaxBUN; //the current maximum course number
            Int32 intNextBUN; //the course number for the next class

            if (db.Books.Count() == 0) //there are no courses in the database yet
            {
                intMaxBUN = 78900; //course numbers start at 78901
            }
            else
            {
                intMaxBUN = db.Books.Max(c => c.UniqueID); //this is the highest number in the database right now
            }

            //add one to the current max to find the next one
            intNextBUN = intMaxBUN + 1;

            //return the value
            return intNextBUN;
        }
    }
}
