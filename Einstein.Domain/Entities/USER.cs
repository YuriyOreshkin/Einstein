using System;
using System.Collections.Generic;
using System.Text;

namespace Einstein.Domain.Entities
{
    public class USER
    {
        public int ID { get; set; }
        public string LOGIN { get; set; }
        public string PASSWORD { get; set; }

        public short ROLEID {get;set;}

        public virtual ROLE ROLE{ get; set; }
    }
}
