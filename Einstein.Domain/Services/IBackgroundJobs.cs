using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Einstein.Domain.Services
{
    public interface IBackgroundJobs
    {
        void MailingOrders(List<Sheet> sheets);
        DateTime GetDateBegin();
        DateTime GetDateEnd();
    }
}
