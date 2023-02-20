using Einstein.Domain.Entities;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Einstein.Domain.Services
{
    public class XMLPaymentServiceConfig :XMLBaseService, IPaymentServiceConfig
    {
        private string filename;
        
        
        public XMLPaymentServiceConfig(string _filename)
        {
          
            filename = _filename;
        }

        
        public void SaveSettings(PAYMENTSETTINGS settings)
        {
            
            SaveSettings(settings, typeof(PAYMENTSETTINGS), filename);

        }

        public PAYMENTSETTINGS ReadSettings()
        {
            if (File.Exists(filename))
            {

                PAYMENTSETTINGS settings = (PAYMENTSETTINGS)ReadSettings(typeof(PAYMENTSETTINGS), filename);
                
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
