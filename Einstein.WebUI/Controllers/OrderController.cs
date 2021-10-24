using Einstein.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Einstein.WebUI.Controllers
{
    public class OrderController : Controller
    {
        
        public ActionResult Index()
        {
            return View(new OrderViewModel());
        }

        
        public ActionResult GridView()
        {
            return View();
        }
    }
}