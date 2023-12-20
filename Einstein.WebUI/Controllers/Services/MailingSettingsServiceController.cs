using Einstein.Domain.Entities;
using Einstein.Domain.Services;
using Einstein.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Einstein.WebUI.Controllers.Services
{
    [Authorize]
    public class MailingSettingsServiceController : Controller
    {
        private IMailingServiceConfig config;
        public MailingSettingsServiceController(IMailingServiceConfig _config)
        {
            config = _config;
        }

        public ActionResult Settings()
        {
            try
            {
                var settings = config.ReadSettings();

                MailingSettingsViewModel view = new MailingSettingsViewModel
                {
                    enablerss = settings.ENABLE,
                    beginfromtoday = settings.BEGINFROMTODAY,
                    endfromtoday = settings.ENDFROMTODAY,
                    recipients = settings.RECIPIENTS.Split(',')
                    
                };

                return PartialView(view);
            }
            catch (Exception ex)
            {
                return Content("Ошибка: "+ ex.Message);
            }
        }


        public JsonResult SaveSettings(MailingSettingsViewModel viewsettings)
        {
            MAILINGSERVICESETTINGS settings = viewsettings.ToEntity(new MAILINGSERVICESETTINGS());

            //Save
            try
            {
                config.SaveSettings(settings);

            }
            catch (Exception exception)
            {
                return Json(new { message = "errors", errors = "Ошибка: " + exception.Message }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { message = "OK", result = viewsettings }, JsonRequestBehavior.AllowGet);
        }

    }
}