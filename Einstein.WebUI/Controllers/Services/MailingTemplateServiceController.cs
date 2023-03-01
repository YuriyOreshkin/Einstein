using Einstein.Domain.Services;
using Einstein.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Einstein.WebUI.Controllers.Services
{
    public class MailingTemplateServiceController : Controller
    {
        private ITemplateService service;
        public MailingTemplateServiceController(ITemplateService _service)
        {
            service = _service;
        }

        public ActionResult Editor()
        {
            var model = new TemplateViewModel() { name= "MailingTemplateService", subject=service.GetTemplateSubject(),  body=service.GetTemplateBody() };

            return PartialView("~/Views/TemplateService/Editor.cshtml", model);

        }

        public ActionResult AvailableParameters()
        {

            var parameters = service.AvailableParameters(typeof(PeriodViewModel));

            return PartialView("~/Views/TemplateService/AvailableParameters.cshtml", parameters);
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