using Einstein.Domain.Entities;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Einstein.Domain.Services
{
    public class XMLMailingServiceConfig : XMLBaseService, IMailingServiceConfig
    {
        private string filename;
     

        public XMLMailingServiceConfig(string _filename)
        {
            
            filename = _filename;
        }

       

        public void SaveSettings(MAILINGSERVICESETTINGS settings)
        {
            
            SaveSettings(settings, typeof(MAILINGSERVICESETTINGS), filename);

        }

        public MAILINGSERVICESETTINGS ReadSettings()
        {
            if (File.Exists(filename))
            {
                MAILINGSERVICESETTINGS settings = (MAILINGSERVICESETTINGS)ReadSettings(typeof(MAILINGSERVICESETTINGS), filename);
                return settings;
            }
            else
            {
                MAILINGSERVICESETTINGS settings = new MAILINGSERVICESETTINGS
                {
                    ENABLE =false,
                    BEGINFROMTODAY = 0,
                    ENDFROMTODAY=1,
                    RECIPIENTS="somemail@yandex.ru",

                };

                SaveSettings(settings);

                return settings;
            }

        }
       
    }
}
