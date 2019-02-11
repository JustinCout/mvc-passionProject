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
using System.Web.Configuration;

namespace http5204_passion_project.Controllers
{
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {
        

            return View();
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}