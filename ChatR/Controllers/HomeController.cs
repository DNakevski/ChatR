using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChatR.Models;
using ChatR.Helpers.Filters;
using ChatR.Entities;

namespace ChatR.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizeUser]
        public ActionResult Index()
        {
            var user = (User)Session["User"];
            ViewBag.UserName = user.UserName;
            return View();
        }

        [AuthorizeUser]
        public ActionResult Chat()
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