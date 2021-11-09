using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Einstein.Domain.Services
{
    public interface IMailServiceConfig
    {
        MAILSERVICESETTINGS ReadSettings();
        void SaveSettings(MAILSERVICESETTINGS settings);
     
    }
}
