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
        [DisplayName("№")]
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
        [Range(1,short.MaxValue, ErrorMessage = "Значение поля {0} должно быть в диапазоне от {1} до {2}")]
        public short persons { get; set; }

        [DisplayName("Количество детей до 14")]
        [Required(ErrorMessage = "Требуется поле {0}")]
        [Range(0, short.MaxValue, ErrorMessage = "Значение поля {0} должно быть в диапазоне от {1} до {2}")]
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

        [DisplayName("Уведомление отправлено")]
        public bool inform { get; set; }
        [DisplayName("Цена билета")]
        public decimal price { get; set; }
        [DisplayName("Оплачено")]
        public decimal prepay { get; set; }
        [DisplayName("Примечание")]
        [StringLength(250,ErrorMessage ="Максимальная длина строки 250 символов.")]
        public string description { get; set; }
        //[Required(ErrorMessage = "Необходимо прочитать условия и дать согласие!")]
        [DisplayName("Сумма")]
        public decimal amount { get 
            {
                return price * persons;
            } 
        }
        private string unMask(string _str)
        {
           return string.Join("", _str.Where(c => char.IsDigit(c) || c == '+'));
        }



        public ORDER ToEntity(ORDER order)
        {

            order.ID = id;
            order.DATE = dateorder;
            order.EMAIL = email;
            order.PERSONS = persons;
            order.PERSONS14 = persons14;
            order.PHONE = unMask(phonenumber);
            order.EVENTID = eventid;
            order.INFORM = inform;
            order.PREPAY = prepay;
            order.DESCRIPTION = description;

            return order;
        }
    }
}
