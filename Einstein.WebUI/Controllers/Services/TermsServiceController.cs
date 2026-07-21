using Einstein.Domain.Services;
using Einstein.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Einstein.WebUI.Controllers.Services
{
    public class TermsServiceController : TemplateServiceController
    {
        private ITemplateService terms;
        public TermsServiceController(ITemplateService _service) : base(_service)
        {
            terms = _service;
        }
        public ActionResult Terms()
        {

            return View("~/Views/Order/Terms.cshtml", terms.GetTemplateBody() as Object);
        }

        //public ActionResult Editor()
        //{
        //    var model = service.GetTemplate();

        //    return PartialView("Editor", model);

        //}


        //public JsonResult SaveTemplate(string body)
        //{
        //    //Save
        //    try
        //    {
        //         service.SaveTemplate(body);
        //    }
        //    catch (Exception exception)
        //    {
        //        return Json(new { message = "errors", errors = "Ошибка: " + exception.Message }, JsonRequestBehavior.AllowGet);
        //    }


        //    return Json(new { message = "OK"  }, JsonRequestBehavior.AllowGet);
        //}
    }
}