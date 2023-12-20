using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Einstein.WebUI.Models
{
    public class MailingSettingsViewModel
    {
        public bool enablerss { get; set; }
        [Required]
        [DisplayName("Начало: от текущей")]
        public short beginfromtoday { get; set; }
        [Required]
        [DisplayName("Конец: от текущей")]
        public short endfromtoday { get; set; }
        
        [Required]
        [DisplayName("Получатели")]
        public string[] recipients { get; set; }

        public MAILINGSERVICESETTINGS ToEntity(MAILINGSERVICESETTINGS settings)
        {

            settings.ENABLE = enablerss;
            settings.BEGINFROMTODAY = beginfromtoday;
            settings.ENDFROMTODAY = endfromtoday;
            settings.RECIPIENTS = String.Join(",",recipients);

            return settings;
        }

    }
}