using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace fa18Team22.Models
{
    public class Genre
    {
        [Display(Name = "Genre ID")]
        public Int32 GenreID { get; set; }

        [Display(Name = "Genre Name")]
        public String GenreName { get; set; }

        //navigational properties
        public List<Book> Books { get; set; }
    }
}

