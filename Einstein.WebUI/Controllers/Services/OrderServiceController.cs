using Einstein.Domain.Entities;
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
    public class OrderServiceController : Controller
    {
        

        private OrdersService ordersService;
        public OrderServiceController(IRepository _repository)
        {
            ordersService = new OrdersService(_repository);
        }


        public ActionResult ReadGrid([DataSourceRequest] DataSourceRequest request)
        {
            return Json(ordersService.GetAll().OrderByDescending(o=>o.dateorder).ToDataSourceResult(request));
        }

        //Create
        [HttpPost]
        public ActionResult CreateForGrid([DataSourceRequest]DataSourceRequest request, OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ordersService.Insert(order, ModelState);
                    
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", "Не удалось внести заявку.");
                }
            }

            return Json(new[] { order }.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public ActionResult UpdateForGrid([DataSourceRequest]DataSourceRequest request, OrderViewModel order)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    ordersService.Update(order, ModelState);
                }
                catch (Exception ex) 
                {
                    ModelState.AddModelError("error", "Не удалось обновить заявку.");
                }
            }

            return Json(new[] { order }.ToDataSourceResult(request, ModelState));

        }

        //Delete
        [HttpPost]
        public ActionResult DestroyForGrid([DataSourceRequest]DataSourceRequest request, OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ordersService.Delete(order, ModelState);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", "Не удалось удалить заявку.");
                }
            }

            return Json(new[] { order }.ToDataSourceResult(request, ModelState));
        }



    }
}