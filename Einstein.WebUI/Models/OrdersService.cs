using Einstein.Domain.Entities;
using Einstein.Domain.Services;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Einstein.WebUI.Models
{
    public class OrdersService 
    {

        private IRepository entities;
        private IMailSender sender;
        public OrdersService(IRepository entities, IMailSender sender)
        {
            this.entities = entities;
            this.sender = sender;
        }


        private OrderViewModel ConvertToViewModel(ORDER order, OrderViewModel view)
        {

            view.id = order.ID;
            view.phonenumber = order.PHONE;
            view.email = order.EMAIL;
            view.eventname = order.EVENT.Title;
            view.eventid = order.EVENTID;
            view.dateorder = order.DATE;
            view.persons = order.PERSONS;
            view.persons14 = order.PERSONS14;
            view.dateevent = order.EVENT.Start.Date;
            view.timeevent = order.EVENT.Start.ToString("HH:mm") + " - " + order.EVENT.End.ToString("HH:mm");
            view.freeplaces = order.EVENT.FreePlaces;
            view.freeplaces14 = order.EVENT.FreePlaces14;
            view.inform = order.INFORM;

            return view;

        }
       

        public IList<OrderViewModel> GetAll()
        {
            IList<OrderViewModel> result  = entities.Orders.ToList().Select(ord => ConvertToViewModel(ord, new OrderViewModel())).ToList();

            return result;
        }


        public void SendNitification(OrderViewModel orderview)
        {
            sender.SendOrder(orderview);
        }


        public void Insert(OrderViewModel orderview, ModelStateDictionary modelState)
        {
            if (ValidateModel(orderview, modelState))
            {
                orderview.dateorder = DateTime.Now;
                orderview.eventid = GetOrderEventId(orderview);
                var entity = orderview.ToEntity(new ORDER());
                entities.AddOrder(entity);

                //orderview.id = entity.ID;
                //orderview.freeplaces = entities.Events.First(e => e.EventID == orderview.eventid).FreePlaces;
                orderview = ConvertToViewModel(entity, orderview);

                //sender.SendOrder(orderview);

            }
        }


        private long GetOrderEventId(OrderViewModel order)
        {
            var evententity = entities.Events.FirstOrDefault(e => e.EventID == order.eventid);
            if (evententity != null)
            {
                if (!String.IsNullOrEmpty(evententity.RecurrenceRule))
                {
                    var newevent = new EVENT
                    {
                        Title = evententity.Title,
                        Start = new DateTime(order.dateevent.Year, order.dateevent.Month, order.dateevent.Day, evententity.Start.TimeOfDay.Hours, evententity.Start.TimeOfDay.Minutes, evententity.Start.TimeOfDay.Milliseconds),
                        End = new DateTime(order.dateevent.Year, order.dateevent.Month, order.dateevent.Day, evententity.End.TimeOfDay.Hours, evententity.End.TimeOfDay.Minutes, evententity.End.TimeOfDay.Milliseconds),
                        IsAllDay = evententity.IsAllDay,
                        //RecurrenceID = Convert.ToInt32(evententity.EventID),
                        MaxPersons = evententity.MaxPersons,
                        StartTimezone = evententity.StartTimezone,
                        EndTimezone = evententity.EndTimezone
                    };
                    entities.AddEvent(newevent);
                    var recException = newevent.Start.ToUniversalTime().ToString("yyyyMMddTHHmmssZ");
                    evententity.RecurrenceException = String.IsNullOrEmpty(evententity.RecurrenceException) ? recException : evententity.RecurrenceException.Contains(recException) ? evententity.RecurrenceException : evententity.RecurrenceException + "," + recException;
                    entities.UpdateEvent(evententity);

                    return newevent.EventID;
                }
                else
                {
                    return evententity.EventID;
                }
                
            }
            return 0;
        }
           
        public void Update(OrderViewModel orderview, ModelStateDictionary modelState)
        {
            if (ValidateModel(orderview, modelState))
            {

                orderview.eventid = GetOrderEventId(orderview);

                var entity = entities.Orders.FirstOrDefault(o => o.ID == orderview.id);
                if (entity != null)
                {
                    entity = orderview.ToEntity(entity);
                    entities.UpdateOrder(entity);

                    orderview = ConvertToViewModel(entity, orderview);
                    //orderview.freeplaces = entities.Events.First(e => e.EventID == orderview.eventid).FreePlaces;
                }
                else {

                    modelState.AddModelError("errors", "Заявка не найдена в базе данных!");
                }
            }
        }

        public void Delete(OrderViewModel orderview, ModelStateDictionary modelState)
        {
               var entity = entities.Orders.FirstOrDefault(o => o.ID == orderview.id);
                entities.DeleteOrder(entity);
        }

        private bool ValidateModel(OrderViewModel orderview, ModelStateDictionary modelState)
        {
            var result = true;

            if (String.IsNullOrEmpty(orderview.phonenumber) && String.IsNullOrEmpty(orderview.email))
            {
                modelState.AddModelError("errors", "Хотя бы один из контактов (почта или телефон) должен быть заполнен.");
                result = false;
            }
            EVENT eVENT = entities.Events.FirstOrDefault(e => e.EventID == orderview.eventid);
            if (eVENT  == null )
            {
                modelState.AddModelError("errors", "Мероприятие '" + orderview.eventname + "' на дату " + orderview.dateevent.ToShortDateString() + " и время " + orderview.timeevent + " не обнаружено.");
                result = false;
            }

            if (eVENT != null && orderview.persons > orderview.freeplaces)
            {
                modelState.AddModelError("errors", "Количество человек("+orderview.persons+") превышает количество свободных мест ("+ orderview.freeplaces + ")");
                return false;
            }
            return result;
        }

        public OrderViewModel One(Func<OrderViewModel, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            entities.Dispose();
        }


    }
}
   