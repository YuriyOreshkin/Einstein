using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Einstein.Domain.Entities
{
    public class ORDER
    {
        public long ID { get; set; }
        public DateTime DATE { get; set; }
        public long EVENTID { get; set; }
        public short PERSONS { get; set; }
        public short PERSONS14 { get; set; }
        public string PHONE { get; set; }
        public string EMAIL { get; set; }
        public bool INFORM { get; set; }

        public string DESCRIPTION { get; set; }
        public decimal PREPAY { get; set; }
        public virtual EVENT EVENT { get; set; }
    }
}
