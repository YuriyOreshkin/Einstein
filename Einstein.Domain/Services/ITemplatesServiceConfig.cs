using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Einstein.Domain.Services
{
    public interface ITemplatesServiceConfig
    {
        TEMPLATES ReadSettings();
        void SaveSettings(TEMPLATES templates);
     
    }
}
