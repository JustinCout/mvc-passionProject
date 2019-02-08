using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using http5204_passion_project.Models;
using System.Diagnostics;

namespace http5204_passion_project.Controllers
{
    public class AuthorsController : Controller
    {
        // GET: Authors
        private ReviewDbContext db = new ReviewDbContext();

        
        public ActionResult Index()
        {
            return View(db.Authors.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(string new_AuthorAlias)
        {
            if (ModelState.IsValid)
            {
                string query = "insert into Authors (AuthorAlias)" +
                               "values (@author)";

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@author", new_AuthorAlias);
  
                

                db.Database.ExecuteSqlCommand(query, param);
                //testing that the paramters do indeed pass to the method
                //Debug>Windows>Output
                Debug.WriteLine(query);


            }
            return RedirectToAction("List");

        }

    }
}