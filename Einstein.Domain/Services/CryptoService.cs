using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Einstein.Domain.Services
{
    public class CryptoService: ICryptoService
    {
        private const string salt = "агслнщг";

        private byte[] GetEntropy(string EntropyString)
        {

            MD5 md5 = MD5.Create();
            return md5.ComputeHash(Encoding.UTF8.GetBytes(EntropyString));
        }

        public string EncryptPassword(string ProxyPassword)
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
        public string DecryptPassword(string ProxyPassword)
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
    }
}
