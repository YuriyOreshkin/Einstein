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
    public class MailSettingsServiceController : Controller
    {
        private IMailServiceConfig mailconfig;
        public MailSettingsServiceController(IMailServiceConfig _mailconfig)
        {
            mailconfig = _mailconfig;
        }

        public ActionResult Settings()
        {
            try
            {
                var settings = mailconfig.ReadSettings();

                MailSettingsViewModel view = new MailSettingsViewModel
                {
                    enable = settings.ENABLE,
                    host = settings.HOST,
                    port = settings.PORT,
                    user = settings.USER,
                    password = settings.PASSWORD
                };

                return PartialView(view);
            }
            catch (Exception ex)
            {
                return Content("Ошибка: "+ ex.Message);
            }
        }


        public JsonResult SaveSettings(MailSettingsViewModel viewsettings)
        {
            MAILSERVICESETTINGS settings = viewsettings.ToEntity(new MAILSERVICESETTINGS());

            //Save
            try
            {
                mailconfig.SaveSettings(settings);

            }
            catch (Exception exception)
            {
                return Json(new { message = "errors", errors = "Ошибка: " + exception.Message }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { message = "OK", result = viewsettings }, JsonRequestBehavior.AllowGet);
        }

    }
}