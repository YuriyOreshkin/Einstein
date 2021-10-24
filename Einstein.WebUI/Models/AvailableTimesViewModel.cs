using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Einstein.WebUI.Models
{
    public class AvailableTimesViewModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int FreePlaces { get; set; }
        public long EventId { get; set; }
        public string Value { get
            {
                return Start.ToString("HH:mm") + " - " + End.ToString("HH:mm");
            }
        }
    }
}