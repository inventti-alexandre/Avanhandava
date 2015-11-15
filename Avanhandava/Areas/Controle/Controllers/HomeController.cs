using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Avanhandava.Areas.Controle.Controllers
{
    public class HomeController : Controller
    {
        // GET: Controle/Home
        public ActionResult Index(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }
    }
}