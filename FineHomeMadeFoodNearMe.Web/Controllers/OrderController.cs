using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindHomeMadeFoodNearMe.Web.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult PlaceOrder()
        {
            return View();
        }

        public ActionResult OrderSummary()
        {
            return View();
        }
    }
}