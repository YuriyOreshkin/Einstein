using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Einstein.WebUI.Models
{
    public class AvailableEventsViewModel
    {
        public string Title { get; set; }
        public List<AvailableDatesViewModel> Dates { get; set; }
        
    }
}