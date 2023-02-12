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

        public XMLMailServiceConfig(string _filename, ICryptoService _crypto)
        {
            crypto = _crypto;
            filename = _filename;
        }

       

       

        public void SaveSettings(MAILSERVICESETTINGS settings)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(MAILSERVICESETTINGS));
            TextWriter writer = new StreamWriter(filename, false, Encoding.GetEncoding(1251));
            settings.PASSWORD = crypto.EncryptPassword(settings.PASSWORD);
            formatter.Serialize(writer, settings);
            
            writer.Close();

        }

        public MAILSERVICESETTINGS ReadSettings()
        {
            if (File.Exists(filename))
            {

                XmlSerializer formatter = new XmlSerializer(typeof(MAILSERVICESETTINGS));

                using (StreamReader fs = new StreamReader(filename, Encoding.GetEncoding(1251), false))
                {
                    MAILSERVICESETTINGS settings = (MAILSERVICESETTINGS)formatter.Deserialize(fs);
                    fs.Close();
                    settings.PASSWORD = crypto.DecryptPassword(settings.PASSWORD);
                    return settings;
                }
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
