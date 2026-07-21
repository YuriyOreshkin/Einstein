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

        public ActionResult Editor(string name)
        {
            var model = new TemplateViewModel() { name = name, subject=service.GetTemplateSubject(),  body=service.GetTemplateBody() };
            switch (name)
            {
                case "TicketTemplateService":
                    return PartialView("~/Views/TemplateService/TicketEditor.cshtml", model);
                case "TermsService":
                    return PartialView("~/Views/TemplateService/TermsEditor.cshtml", model);

                default:
                    return PartialView("~/Views/TemplateService/OrderEditor.cshtml", model);
            }
            

        }

        public virtual ActionResult AvailableParameters()
        {

            var parameters = service.AvailableParameters(typeof(OrderViewModel));

            return PartialView("~/Views/TemplateService/AvailableParameters.cshtml",parameters);
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