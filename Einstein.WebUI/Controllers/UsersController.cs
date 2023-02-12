using Einstein.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Einstein.WebUI.Controllers
{
    public class UsersController : Controller
    {
        private IRepository repos;
        public UsersController(IRepository _repos) 
        {
            repos = _repos;
        }
        // GET: Scheduler
        [Authorize(Roles = "1")]
        public ActionResult Index()
        {
            ViewData["roles"] = repos.Roles.ToList().Select(r => new SelectListItem { Value = r.ID.ToString() , Text=r.NAME});


            return View();
        }
    }
}