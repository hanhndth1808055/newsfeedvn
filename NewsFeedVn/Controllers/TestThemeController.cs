using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsFeedVn.Controllers
{
    public class TestThemeController : Controller
    {
        // GET: TestTheme
        public ActionResult IndexBackend()
        {
            return View("~/Views/Backend/Dashboard.cshtml");
        }

        public ActionResult IndexFrontend()
        {
            return View("~/Views/Frontend/Home.cshtml");
        }

        public ActionResult DemoForm()
        {
            return View("~/Views/Backend/DemoForm2.cshtml");
        }
    }
}