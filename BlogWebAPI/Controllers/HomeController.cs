﻿using System.Web.Mvc;

namespace BlogWebAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HomeController'
    public class HomeController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HomeController'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'HomeController.Index()'
        public ActionResult Index()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'HomeController.Index()'
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
