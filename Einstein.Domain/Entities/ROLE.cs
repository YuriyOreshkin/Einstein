using System;
using System.Collections.Generic;
using System.Text;

namespace Einstein.Domain.Entities
{
    public class ROLE
    {
        public ROLE() 
        {
            Users = new HashSet<USER>();
        }
        public short ID { get; set; }
        public string NAME { get; set; }
        public virtual ICollection<USER> Users { get; set; }
    }
}
