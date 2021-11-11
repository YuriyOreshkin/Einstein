﻿using Einstein.Domain.Entities;
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
        [Required(ErrorMessage ="Требуется поле {0}")]
        public string eventname { get; set; }

        public long eventid { get; set; }

        [DisplayName("Дата заявки")]
        public DateTime dateorder { get; set; }

        [DisplayName("Дата мероприятия")]
        [Required(ErrorMessage = "Требуется поле {0}")]
        public DateTime dateevent { get; set; }

        [DisplayName("Время посещения")]
        [Required(ErrorMessage = "Требуется поле {0}")]
        public string timeevent { get; set; }

        [DisplayName("Количество человек")]
        [Required(ErrorMessage = "Требуется поле {0}")]
        //[Range(1,short.MaxValue, ErrorMessage = "Значение поля {0} должно быть в диапазоне от {1} до {2}")]
        public short persons { get; set; }

        [DisplayName("Количество детей до 14")]
        [Required(ErrorMessage = "Требуется поле {0}")]
        //[Range(1, short.MaxValue, ErrorMessage = "Значение поля {0} должно быть в диапазоне от {1} до {2}")]
        public short persons14 { get; set; }

        [DisplayName("Свободные места")]
        public int freeplaces { get; set; }

        [DisplayName("Свободные места до 14")]
        public int freeplaces14 { get; set; }

        [DisplayName("Номер телефона")]
        [Required(ErrorMessage = "Требуется поле {0}")]
        public string phonenumber { get; set; }

        [DisplayName("Электронная почта")]
        [Required(ErrorMessage = "Требуется поле {0}")]
        public string email { get; set; }

        public ORDER ToEntity()
        {
            return new ORDER
            {
                ID=id,
                DATE =dateorder,
                EMAIL=email,
                PERSONS= persons,
                PERSONS14 =persons14,
                PHONE=phonenumber,
                EVENTID=eventid
            };
        }
    }
}
