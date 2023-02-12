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
    public class PaymentSettingsServiceController : Controller
    {
        private IPaymentServiceConfig paymentconfig;
        public PaymentSettingsServiceController(IPaymentServiceConfig _paymentconfig)
        {
            paymentconfig = _paymentconfig;
        }

        public ActionResult Settings()
        {
            try
            {
                var settings = paymentconfig.ReadSettings();

                PaymentSettingsViewModel view = new PaymentSettingsViewModel
                {
                    enablepass = settings.ENABLE,
                    terminalid = settings.TERMINALID,
                    frame=settings.FRAME,
                    language=settings.LANGUAGE
                };

                return PartialView(view);
            }
            catch (Exception ex)
            {
                return Content("Ошибка: "+ ex.Message);
            }
        }


        public JsonResult SaveSettings(PaymentSettingsViewModel viewsettings)
        {
            PAYMENTSETTINGS settings = viewsettings.ToEntity(new PAYMENTSETTINGS());

            //Save
            try
            {
                paymentconfig.SaveSettings(settings);

            }
            catch (Exception exception)
            {
                return Json(new { message = "errors", errors = "Ошибка: " + exception.Message }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { message = "OK", result = viewsettings }, JsonRequestBehavior.AllowGet);
        }

    }
}