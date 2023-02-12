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

        public virtual DbSet<PAYMENT> Payments { get; set; }
        
        public virtual DbSet<USER> Users { get; set; }
        public virtual DbSet<ROLE> Roles { get; set; }
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
        public IQueryable<USER> Users
        {
            get
            {
                return db.Users;
            }
        }
        public IQueryable<ROLE> Roles
        {
            get
            {
                return db.Roles;
            }
        }
        public IQueryable<PAYMENT> Payments
        {
            get
            {
                return db.Payments;
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

        public void AddPayment(PAYMENT payment)
        {
            var entity = db.Payments.FirstOrDefault(p => p.PAYMENTID == payment.PAYMENTID && p.STATUS == payment.STATUS);
            if (entity == null)
            {
                db.Payments.Add(payment);
            }
            else
            {
                entity.DATE = payment.DATE;
                db.Entry(entity).State = EntityState.Modified;
            }

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

        public void AddUser(USER user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void UpdateUser(USER user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteUser(USER user)
        {
            db.Users.Remove(user);
            db.SaveChanges();
        }
    }
}
