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
        [Required]
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
        [Required]
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

        [DisplayName("Мах кол-во человек")]
        [Required]
        [Range(1, int.MaxValue)]
        public int MaxPersons { get; set; }

        public int FreePlaces { get; set; }

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

       
        public EVENT ToEntity()
        {
            return new EVENT
            {
                EventID = TaskID,
                Title = Title,
                Start = Start,
                End = End,
                Description = Description,
                RecurrenceRule = RecurrenceRule,
                RecurrenceException = RecurrenceException,
                StartTimezone = StartTimezone,
                EndTimezone = EndTimezone,
                RecurrenceID = RecurrenceID,
                IsAllDay = IsAllDay,
                MaxPersons = MaxPersons
            };
        }

    }
}