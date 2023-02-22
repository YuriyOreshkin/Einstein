using Einstein.Domain.Services;
using Einstein.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Einstein.WebUI.Controllers.Services
{
    public class TermsServiceController : Controller
    {
        private ITermsService service;
        public TermsServiceController(ITermsService _service)
        {
            service = _service;
        }

        public ActionResult Editor()
        {
            var model = service.GetTemplate();

            return PartialView("Editor", model);

        }


        public JsonResult SaveTemplate(string body)
        {
            //Save
            try
            {
                 service.SaveTemplate(body);
            }
            catch (Exception exception)
            {
                return Json(new { message = "errors", errors = "Ошибка: " + exception.Message }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { message = "OK"  }, JsonRequestBehavior.AllowGet);
        }
    }
}