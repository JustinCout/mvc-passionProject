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
using System.IO;
using System.Web.Configuration;

namespace http5204_passion_project.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Authors
        private ReviewDbContext db = new ReviewDbContext();

        
        public ActionResult Index()
        {
            return RedirectToAction("List");
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
                string query = "insert into Authors (AuthorAlias, HasPic, ImageType)" +
                               "values (@author, 0, 0)";

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@author", new_AuthorAlias);
  
                

                db.Database.ExecuteSqlCommand(query, param);
         
                Debug.WriteLine(query);


            }
            return RedirectToAction("List");

        }

        public ActionResult List()
        {
            return View(db.Authors.ToList());
        }

        public ActionResult Show(int id)
        {
            string query = "select * from authors where authorid =@id";

            Debug.WriteLine(query);
            
            return View(db.Authors.SqlQuery(query, new SqlParameter("@id", id)).FirstOrDefault());
        }

        public ActionResult Edit(int id)
        {
            Author a = new Author();
            a = db.Authors.Find(id);

            return View(a);
        }

        [HttpPost]
        public ActionResult Edit(int? id, string AuthorAlias, string ImageType)
        {
            if ((id == null) || (db.Authors.Find(id) == null))
            {
                return HttpNotFound();
            }

            string query = "update Authors set AuthorAlias=@name, ImageType=@image where Authorid=@id";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@name", AuthorAlias);
            param[1] = new SqlParameter("@image", ImageType);
            param[2] = new SqlParameter("@id", id);
            
            db.Database.ExecuteSqlCommand(query, param);

            return RedirectToAction("Show/" + id);
        }

        //Was not able to get uploading an image to work

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "AuthorId, AuthorAlias")] Author author, HttpPostedFileBase authorimg)
        //{
        //    author.HasPic = 0;


        //    if (authorimg.ContentLength > 0)
        //    {
                
        //        var validImg = new[] { "jpeg", "jpg", "png", "gif" };
        //        var extension = Path.GetExtension(authorimg.FileName).Substring(1);

        //        if (validImg.Contains(extension))
        //        {

        //            string fn = author.AuthorId + "." + extension;

        //            //get a direct file path to imgs/authors/
        //            string path = Path.Combine(Server.MapPath("~/images/authors"), fn);

        //            //save the file
        //            authorimg.SaveAs(path);

        //            //let the model know that there is a picture with an extension
        //            author.HasPic = 1;
        //            author.ImageType = extension;

        //        }
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(author).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Show");
        //    }

        //    return View(author);
        //}

        public ActionResult Delete(int? id)
        {
            if ((id == null) || (db.Authors.Find(id) == null))
            {
                return HttpNotFound();

            }
            
            string query = "delete from Reviews where Authors_AuthorId=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);

            query = "delete from Authors where AuthorId=@id";
            param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);
            return RedirectToAction("List");
        }
    }
}