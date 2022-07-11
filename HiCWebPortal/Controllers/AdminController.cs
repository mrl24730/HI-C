using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPortal.DB;

namespace WebPortal.Controllers
{
    public class AdminController : Controller
    {
        private static readonly string AdminPassword = ConfigurationManager.AppSettings["AdminPassword"];

        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.hasError = false;
            return View();
        }

        public ActionResult Stats()
        {
            if (Session["isAuthenticated"] != null)
            {
             
                var stats = AdminRepository.GetStats();
                ViewBag.stats = stats;
            
                return View();
            }
            return RedirectToAction("Index", "Admin");
        }


        [HttpPost]
        public ActionResult Index(string password)
        {
            if (password == AdminPassword)
            {
                Session.Clear();
                Session["isAuthenticated"] = "1";
                return RedirectToAction("Stats", "Admin");
            }
            else
            {
                ViewBag.hasError = true;
                return View("Index");
            }

        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            Response.Cookies["ASP.NET_SessionId"].Value = "";
            return RedirectToAction("Index", "Admin");
        }
    }
}
