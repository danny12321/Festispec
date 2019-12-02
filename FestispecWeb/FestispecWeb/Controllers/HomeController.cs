﻿using Festipec.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FestispecWeb.Controllers
{
    public class HomeController : Controller
    {

        FestispecEntities fe = new FestispecEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Availability()
        {
            return View();
        }

        public ActionResult Planning()
        {
            return View();
        }

        public ActionResult Questionnaires()
        {
            return View();
        }
        public ActionResult Inspections()
        {
            return View(fe.Inspections.ToList());
        }

    }
}