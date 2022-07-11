using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPortal.Controllers
{
    public class TeaserController : Controller
    {
        // GET: Teaser
        public ActionResult Index()
        {
            return View();
        }
    }
}
