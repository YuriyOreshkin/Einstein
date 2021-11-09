using Einstein.Domain.Entities;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Einstein.Domain.Services
{
    public class XMLMailServiceConfig : IMailServiceConfig
    {
        private string filename;

        
        public XMLMailServiceConfig(string _filename)
        {
          
            filename = _filename;
        }

        public void SaveSettings(MAILSERVICESETTINGS settings)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(MAILSERVICESETTINGS));
            TextWriter writer = new StreamWriter(filename, false, Encoding.GetEncoding(1251));
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
                    PASSWORD= "mypassword"

                };

                SaveSettings(settings);

                return settings;
            }

        }
       
    }
}
