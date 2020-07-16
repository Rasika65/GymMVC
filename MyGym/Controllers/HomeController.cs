using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyGym.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string role)
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            if (Request.IsAuthenticated)
            {
                if (Session["Role"].ToString() == "Admin")
                    return View("AdminDashboard");
                if (Session["Role"].ToString() == "User")
                    return View("UserDashboard");
                else
                    return View("~/Views/Account/Login.cshtml");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
