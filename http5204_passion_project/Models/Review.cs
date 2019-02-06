using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace http5204_passion_project.Models
{
    public class Review
    {
        [Key, ScaffoldColumn(false)]
        public int ReviewId { get; set; }

        [Required, StringLength(255), Display(Name = "Title: ")]
        public string ReviewName { get; set; }

        [Required, StringLength(255), Display(Name = "Series: ")]
        public string ReviewSeries { get; set; }

        [Required, StringLength(255), Display(Name = "Category: ")] //will be a dropdown
        public string ReviewCategory { get; set; }

        [Required, StringLength(1000), Display(Name = "Content: ")]
        public string ReviewContent { get; set; }

        public DateTime ReviewDate { get; set; }

        public virtual Author Authors { get; set; }
    }
}