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
        public OrderController(IRepository _repository)
        {
            service = new EventsService(_repository);
        }

        public ActionResult Index()
        {
            return View(new OrderViewModel());
        }

        
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