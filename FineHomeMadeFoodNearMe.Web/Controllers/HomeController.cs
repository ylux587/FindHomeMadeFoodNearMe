﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FindHomeMadeFoodNearMe.Web.Controllers
{
    using Models;

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

        public ActionResult Register()
        {
            return View("RegisterLogin", new RegisterLoginModel { InRegister = true });
        }

        public ActionResult Login()
        {
            return View("RegisterLogin", new RegisterLoginModel { InRegister = false });
        }
    }
}