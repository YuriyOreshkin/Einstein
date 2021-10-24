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
    public class OrderViewModel
    {
        [ScaffoldColumn(false)]
        public long id { get; set; }

        [DisplayName("Мероприятие")]
        [Required]
        public string eventname { get; set; }

        public long eventid { get; set; }

        [DisplayName("Дата заявки")]
        public DateTime dateorder { get; set; }

        [DisplayName("Дата мероприятия")]
        [Required]
        public DateTime dateevent { get; set; }

        [DisplayName("Время посещения")]
        [Required]
        public string timeevent { get; set; }

        [DisplayName("Количество человек")]
        [Required]
        [Range(1,short.MaxValue)]
        public short persons { get; set; }


        public int freeplaces { get; set; }

        [DisplayName("Номер телефона")]
        [Required]
        public string phonenumber { get; set; }

        [DisplayName("Электронная почта")]
        [Required]
        public string email { get; set; }

        public ORDER ToEntity()
        {
            return new ORDER
            {
                ID=id,
                DATE =dateorder,
                EMAIL=email,
                PERSONS= persons,
                PHONE=phonenumber,
                EVENTID=eventid
            };
        }
    }
}
