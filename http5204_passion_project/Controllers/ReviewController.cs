using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Net;
using http5204_passion_project.Models;
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


            return View();
        }

        [HttpPost]

        public ActionResult Create(string new_ReviewName, string new_ReviewSeries, string new_ReviewCategory,
            string new_ReviewDate, string new_ReviewContent, string Authors_AuthorId)
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
                param[5] = new SqlParameter("@authorID", 1); //MAKE SURE TO CHANGE IT LATER! to author

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

        public ActionResult Edit()
        {
            return View();
        }
    }
}