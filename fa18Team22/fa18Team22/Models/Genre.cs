using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace fa18Team22.Models
=======
using System.ComponentModel.DataAnnotations;
namespace fa18Team22
>>>>>>> afd9985a6cc2b0bf82c0eced24d03d6e9c393421
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
