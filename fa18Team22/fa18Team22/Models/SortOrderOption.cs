using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace fa18Team22.Models
{
    public class SortOrderOption
    {
        [Display(Name = "SortOrderOption ID")]
        public Int32 SortOrderOptionID { get; set; }

        [Display(Name = "Sort Option")]
        public String SortOption { get; set; }
    }
}
