using System;
using System.Collections.Generic;
using System.Text;

namespace Einstein.Domain.Entities
{
    public class MAILSERVICESETTINGS
    {
        public bool ENABLE { get; set; }
        public string HOST { get; set; }
        public int PORT { get; set; }
        public string USER { get; set; }
        public string PASSWORD { get; set; }

    }
}
