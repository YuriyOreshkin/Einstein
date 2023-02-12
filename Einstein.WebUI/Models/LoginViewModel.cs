using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Einstein.WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Требуется поле {0}")]
        [Display(Name = "Пользователь")]
        [EmailAddress]
        public string login { get; set; }

        [Required(ErrorMessage = "Требуется поле {0}")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool remember { get; set; }

        public USER ToEntity() 
        {
            return new USER
            {
                LOGIN = login,
                PASSWORD = password
            };
        }
    }
}