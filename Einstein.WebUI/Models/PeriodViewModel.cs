using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Einstein.WebUI.Models
{
    public class PeriodViewModel
    {
        [DisplayName("Дата начала")]
        public DateTime datebegin { get; set; }

        [DisplayName("Дата окончания")]
        public DateTime dateend { get; set; }

    }
}
