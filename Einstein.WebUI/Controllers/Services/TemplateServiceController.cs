using Einstein.Domain.Services;
using Einstein.WebUI.Models;
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
            var model = new TemplateViewModel() { subject=service.GetTemplateSubject(),  body=service.GetTemplateBody() };

            return PartialView("Editor", model);

        }

        public ActionResult AvailableParameters()
        {

            var parameters = service.AvailableParameters(typeof(OrderViewModel));

            return PartialView(parameters);
        }

        public JsonResult SaveTemplate(string subject, string body)
        {
            //Save
            try
            {

                 service.SaveTemplate(subject, body);
            }
            catch (Exception exception)
            {
                return Json(new { message = "errors", errors = "Ошибка: " + exception.Message }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { message = "OK"  }, JsonRequestBehavior.AllowGet);
        }
    }
}