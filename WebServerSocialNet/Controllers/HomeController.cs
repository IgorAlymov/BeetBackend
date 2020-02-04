using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebServerSocialNet.Helpers;

namespace WebServerSocialNet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            /*
            var db = new DbSeed();
            db.Initialize();
            */

            return View();
        }
    }
}
