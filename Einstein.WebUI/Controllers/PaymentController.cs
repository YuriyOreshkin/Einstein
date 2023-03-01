using Einstein.Domain.Services;
using Einstein.WebUI.Extensions;
using Einstein.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Einstein.WebUI.Controllers
{
   
  
    public class PaymentController : ApiController
    {
        private OrdersService ordersService;
        public PaymentController(IRepository _repository, IOrderSender sender, IPaymentServiceConfig payservice, IBackgroundJobs backgroundJobs)
        {
            ordersService = new OrdersService(_repository, sender, payservice,backgroundJobs);
        }
        // GET: api/Default



        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public HttpResponseMessage Confirm([FromBody] PaymentNotificationViewModel payment)
        {
            if (ordersService.Confirm(payment))
            {

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent("OK");
                response.Headers.Add("Access-Control-Allow-Origin", "*");
                //var message = String.Format("Поступила оплата по заказу № {0}", payment.OrderId);
                //Services.OrderServiceController.SendSignal(message);
                
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, "ERROR"); 
            }
            
        }
    }
}
