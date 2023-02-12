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
using Einstein.WebUI.Extensions;

namespace Einstein.WebUI.Controllers.Services
{
    public class OrderServiceController : Controller
    {

        private OrdersService ordersService;
        public OrderServiceController(IRepository _repository, IMailSender sender, IPaymentServiceConfig payservice)
        {
            ordersService = new OrdersService(_repository, sender, payservice);
        }


        public ActionResult ReadGrid([DataSourceRequest] DataSourceRequest request)
        {
            return Json(ordersService.GetAll().OrderByDescending(o => o.dateorder).ToDataSourceResult(request));
        }

        public ActionResult Booking(long id)
        {

            var order = ordersService.One(o => o.id == id);
            if (order == null)
            {
                return RedirectToAction("Index", "Order");
            }
            return View("~/Views/Order/ETicket.cshtml", order);
        }
        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }


        //Create
        [HttpPost]
        public ActionResult CreateForGrid([DataSourceRequest] DataSourceRequest request, OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    ordersService.Insert(order, ModelState);
                    //Отсылаем уведомление
                    ordersService.SendNotification(order);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", "Не удалось внести заявку.");
                }
            }

            return Json(new[] { order }.ToDataSourceResult(request, ModelState));

        }




        [HttpPost]
        public ActionResult SendNotification(OrderViewModel order)
        {
            ordersService.SendNotification(order);
            if (!order.inform)
            {

                return Json(new { message = "errors", result = "Не удалось отправить уведомление!" }, JsonRequestBehavior.AllowGet);

            }

            return Json(new { message = "OK", result = "Уведомление успешно отправлено!" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PayOrder(OrderViewModel order)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    ordersService.Insert(order, ModelState);

                    var message = String.Format("Новая заявка от {0} по мероприятию {1} на дату {2} и время  {3}", order.email, order.eventname, order.dateevent.ToShortDateString(), order.timeevent);
                    SendSignal(message);
                    //Отсылаем уведомление
                    ordersService.SendNotification(order);

                    var payment = ordersService.InitPayment(order);
                    return Json(payment);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", "Не удалось внести заявку.");
                }
            }

            return Json(new { Errors = ModelState.IsValid ? null : ModelState.SerializeErrors() });
        }
        [HttpPost]
        public ActionResult PayTicket(long orderid)
        {
            var order = ordersService.One(o => o.id == orderid);
            if (order != null)
            {
                try
                {
                    var payment = ordersService.InitPayment(order);
                    return Json(payment);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", "Не удалось оплатить билет.");
                }
            }
            else
            {
                ModelState.AddModelError("error", String.Format("Заказ № {0} не найден в системе!", orderid));
            }

            return Json(new { Errors = ModelState.IsValid ? null : ModelState.SerializeErrors() });
        }

        public static void SendSignal(string message) 
        {

            var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<OrdersHub>();

            context.Clients.All.displayAddOrder(message);
        }

        [HttpPost]
        public ActionResult CreateOrder(OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    ordersService.Insert(order, ModelState);
                    var message =String.Format("Новая заявка от {0} по мероприятию {1} на дату {2} и время  {3}", order.email, order.eventname, order.dateevent.ToShortDateString(), order.timeevent);
                    SendSignal(message);
                    //Отсылаем уведомление
                    ordersService.SendNotification(order);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("error", "Не удалось внести заявку.");
                }
            }

            return Json(new { Errors = ModelState.IsValid ? null : ModelState.SerializeErrors() });
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }


        [HttpPost]
        [Authorize(Roles = "1")]
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
        [Authorize(Roles = "1")]
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