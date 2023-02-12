using Einstein.Domain.Entities;
using Einstein.Domain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Einstein.WebUI.Models
{
    public class UserViewModel
    {
        [ScaffoldColumn(false)]
        public int id { get; set; }

        [Required(ErrorMessage = "Требуется поле {0}")]
        [Display(Name = "Логин")]
        [EmailAddress]
        public string login { get; set; }

        [Required(ErrorMessage = "Требуется поле {0}")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string password { get; set; }

        [Display(Name = "Роль")]
        [UIHint("GridForeignKey")]
        public short roleid { get; set; }

        public USER ToEntity(USER user, ICryptoService crypto) 
        {

            user.LOGIN = login;
            user.PASSWORD =crypto.EncryptPassword(password);
            user.ROLEID = roleid;

            return user;
            
        }
    }
}