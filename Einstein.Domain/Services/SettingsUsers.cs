using Einstein.Domain.Entities;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Einstein.Domain.Services
{
    public class SettingsUsers : IUsers
    {
        private IMailServiceConfig config;


        public SettingsUsers(IMailServiceConfig _config)
        {
            config = _config;
        }

        public void ValidateUser(USER user)
        {
            var setting = config.ReadSettings();
            if (setting != null)
            {
                if (setting.USER == user.NAME)
                {
                    if (setting.PASSWORD != user.PASSWORD)
                    {
                        throw new Exception("Пароль не верный.");
                    }
                }
                else {
                    throw new Exception("Пользлватель не найден.");
                }
            }
            else
            {
                throw new Exception("Не удалось прочитать настройки.");
            }
        }
    }
}
