using System;
using System.Collections.Generic;
using System.Text;

namespace Einstein.Domain.Entities
{
    public class MAILINGSERVICESETTINGS
    {
        public bool ENABLE { get; set; }
        public short BEGINFROMTODAY { get; set; }
        public short ENDFROMTODAY { get; set; }
        public string RECIPIENTS { get; set; }

    }
}
