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
        public int MaxPersons14 { get; set; }

        public decimal Price { get; set; }

        [NotMapped]
        public int FreePlaces
        { get
            {

                return MaxPersons - Persons;
            }
        }

        [NotMapped]
        public int Persons
        {
            get
            {
                return  Orders.Sum(s => s.PERSONS);
            }
        }

        [NotMapped]
        public int FreePlaces14
        {
            get
            {
               
                //if ((MaxPersons14 - Persons14) < FreePlaces)
                //{
                    return MaxPersons14 - Persons14;
                //}
                //else {
                //    if (FreePlaces > 0)
                //    {

                //        return FreePlaces - 1;
                //    }
                //    else {

                //        return FreePlaces;
                //    }
                //}
            }
        }

        [NotMapped]
        public int Persons14
        {
            get
            {
                return Orders.Sum(s => s.PERSONS14);
            }
        }

        [NotMapped]
        public decimal Total
        {
            get
            {
                return Orders.Sum(s => s.PREPAY);
            }
        }
        public virtual ICollection<ORDER> Orders { get; set; }
        
    }
}
