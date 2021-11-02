using Einstein.Domain.Entities;
using Einstein.Domain.Services;
using Einstein.WebUI.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Einstein.WebUI.Hubs;
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
                    
                    var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<OrdersHub>();

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", "Не удалось внести заявку.");
                }
            }

            return Json(new[] { order }.ToDataSourceResult(request, ModelState));

        }

        [HttpPost]
        public ActionResult CreateOrder(OrderViewModel order)
        {
            
            if (ModelState.IsValid)
            {
                try
                {

                    ordersService.Insert(order, ModelState);
                    var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<OrdersHub>();

                    context.Clients.All.displayAddOrder("Новая заявка от " +order.email+ " по мероприятию "+order.eventname +" на дату "+ order.dateevent.ToShortDateString() + " и время " + order.timeevent + order.eventname );
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", "Не удалось внести заявку.");
                }
            }

            return Json(new { Errors  = ModelState.IsValid ? null : ModelState.SerializeErrors() } );
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