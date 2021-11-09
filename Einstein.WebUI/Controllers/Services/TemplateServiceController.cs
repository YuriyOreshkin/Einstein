using Einstein.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Einstein.WebUI.Controllers.Services
{
    public class TemplateServiceController : Controller
    {
        private ITemplateService service;
        public TemplateServiceController(ITemplateService _service)
        {
            service = _service;
        }

        public ActionResult Editor()
        {
            string text = service.GetTemplateText();

            return PartialView("Editor", text);

        }


        public JsonResult SaveTemplate(string data)
        {
            //Save
            try
            {
                 service.SaveTemplate(data);
            }
            catch (Exception exception)
            {
                return Json(new { message = "errors", errors = "Ошибка: " + exception.Message }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { message = "OK"  }, JsonRequestBehavior.AllowGet);
        }
    }
}