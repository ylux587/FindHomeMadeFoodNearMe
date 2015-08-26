namespace FindHomeMadeFoodNearMe.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using FindHomeMadeFoodNearMe.Web.Models;

    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult ManageDish(long providerId)
        {
            return View(new ManageDishModel { ProviderId = providerId });
        }
    }
}