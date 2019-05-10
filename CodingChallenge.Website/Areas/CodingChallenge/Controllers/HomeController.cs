using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodingChallenge.Website.Areas.CodingChallenge.Controllers
{
    public class HomeController : Controller
    {
        // GET: CodingChallenge/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}