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
    public class EventsService: ISchedulerEventService<EventViewModel>
    {
       

        private IRepository entities;

        public EventsService(IRepository entities)
        {
            this.entities = entities;
        }


        private HashSet<Occurrence> LoadCalendar(DateTime start, DateTime end)
        {
            var calendar = new Ical.Net.Calendar();
          
            var events = entities.Events.Where(e => (e.Start >= start || e.RecurrenceRule != null)).ToList().Where(e => e.FreePlaces > 0).ToList();

            foreach (var task in events)
            {
                calendar.Events.Add(
                    new CalendarEvent{
                        Start= new CalDateTime(task.Start),
                        End = new CalDateTime(task.End),
                        IsAllDay= task.IsAllDay,
                        Name = task.Title,
                        //Description=EventID|FreePlaces
                        Resources=new List<string>() { task.EventID.ToString(),  task.FreePlaces.ToString() , task.FreePlaces14.ToString(), task.Price.ToString()  },
                        Description =task.Description,
                        RecurrenceRules = String.IsNullOrEmpty(task.RecurrenceRule) ? null: new List<RecurrencePattern> { new RecurrencePattern(task.RecurrenceRule) },
                        ExceptionDates = String.IsNullOrEmpty(task.RecurrenceException) ? null: new List<PeriodList> { ExceptionDates(task.RecurrenceException) }
                        //ExceptionRules = String.IsNullOrEmpty(task.RecurrenceException) ? null : new List<RecurrencePattern> { new RecurrencePattern(task.RecurrenceException) },
                    });
            }

            return calendar.GetOccurrences(new CalDateTime(start), new CalDateTime(end));
        }

        private PeriodList ExceptionDates(string RecurrenceException)
        {
            if (String.IsNullOrEmpty(RecurrenceException))
            {
                return null;

            }
            else
            {
                var result = new PeriodList();
                foreach (string date in RecurrenceException.Split(','))
                {
                    IDateTime loc = new CalDateTime(date);
                    result.Add(new Period(loc.ToTimeZone("W-SU")));
                }
                return result;
            }
        }




        public IEnumerable<AvailableEventsViewModel> GetAvailableEvents(DateTime start, DateTime end)
        {
            var result = new List<AvailableEventsViewModel>();
            //var start = DateTime.Now;
           // var end = DateTime.Now.AddMonths(2);
           
            var avev = LoadCalendar(start, end);

            foreach (Occurrence task in avev)
            {
                CalendarEvent @event = (CalendarEvent)task.Source;
                
                var view = result.FirstOrDefault(e => e.Title == @event.Name);
                if (view == null)
                {
                    view = new AvailableEventsViewModel()
                    {

                        Title = @event.Name,
                        Dates = GetAvailableDates(@event,task.Period, new List<AvailableDatesViewModel>())
                    };
                    result.Add(view);
                }
                else
                {
                    view.Dates = GetAvailableDates(@event,task.Period, view.Dates);
                }
            }
            
            return result;
        }

        private List<AvailableDatesViewModel> GetAvailableDates(CalendarEvent @event, Period period, List<AvailableDatesViewModel> dates)
        {
            if (!(@event.ExceptionDates.Count()>0 && @event.ExceptionDates.First().Any(c => c.StartTime.Value == period.StartTime.Value)))
            {
                var date = dates.FirstOrDefault(d => d.Date == period.StartTime.Date);

                if (date == null)
                {
                    date = new AvailableDatesViewModel()
                    {
                        Date = period.StartTime.Date,
                        Times = GetAvailableTimes(@event, period, new List<AvailableTimesViewModel>())

                    };
                    dates.Add(date);
                }
                else
                {
                    date.Times = GetAvailableTimes(@event, period, date.Times);
                }

            }

            return dates;
        }

        private List<AvailableTimesViewModel> GetAvailableTimes(CalendarEvent @event, Period period, List<AvailableTimesViewModel> times)
        {
            
            var time = times.FirstOrDefault(d => d.Start == period.StartTime.Value && d.End ==period.EndTime.Value);
            if (time == null)
            {

                 time = new AvailableTimesViewModel()
                {
                    Start = period.StartTime.Value,
                    End = period.EndTime.Value,
                    FreePlaces=int.Parse(@event.Resources[1]),
                    FreePlaces14= int.Parse(@event.Resources[2]),
                    EventId =long.Parse(@event.Resources[0]),
                    Price=decimal.Parse(@event.Resources[3])
                };

                times.Add(time);
            }

            return times;
        }

        public EventViewModel ConvertToViewModel(EVENT task, EventViewModel view)
        {

            view.TaskID = task.EventID;
            view.Title = task.Title;
            view.Start = DateTime.SpecifyKind(task.Start, DateTimeKind.Local);
            view.End = DateTime.SpecifyKind(task.End, DateTimeKind.Local);
            view.StartTimezone = task.StartTimezone;
            view.EndTimezone = task.EndTimezone;
            view.Description = task.Description;
            view.IsAllDay = task.IsAllDay;
            view.RecurrenceRule = task.RecurrenceRule;
            view.RecurrenceException = task.RecurrenceException;
            view.RecurrenceID = task.RecurrenceID;
            view.MaxPersons = task.MaxPersons;
            view.MaxPersons14 = task.MaxPersons14;
            view.FreePlaces = task.FreePlaces;
            view.FreePlaces14 = task.FreePlaces14;
            view.Persons = task.Persons;
            view.Persons14 = task.Persons14;
            view.Price = task.Price;
            view.Total = task.Total;
            view.Color = task.Color;
            return view;
    }


        public IList<EventViewModel> GetAll()
        {
            IList<EventViewModel> result =new List<EventViewModel>();

           
            result = entities.Events.ToList().Select(task => ConvertToViewModel(task, new EventViewModel())).ToList();

            return result;
        }

        public IEnumerable<EventViewModel> Read()
        {
            return GetAll();
        }

        public void Insert(EventViewModel appointment, ModelStateDictionary modelState)
        {
            if (ValidateModel(appointment, modelState))
            {
               
                    var entity = appointment.ToEntity(new EVENT());
                    
                    entities.AddEvent(entity);
                    appointment = ConvertToViewModel(entity,appointment);
                
            }
        }

        public void Update(EventViewModel appointment, ModelStateDictionary modelState)
        {
            if (ValidateModel(appointment, modelState))
            {
                if (string.IsNullOrEmpty(appointment.Title))
                {
                    appointment.Title = "";
                }
               
                    var entity = entities.Events.FirstOrDefault(e=>e.EventID == appointment.TaskID);
                    entity= appointment.ToEntity(entity); 
                    entities.UpdateEvent(entity);

                appointment = ConvertToViewModel(entity,appointment);
            }
        }


        public void DeleteOne(EVENT appointment, ModelStateDictionary modelState) 
        {
            if (appointment.Orders.Any())
            {
                modelState.AddModelError("errors", "По мероприятию "+appointment.Title+" на "+appointment.Start.ToString("dd.MM.yyyy HH:ss") +" уже созданы заявки. Удаление невозможно!");
            }
            else {

                entities.DeleteEvent(appointment);
            }
        }
        public void Delete(EventViewModel appointment, ModelStateDictionary modelState)
        {
            
                var entity = entities.Events.FirstOrDefault(ev => ev.EventID == appointment.TaskID);

                var recurrenceExceptions = entities.Events.Where(t => t.RecurrenceID == appointment.TaskID);

                foreach (var recurrenceException in recurrenceExceptions)
                {
                
                    DeleteOne(recurrenceException,modelState);
                }


                DeleteOne(entity,modelState);
            
        }

        private bool ValidateModel(EventViewModel appointment, ModelStateDictionary modelState)
        {
            var result = true;

            if (string.IsNullOrEmpty(appointment.Title))
            {
                modelState.AddModelError("errors", "Заголовок обязателен к заполнению.");
                result= false;
            }
            if (appointment.Start > appointment.End)
            {
                modelState.AddModelError("errors", "Дата начала мероприятия должна быть меньше даты окончания");
                result= false;
            }

           

            if (appointment.Persons > appointment.MaxPersons)
            {
                modelState.AddModelError("errors", String.Format("Максимальное количество мест не должно быть меньше заявленых {0}.", appointment.Persons));
                result = false;
            }

            if (appointment.Persons14 > appointment.MaxPersons14)
            {
                modelState.AddModelError("errors", String.Format("Максимальное количество мест для детей до 14 не должно быть меньше заявленых {0}.", appointment.Persons14));
                result = false;
            }

            return result;
        }

        public EventViewModel One(Func<EventViewModel, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Dispose()
        {
            entities.Dispose();
        }


    }
}
   