using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Einstein.WebUI.Controllers
{
    public class SchedulerController : Controller
    {
        // GET: Scheduler
        [Authorize]
        public ActionResult Index()
        {
            
            return View();
        }
    }
}