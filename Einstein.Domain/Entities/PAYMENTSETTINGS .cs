using System;
using System.Collections.Generic;
using System.Text;

namespace Einstein.Domain.Entities
{
    public class PAYMENTSETTINGS
    {
        public bool ENABLE { get; set; }
        public string TERMINALID { get; set; }

        public bool FRAME { get; set; }

        public string LANGUAGE { get; set; }
    }
}
