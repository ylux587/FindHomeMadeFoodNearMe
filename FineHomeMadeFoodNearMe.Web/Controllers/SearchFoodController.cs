using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindHomeMadeFoodNearMe.Web.Controllers
{
    public class SearchFoodController : Controller
    {
        // GET: SearchFood
        public ActionResult FoodMap()
        {
            return View();
        }
    }
}