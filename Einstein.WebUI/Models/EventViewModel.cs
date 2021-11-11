using Einstein.Domain.Entities;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Einstein.WebUI.Models
{
    public class EventViewModel: ISchedulerEvent
    {
        public long TaskID { get; set; }
        [Required(ErrorMessage ="Требуется поле {0}")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }
        [DisplayName("Описание")]
        public string Description { get; set; }

        
        private DateTime start;
        [Required]
        [DisplayName("Начало")]
        public DateTime Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
            }
        }

        public string StartTimezone { get; set; }

        private DateTime end;
        [Required(ErrorMessage = "Требуется поле {0}")]
        [DisplayName("Окончание")]
        public DateTime End
        {
            get
            {
                return end;
            }
            set
            {
                end = value;
            }
        }

        public string EndTimezone { get; set; }
        [DisplayName("Повтор")]
        public string RecurrenceRule { get; set; }
        [DisplayName("Повтор")]
        public int? RecurrenceID { get; set; }

        public string RecurrenceException { get; set; }
        [DisplayName("Весь день")]
        public bool IsAllDay { get; set; }

        [DisplayName("Максимальное кол-во посетителей")]
        [Required(ErrorMessage = "Требуется поле {0}")]
        [Range(1, int.MaxValue,ErrorMessage = "Значение поля {0} должно быть в диапазоне от {1} до {2}")]
        public int MaxPersons { get; set; }
        [DisplayName("Максимальное кол-во детей до 14")]
        [Required(ErrorMessage = "Требуется поле {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Значение поля {0} должно быть в диапазоне от {1} до {2}")]
        public int MaxPersons14 { get; set; }

        public int Persons { get; set; }
        public int FreePlaces { get; set; }
        public int Persons14 { get; set; }
        public int FreePlaces14 { get; set; }

        private bool isAllDay;
        bool ISchedulerEvent.IsAllDay
        {
            get
            {
                return this.isAllDay;
            }

            set
            {
                this.isAllDay = value;
            }
        }


        public EVENT ToEntity(EVENT @event)
        {

            @event.EventID = TaskID;
            @event.Title = Title;
            @event.Start = Start;
            @event.End = End;
            @event.Description = Description;
            @event.RecurrenceRule = RecurrenceRule;
            @event.RecurrenceException = RecurrenceException;
            @event.StartTimezone = StartTimezone;
            EndTimezone = EndTimezone;
            @event.RecurrenceID = RecurrenceID;
            @event.IsAllDay = IsAllDay;
            @event.MaxPersons = MaxPersons;
            @event.MaxPersons14 = MaxPersons14;
            return @event;

        }

    }
}