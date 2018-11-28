using System;
using System.Collections.Generic;
using fa18Team22.DAL;
using fa18Team22.Models;
using System.Linq;

namespace fa18Team22.Seeding
{
    public static class SeedSortOrderOptions
    {
        public static void SeedAllSortOrderOptions(AppDbContext db)
        {
            //check to see if all the languages have already been added
            if (db.SortOrderOptions.Count() == 7)
            {
                //exit the program - we don't need to do any of this
                NotSupportedException ex = new NotSupportedException("Sort Order Option record count is already 7!");
                throw ex;
            }
            Int32 intSortOrderOptionsAdded = 0;
            try
            {
                //Create a list of languages
                List<SortOrderOption> SortOrderOptions = new List<SortOrderOption>();

                SortOrderOption g1 = new SortOrderOption { SortOption = "Adventure" };
                SortOrderOptions.Add(g1);

                SortOrderOption g2 = new SortOrderOption { SortOption = "Contemporary Fiction" };
                SortOrderOptions.Add(g2);

                SortOrderOption g3 = new SortOrderOption { SortOption = "Fantasy" };
                SortOrderOptions.Add(g3);

                SortOrderOption g4 = new SortOrderOption { SortOption = "Historical Fiction" };
                SortOrderOptions.Add(g4);

                SortOrderOption g5 = new SortOrderOption { SortOption = "Horror" };
                SortOrderOptions.Add(g5);

                SortOrderOption g6 = new SortOrderOption { SortOption = "Humor" };
                SortOrderOptions.Add(g6);

                SortOrderOption g7 = new SortOrderOption { SortOption = "Mystery" };
                SortOrderOptions.Add(g7);


                SortOrderOption ss;

                //loop through the list and see which (if any) need to be added
                foreach (SortOrderOption soo in SortOrderOptions)
                {
                    //see if the language already exists in the database
                    ss = db.SortOrderOptions.FirstOrDefault(x => x.SortOption == soo.SortOption);

                    //language was not found
                    if (ss == null)
                    {
                        //Add the language
                        db.SortOrderOptions.Add(soo);
                        db.SaveChanges();
                        intSortOrderOptionsAdded += 1;
                    }

                }
            }
            catch
            {
                String msg = "Sort Order Options Added: " + intSortOrderOptionsAdded.ToString();
                throw new InvalidOperationException(msg);
            }

        }
    }
}
