using Einstein.Domain.Entities;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Einstein.Domain.Services
{
    public class XMLMailServiceConfig : XMLBaseService, IMailServiceConfig
    {
        private string filename;
        private ICryptoService crypto;

        public XMLMailServiceConfig(string _filename, ICryptoService _crypto)
        {
            crypto = _crypto;
            filename = _filename;
        }

       

        public void SaveSettings(MAILSERVICESETTINGS settings)
        {
           
            settings.PASSWORD = crypto.EncryptPassword(settings.PASSWORD);
            SaveSettings(settings, typeof(MAILSERVICESETTINGS), filename);

        }

        public MAILSERVICESETTINGS ReadSettings()
        {
            if (File.Exists(filename))
            {

                MAILSERVICESETTINGS settings = (MAILSERVICESETTINGS)ReadSettings(typeof(MAILSERVICESETTINGS), filename);
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
