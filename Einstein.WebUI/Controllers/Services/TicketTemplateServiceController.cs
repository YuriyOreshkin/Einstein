using Einstein.Domain.Services;
using Einstein.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Einstein.WebUI.Controllers.Services
{
    public class TicketTemplateServiceController : TemplateServiceController
    {
        ITemplateService service;
        public TicketTemplateServiceController(ITemplateService _service) : base(_service)
        {
            service = _service;
        }

        public ActionResult Ticket(OrderViewModel order)
        {
            return View("~/Views/Order/Ticket.cshtml", new { body = service.GetTemplateBody(order), orderid=order.id, topay=order.amount-order.prepay });
        }

    }
}