using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace http5204_passion_project.Models
{
    public class ReviewDbContext : DbContext
    {
        public ReviewDbContext()
        {

        }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}