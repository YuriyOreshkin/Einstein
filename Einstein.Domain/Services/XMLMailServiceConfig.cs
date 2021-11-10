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
        private const string salt = "агслнщг";
        
        public XMLMailServiceConfig(string _filename)
        {
          
            filename = _filename;
        }

        private byte[] GetEntropy(string EntropyString)
        {

            MD5 md5 = MD5.Create();
            return md5.ComputeHash(Encoding.UTF8.GetBytes(EntropyString));
        }

        private string EncryptPassword(string ProxyPassword)
        {
            if (string.IsNullOrEmpty(ProxyPassword)) return ProxyPassword;
            //if (string.IsNullOrEmpty(ProxyUser))
            //    return false;

            byte[] entropy = GetEntropy(salt);
            byte[] pass = Encoding.UTF8.GetBytes(ProxyPassword);
            byte[] crypted = ProtectedData.Protect(pass,
                entropy, DataProtectionScope.LocalMachine);
            ProxyPassword = Convert.ToBase64String(crypted);

            return ProxyPassword;
        }

        private string DecryptPassword(string ProxyPassword)
        {
            if (string.IsNullOrEmpty(ProxyPassword)) return ProxyPassword;
           

            byte[] pass = null;
            
                pass = Convert.FromBase64String(ProxyPassword);
            
            byte[] entropy = GetEntropy(salt);

                pass = ProtectedData.Unprotect(pass, entropy,
                        DataProtectionScope.LocalMachine);
            
            ProxyPassword = Encoding.UTF8.GetString(pass);

            return ProxyPassword;
        }

        public void SaveSettings(MAILSERVICESETTINGS settings)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(MAILSERVICESETTINGS));
            TextWriter writer = new StreamWriter(filename, false, Encoding.GetEncoding(1251));
            settings.PASSWORD = EncryptPassword(settings.PASSWORD);
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
                    settings.PASSWORD = DecryptPassword(settings.PASSWORD);
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
