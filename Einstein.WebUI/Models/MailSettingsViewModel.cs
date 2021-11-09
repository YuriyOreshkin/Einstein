using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Einstein.WebUI.Models
{
    public class MailSettingsViewModel
    {
        public bool enable { get; set; }
        [Required]
        [DisplayName("SMTP-сервер")]
        public string host { get; set; }
        [Required]
        [DisplayName("Порт")]
        public int port { get; set; }
        [Required]
        [DisplayName("Пользователь")]
        public string user { get; set; }
        [Required]
        [DisplayName("Пароль")]
        public string password { get; set; }

        public MAILSERVICESETTINGS ToEntity(MAILSERVICESETTINGS settings)
        {

            settings.ENABLE = enable;
            settings.HOST = host;
            settings.PORT = port;
            settings.USER = user;
            settings.PASSWORD = password;

            return settings;
        }

    }
}