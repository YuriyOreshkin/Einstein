using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Einstein.WebUI.Models
{
    public class TemplateSettingsViewModel
    {
        public string id { get; set; }
        public string anchor { get; set; }
        public string title { get; set; }

        public string filename { get; set; }
    }
}