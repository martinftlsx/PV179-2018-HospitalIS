﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalIS.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.message = "your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "your contact page.";

            return View();
        }
    }
}