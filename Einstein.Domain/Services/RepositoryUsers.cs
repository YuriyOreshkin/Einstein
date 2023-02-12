using Einstein.Domain.Entities;
using System;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Einstein.Domain.Services
{
    public class RepositoryUsers : IUsers
    {
        private IRepository repos;
        private ICryptoService crypto;

        public RepositoryUsers(IRepository _repos,ICryptoService _crypto)
        {
            repos = _repos;
            crypto = _crypto;
        }

        public void ValidateUser(USER user)
        {
            var setting = repos.Users.FirstOrDefault(u=>u.LOGIN ==user.LOGIN);
            if (setting != null)
            {
               
                if (crypto.DecryptPassword(setting.PASSWORD) != user.PASSWORD)
                {
                    throw new Exception("Пароль не верный.");
                }
            }
            else
            {
                throw new Exception("Пользлватель не найден.");
            }
           
        }
    }
}
