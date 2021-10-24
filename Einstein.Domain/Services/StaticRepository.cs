using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Einstein.Domain.Services
{
    public class StaticRepository : IRepository
    {
        private List<CATEGORY> categories;
        public StaticRepository()
        {
            categories = new List<CATEGORY> {
                new CATEGORY { ID=1, NAME ="Олимпиада"},
                new CATEGORY { ID=2, NAME= "Квест"}
            };
        }

        public IQueryable<CATEGORY> OPENCATEGORIES()
        {
            return categories.AsQueryable();
        }
    }
}
