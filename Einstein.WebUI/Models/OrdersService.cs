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
using Newtonsoft.Json;

namespace Einstein.WebUI.Models
{
    public class OrdersService 
    {

        private IRepository entities;
        private IMailSender sender;
        IPaymentServiceConfig payservice;
        public OrdersService(IRepository entities, IMailSender sender,IPaymentServiceConfig payservice)
        {
            this.entities = entities;
            this.sender = sender;
            this.payservice = payservice;
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
            view.price = order.EVENT.Price;
            view.inform = order.INFORM;
            view.prepay = order.PREPAY;
            view.description = order.DESCRIPTION;
            

            return view;

        }
       

        public IList<OrderViewModel> GetAll()
        {
            IList<OrderViewModel> result  = entities.Orders.ToList().Select(ord => ConvertToViewModel(ord, new OrderViewModel())).ToList();

            return result;
        }


        public void SendNotification(OrderViewModel orderview)
        {
            var entity = entities.Orders.FirstOrDefault(o => o.ID == orderview.id);
            if (entity != null)
            {
                try
                {
                    sender.SendOrder(orderview);

                    orderview.inform = true;
                    entity.INFORM = true;

                }
                catch (Exception ex)
                {
                    entity.INFORM = false;
                    orderview.inform = false;
                }
                finally
                {

                    entities.UpdateOrder(entity);
                }
            }
        }


        public bool Confirm(PaymentNotificationViewModel payment)
        {
            var orderid = long.Parse(payment.OrderId);
            var entity = entities.Orders.FirstOrDefault(o => o.ID ==orderid );

            try
            {
                if (entity != null && int.Parse(payment.ErrorCode) == 0 && payment.Status == "CONFIRMED")
                {
                    if (!entities.Payments.Any(p => p.PAYMENTID == payment.PaymentId && p.STATUS== payment.Status))
                    {
                        entity.PREPAY = entity.PREPAY + payment.Amount / 100;
                        entities.UpdateOrder(entity);
                      
                    }
                }
                entities.AddPayment(payment.ToEntity(new PAYMENT()));
            }
            catch (Exception ex)
            {
                return false;
            }

            
            return true;
        }
        public PaymentFormViewModel InitPayment(OrderViewModel order) 
        {
            var payment = payservice.ReadSettings();

            return new PaymentFormViewModel
            {
                terminalkey = new ValuePaymentViewModel() { value = payment.TERMINALID },
                order = new ValuePaymentViewModel() { value = order.id.ToString() },
                frame = new ValuePaymentViewModel() { value = payment.FRAME.ToString() },
                language = new ValuePaymentViewModel() { value =String.IsNullOrEmpty(payment.LANGUAGE) ? "ru" : payment.LANGUAGE },
                email = new ValuePaymentViewModel() { value = order.email },
                phone = new ValuePaymentViewModel() { value = order.phonenumber },
                amount = new ValuePaymentViewModel() { value = (order.amount-order.prepay).ToString() },
                description = new ValuePaymentViewModel() { value =order.eventname}
            };
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

                //SendNotification(orderview);
               
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
                        MaxPersons14= evententity.MaxPersons14,
                        StartTimezone = evententity.StartTimezone,
                        EndTimezone = evententity.EndTimezone,
                        Price=evententity.Price
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

            if (eVENT != null)
            {
                var freeplaces =eVENT.MaxPersons -eVENT.Orders.Where(o => o.ID != orderview.id).Sum(s => s.PERSONS);
                if (freeplaces < orderview.persons)
                {
                    modelState.AddModelError("errors", String.Format("К сожалению, свободных мест осталось только {0}", freeplaces));

                    result = false;
                }
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
   