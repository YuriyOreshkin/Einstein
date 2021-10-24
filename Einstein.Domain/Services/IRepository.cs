using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Einstein.Domain.Services
{
    public interface IRepository:IDisposable
    {
        IQueryable<EVENT> Events { get; }
        void AddEvent(EVENT task);
        void UpdateEvent(EVENT task);
        void DeleteEvent(EVENT task);
        IQueryable<ORDER> Orders { get; }
        void AddOrder(ORDER order);
        void UpdateOrder(ORDER order);
        void DeleteOrder(ORDER order);
         
        
        }
}
