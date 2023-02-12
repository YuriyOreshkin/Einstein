using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Einstein.WebUI.Models
{
    public class PaymentSettingsViewModel
    {
        public bool enablepass { get; set; }
        [Required]
        [DisplayName("Терминал ID")]
        public string terminalid { get; set; }
        [DisplayName("Открывыть платежную форму в новом окне")]
        public bool frame { get; set; }
        [Required]
        [DisplayName("Язык")]
        public string language { get; set; }

        public PAYMENTSETTINGS ToEntity(PAYMENTSETTINGS settings)
        {

            settings.ENABLE = enablepass;
            settings.TERMINALID = terminalid;
            settings.FRAME = frame;
            settings.LANGUAGE = language;
            return settings;
        }

    }
}