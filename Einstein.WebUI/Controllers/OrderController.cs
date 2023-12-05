using Einstein.Domain.Services;
using Einstein.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Einstein.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private EventsService service;
        private ITermsService terms;
        IPaymentServiceConfig payment;
        public OrderController(IRepository _repository,ITermsService _terms,IPaymentServiceConfig _payment)
        {
            service = new EventsService(_repository);
            terms = _terms;
            payment = _payment;
        }

        public ActionResult Index(string id)
        {
            var settings = payment.ReadSettings();
            ViewBag.Pay = settings != null ? settings.ENABLE : false;
            ViewBag.Event = id;
            return View(new OrderViewModel());
        }
       
        public ActionResult Terms()
        {

            return View("Terms", terms.GetTemplate() as Object);
        }

        public ActionResult ListView() 
        {

            return View();
        }

        [Authorize]
        public ActionResult GridView(long? eventid)
        {
            
            if (eventid.HasValue)
            {
                EventViewModel  @event =service.GetAll().FirstOrDefault(e => e.TaskID == eventid);
                if (@event != null)
                {
                    return View(@event);
                }
            }
            return View(new EventViewModel());
        }

        [Authorize]
        public ActionResult GridViewByDate(int? recurrenceId,string start)
        {

            if (recurrenceId.HasValue)
            {
                EventViewModel @event = service.GetAll().FirstOrDefault(e => e.TaskID == recurrenceId);
                if (@event != null)
                {
                    @event.Start = DateTime.Parse(start);
                    @event.RecurrenceID = recurrenceId;
                    //@event.TaskID = 0;
                    return View("GridView",@event);
                }
            }
            return View("GridView", new EventViewModel());
        }
    }
}