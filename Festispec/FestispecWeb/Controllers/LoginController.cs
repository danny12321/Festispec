using FestispecWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FestispecWeb.Controllers
{
    public class LoginController : Controller
    {
       [HttpPost]  
        public ActionResult Login(LoginModel user)
        {
            Session["username"] = user.UserName;
            return View(user);
        }

        //This action will be called when the login is successful
        public ActionResult UserLandingView()
        {
            
            return View();
        }
    }
}