using Einstein.Domain.Services;
using Einstein.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Einstein.WebUI.Controllers
{
    public class TemplatesController : Controller
    {
        private ITemplatesServiceConfig templates;
        public TemplatesController(ITemplatesServiceConfig _templates) 
        {
            templates = _templates;
        }
        // GET: Scheduler
        [Authorize(Roles = "1")]
        public ActionResult Index()
        {
            var tabs = templates.ReadSettings().TEMPLATEs.Select(t =>new TemplateSettingsViewModel {
                id=t.ID,
                title=t.TITLE,
                anchor=t.ANCHOR,
                filename=t.FILENAME
            }
            );


            return View(tabs);
        }
    }
}