using Einstein.Domain.Entities;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Einstein.Domain.Services
{
    public class XMLMailServiceConfig : IMailServiceConfig
    {
        private string filename;
        private ICryptoService crypto;
        private IXmlService xml;
        public XMLMailServiceConfig(string _filename, IXmlService _xml, ICryptoService _crypto)
        {
            crypto = _crypto;
            filename = _filename;
            xml = _xml;
        }

       

        public void SaveSettings(MAILSERVICESETTINGS settings)
        {
           
            settings.PASSWORD = crypto.EncryptPassword(settings.PASSWORD);
            xml.SaveSettings(settings, typeof(MAILSERVICESETTINGS), filename);

        }

        public MAILSERVICESETTINGS ReadSettings()
        {
            if (xml.FileExist(filename))
            {

                MAILSERVICESETTINGS settings = (MAILSERVICESETTINGS)xml.ReadSettings(typeof(MAILSERVICESETTINGS), filename);
                settings.PASSWORD = crypto.DecryptPassword(settings.PASSWORD);
                return settings;
            }
            else
            {
                MAILSERVICESETTINGS settings = new MAILSERVICESETTINGS
                {
                    ENABLE =false,
                    HOST = " smtp.yandex.ru",
                    PORT=25,
                    USER="somemail@yandex.ru",
                    PASSWORD= ""

                };

                SaveSettings(settings);

                return settings;
            }

        }
       
    }
}
