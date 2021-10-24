using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Einstein.Domain.Entities
{
    public class EVENT
    {
      
        public EVENT()
        {
            this.Orders = new HashSet<ORDER>();
        }

        public long EventID { get; set; }
        public System.DateTime Start { get; set; }
        public System.DateTime End { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsAllDay { get; set; }
        public string RecurrenceRule { get; set; }
        public Nullable<int> RecurrenceID { get; set; }
        public string RecurrenceException { get; set; }
        public string StartTimezone { get; set; }
        public string EndTimezone { get; set; }
        public int MaxPersons { get; set; }
        [NotMapped]
        public int FreePlaces { get {

                return MaxPersons - Orders.Sum(s => s.PERSONS);
            } }

        public virtual ICollection<ORDER> Orders { get; set; }
        
    }
}
