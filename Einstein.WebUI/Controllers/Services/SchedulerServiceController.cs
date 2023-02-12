using Einstein.Domain.Services;
using Einstein.WebUI.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Einstein.WebUI.Controllers.Services
{
    public class SchedulerServiceController : Controller
    {
        private EventsService eventsService;
        
        public SchedulerServiceController(IRepository repository)
        {
            eventsService = new EventsService(repository);
        }

        public virtual JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {

            return Json(eventsService.GetAll().ToDataSourceResult(request));
        }
        public virtual JsonResult ReadAvailableEvents([DataSourceRequest] DataSourceRequest request)
        {

            return Json(eventsService.GetAll().ToDataSourceResult(request));
        }


        public virtual JsonResult Destroy([DataSourceRequest] DataSourceRequest request, EventViewModel appointment)
        {
           
                try
                {
                    eventsService.Delete(appointment, ModelState);
                }
                catch (Exception ex) 
                {
                    ModelState.AddModelError("error", "Не удалось удалить мероприятие.");
                }
            

            return Json(new[] { appointment }.ToDataSourceResult(request, ModelState));
        }

        public virtual JsonResult Create([DataSourceRequest] DataSourceRequest request, EventViewModel appointment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    eventsService.Insert(appointment, ModelState);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", "Не удалось создать мероприятие.");
                }
            }
    

            return Json(new[] { appointment }.ToDataSourceResult(request, ModelState));
        }
       
        public virtual JsonResult Update([DataSourceRequest] DataSourceRequest request, EventViewModel appointment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    eventsService.Update(appointment, ModelState);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", "Не удалось обновить мероприятие.");
                }
        }


            return Json(new[] { appointment }.ToDataSourceResult(request, ModelState));
        }


        public JsonResult DropDownListReadNames()
        {
            var titles = eventsService.GetAvailableEvents(DateTime.Now,DateTime.Now.AddMonths(2));
            return Json(titles, JsonRequestBehavior.AllowGet);
        }

       

        protected override void Dispose(bool disposing)
        {
            eventsService.Dispose();
            base.Dispose(disposing);
        }


    }
}