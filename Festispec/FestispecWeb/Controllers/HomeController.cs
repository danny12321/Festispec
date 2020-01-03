
using DHTMLX.Scheduler;
using FestispecWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FestispecWeb.Controllers
{
    public class HomeController : Controller
    {
        FestispecEntities1 db = new FestispecEntities1();
        [HttpPost]
        public ActionResult Login(LoginModel user)
        {
            var passwordhash = user.ComputeSha256Hash(user.Password);
            var user1 = db.Users.Where(u => u.email.Equals(user.UserName) && u.password.Equals(passwordhash)).FirstOrDefault();
            if (user1 != null)
            {
                user.Password = null;
                Session["username"] = user.UserName;
                return RedirectToAction("Index");

            }
            
            return View(user);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}