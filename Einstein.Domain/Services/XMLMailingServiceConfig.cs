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
     

        public XMLMailingServiceConfig(string _filename)
        {
            
            filename = _filename;
        }

       

        public void SaveSettings(MAILINGSERVICESETTINGS settings)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(MAILINGSERVICESETTINGS));
            TextWriter writer = new StreamWriter(filename, false, Encoding.GetEncoding(1251));
          
            formatter.Serialize(writer, settings);
            
            writer.Close();

        }

        public MAILINGSERVICESETTINGS ReadSettings()
        {
            if (File.Exists(filename))
            {

                XmlSerializer formatter = new XmlSerializer(typeof(MAILINGSERVICESETTINGS));

                using (StreamReader fs = new StreamReader(filename, Encoding.GetEncoding(1251), false))
                {
                    MAILINGSERVICESETTINGS settings = (MAILINGSERVICESETTINGS)formatter.Deserialize(fs);
                    fs.Close();
                    return settings;
                }
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
