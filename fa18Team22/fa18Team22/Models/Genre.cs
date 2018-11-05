using System;
namespace fa18Team22
{
    public class Genre
    {
        public Int32 GenreID { get; set; }
        public String GenreName { get; set; }


        //navigational properties
        public virtual Book Book { get; set; }
    }
}
