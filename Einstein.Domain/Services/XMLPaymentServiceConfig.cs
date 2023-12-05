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
        private IXmlService xml;

        public XMLPaymentServiceConfig(string _filename, IXmlService _xml)
        {
          
            filename = _filename;
            xml = _xml;
        }

        
        public void SaveSettings(PAYMENTSETTINGS settings)
        {
            
            xml.SaveSettings(settings, typeof(PAYMENTSETTINGS), filename);

        }

        public PAYMENTSETTINGS ReadSettings()
        {
            if (xml.FileExist(filename))
            {

                PAYMENTSETTINGS settings = (PAYMENTSETTINGS)xml.ReadSettings(typeof(PAYMENTSETTINGS), filename);
                
                return settings;
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
