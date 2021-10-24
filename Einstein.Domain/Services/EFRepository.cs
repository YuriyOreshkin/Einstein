using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Einstein.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Einstein.Domain.Services
{

    public partial class DBEntities : DbContext
    {
        public DBEntities()
            : base("name=EinsteinEntities")
        {
        }

       

        public virtual DbSet<EVENT> Events { get; set; }
        public virtual DbSet<ORDER> Orders { get; set; }
        //public virtual DbSet<Mail> Mails { get; set; }
        //public virtual DbSet<Person> People { get; set; }
        //public virtual DbSet<Task> Tasks { get; set; }
    }

    public class EFRepository : IRepository, IDisposable
    {
        private DBEntities db = new DBEntities();
        private bool disposed = false;
        public IQueryable<EVENT> Events
        {
            get
            {
                return db.Events;
            }
        }

        public IQueryable<ORDER> Orders
        {
            get
            {
                return db.Orders;
            }
        }

        public void AddEvent(EVENT task)
        {
            db.Events.Add(task);
            db.SaveChanges();
        }

        public void AddOrder(ORDER order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
        }
        public void DeleteEvent(EVENT task)
        {
            db.Events.Remove(task);
            db.SaveChanges();
        }

        public void UpdateEvent(EVENT task)
        {
            db.Entry(task).State = EntityState.Modified;
            db.SaveChanges();

        }

        public void UpdateOrder(ORDER order)
        {
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();

        }
        public void DeleteOrder(ORDER order)
        {
            db.Orders.Remove(order);
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
