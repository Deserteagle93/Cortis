﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketPlaceWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Hello There and Welcome To marketpalce website";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}
