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

        
        public ActionResult GridView(long? eventid, long? recurrenceId)
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
    }
}