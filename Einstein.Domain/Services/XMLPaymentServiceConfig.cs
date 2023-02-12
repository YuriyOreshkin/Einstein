using Einstein.Domain.Entities;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Einstein.Domain.Services
{
    public class XMLPaymentServiceConfig : IPaymentServiceConfig
    {
        private string filename;
        
        
        public XMLPaymentServiceConfig(string _filename)
        {
          
            filename = _filename;
        }

        
        public void SaveSettings(PAYMENTSETTINGS settings)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(PAYMENTSETTINGS));
            TextWriter writer = new StreamWriter(filename, false, Encoding.GetEncoding(1251));
           
            formatter.Serialize(writer, settings);
            
            writer.Close();

        }

        public PAYMENTSETTINGS ReadSettings()
        {
            if (File.Exists(filename))
            {

                XmlSerializer formatter = new XmlSerializer(typeof(PAYMENTSETTINGS));

                using (StreamReader fs = new StreamReader(filename, Encoding.GetEncoding(1251), false))
                {
                    PAYMENTSETTINGS settings = (PAYMENTSETTINGS)formatter.Deserialize(fs);
                    fs.Close();
                    
                    return settings;
                }
            }
            else
            {
                PAYMENTSETTINGS settings = new PAYMENTSETTINGS
                {
                    ENABLE = false,
                    TERMINALID = "TinkoffBankTest",
                    FRAME=false,
                    LANGUAGE= "ru"


                };

                SaveSettings(settings);

                return settings;
            }

        }

        public void Save(string text)
        {
            File.WriteAllText(Path.Combine(Path.GetDirectoryName(filename),"payresponce.txt"), text);
        }
    }
}
