using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using http = System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace ContactBook.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}