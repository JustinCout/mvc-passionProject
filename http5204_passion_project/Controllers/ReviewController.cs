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


            return View(db.Reviews.ToList());
        }

        [HttpPost]

        public ActionResult Create(string ReviewTitle, string ReviewSeries, string ReviewCategory,
            string ReviewDate, string AuthorAlias, string ReviewContent)
        {
            string query = "insert into Reviews (ReviewTitle, ReviewSeries, ReviewCategory, ReviewDate, AuthorAlias, ReviewContent) " +
                           "values (@title,@series,@category,@date,@alias,@content)";

            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@title", ReviewTitle);
            param[1] = new SqlParameter("@series", ReviewSeries);
            param[2] = new SqlParameter("@category", ReviewCategory);
            param[3] = new SqlParameter("@date", ReviewDate);
            param[4] = new SqlParameter("@alias", AuthorAlias);
            param[5] = new SqlParameter("@content", ReviewContent);

            db.Database.ExecuteSqlCommand(query, param);
            //testing that the paramters do indeed pass to the method
            //Debug>Windows>Output
            Debug.WriteLine(query);

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