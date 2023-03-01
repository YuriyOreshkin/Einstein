using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Einstein.Domain.Services
{
    public interface ISender
    {
        void SendEMail(string to, string subject, string body, string attachment = null);

        bool IsEnable();
    }
}
