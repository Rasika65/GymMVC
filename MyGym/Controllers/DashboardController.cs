using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyGym.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

        //[MyExceptionHandler]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminDashboard()
        {
            return View("~/Views/Home/AdminDashboard.cshtml");
        }

        //[MyExceptionHandler]
        [Authorize(Roles = "User")]
        public ActionResult UserDashboard()
        {
            return View("~/Views/Home/UserDashboard.cshtml");
        }

    }
}
