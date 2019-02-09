using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Net;
using http5204_passion_project.Models;
using http5204_passion_project.Models.ViewModels;
using System.Diagnostics;


namespace http5204_passion_project.Controllers
{
    public class ReviewController : Controller
    {
        private ReviewDbContext db = new ReviewDbContext();
       
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult New()
        {
            //EditReview er = new EditReview();

            //er.Authors = db.Authors.ToList();

            return View(db.Authors.ToList());
        }

        [HttpPost]

        public ActionResult Create(string new_ReviewName, string new_ReviewSeries, string new_ReviewCategory,
            string new_ReviewDate, string new_ReviewContent, int? Authors_AuthorId)
        {
            if (ModelState.IsValid)
            {
                string query = "insert into Reviews (ReviewName, ReviewSeries, ReviewCategory, ReviewContent, ReviewDate, Authors_AuthorId)" +
                               "values (@name, @series, @category, @content, @date, @authorID)";

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@name", new_ReviewName);
                param[1] = new SqlParameter("@series", new_ReviewSeries);
                param[2] = new SqlParameter("@category", new_ReviewCategory);
                param[3] = new SqlParameter("@content", new_ReviewContent);
                param[4] = new SqlParameter("@date", new_ReviewDate);
                param[5] = new SqlParameter("@authorID", Authors_AuthorId); 

                db.Database.ExecuteSqlCommand(query, param);
                //testing that the paramters do indeed pass to the method
                //Debug>Windows>Output
                Debug.WriteLine(query);

              
            }
            return RedirectToAction("List");

        }

        public ActionResult Show(int id)
        {
            string query = "select * from reviews where ReviewId =@id";

            //should return type of "Page"

            Debug.WriteLine(query);
            //This line means run the query, take the first one

            return View(db.Reviews.SqlQuery(query, new SqlParameter("@id", id)).FirstOrDefault());
        }

        public ActionResult List()
        {
            return View(db.Reviews.ToList());
        }

        public ActionResult Delete()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            //For edit we need a list of authors to pick from
            //we also need to know what the current author is

            Review re = new Review();
            re.Authors = db.Authors.ToList();
            //Line equivalent to "Select * from blogs where blogid = .."
            blogeditview.blog = db.Blogs.Find(id);

            return View(blogeditview);
        }

        [HttpPost]
        public ActionResult Edit(int id, string BlogTitle, int BlogAuthor, string BlogBio)
        {
            //If the ID doesn't exist or the blog doesn't exist
            if ((id == null) || (db.Blogs.Find(id) == null))
            {
                return HttpNotFound();

            }
            string query = "update blogs set BlogTitle=@title, BlogBio=@bio, author_AuthorID=@author where blogid=@id";
            SqlParameter[] myparams = new SqlParameter[4];
            myparams[0] = new SqlParameter("@title", BlogTitle);
            myparams[1] = new SqlParameter("@bio", BlogBio);
            myparams[2] = new SqlParameter("@author", BlogAuthor);
            myparams[3] = new SqlParameter("@id", id);
            //forcing the blog to have an author

            db.Database.ExecuteSqlCommand(query, myparams);

            return RedirectToAction("Show/" + id);
        }
    }
}