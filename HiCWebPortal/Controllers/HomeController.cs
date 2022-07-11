using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPortal.Controllers
{
    public class HomeController : Controller
    {
        private static readonly string envir = (string)ConfigurationManager.AppSettings["Environment"];
        private static readonly string domain = (string)ConfigurationManager.AppSettings["domain_" + envir];
        private static string EventStartDate = ConfigurationManager.AppSettings["EventStartDate"];
        private static DateTime parsedDate = DateTime.Parse(EventStartDate, new CultureInfo("zh-HK"));
        private static bool isStarted = parsedDate < DateTime.Now;

        // GET: Home
        public ActionResult Index()
        {
            string preview = Request.QueryString["preview"];
            
            if (!isStarted && preview != "kitchen")
            {
                return RedirectToAction("Index", "Teaser");
            }
            ViewBag.domain = domain;
            return View();
        }

        public ActionResult Pick()
        {
            string preview = Request.QueryString["preview"];
            
            if (!isStarted && preview != "kitchen")
            {
                return RedirectToAction("Index", "Teaser");
            }
            ViewBag.domain = domain;
            return View();
        }

        public ActionResult Form()
        {
            string preview = Request.QueryString["preview"];
            
            if (!isStarted && preview != "kitchen")
            {
                return RedirectToAction("Index", "Teaser");
            }
            ViewBag.domain = domain;
            return View();
        }

        //public ActionResult Ready()
        //{
        //    return View();
        //}

        public ActionResult Sent()
        {
            string preview = Request.QueryString["preview"];
            
            if (!isStarted && preview != "kitchen")
            {
                return RedirectToAction("Index", "Teaser");
            }
            ViewBag.domain = domain;
            return View();
        }
    }
}
