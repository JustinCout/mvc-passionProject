using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace http5204_passion_project.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required, StringLength(255), Display(Name = "Nickname: ")]
        public string AuthorAlias { get; set; }

        public int HasPic { get; set; }

        public string ImageType { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}