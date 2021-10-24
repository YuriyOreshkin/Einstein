using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Einstein.WebUI.Models
{
    public class AvailableDatesViewModel
    {
        public DateTime Date { get; set; }
        public List<AvailableTimesViewModel> Times{get;set;}
    }
}