namespace FindHomeMadeFoodNearMe.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class WebHomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Find your faviorate home made food near you!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact information not avaiable at this moment.";

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}