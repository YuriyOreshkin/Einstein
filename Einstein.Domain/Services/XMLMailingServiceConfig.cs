using Einstein.Domain.Entities;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Einstein.Domain.Services
{
    public class XMLMailingServiceConfig : IMailingServiceConfig
    {
        private string filename;
        private IXmlService xml;

        public XMLMailingServiceConfig(string _filename, IXmlService _xml)
        {
            
            filename = _filename;
            xml = _xml;
        }


        public void SaveSettings(MAILINGSERVICESETTINGS settings)
        {
            
            xml.SaveSettings(settings, typeof(MAILINGSERVICESETTINGS), filename);

        }

        public MAILINGSERVICESETTINGS ReadSettings()
        {
            if (xml.FileExist(filename))
            {
                MAILINGSERVICESETTINGS settings = (MAILINGSERVICESETTINGS)xml.ReadSettings(typeof(MAILINGSERVICESETTINGS), filename);
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
