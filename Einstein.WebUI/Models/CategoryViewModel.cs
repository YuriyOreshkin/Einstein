using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Einstein.WebUI.Models
{
    public class CategoryViewModel
    {
        [ScaffoldColumn(false)]
        public long id { get; set; }

        [DisplayName("Наименование")]
        [StringLength(100)]
        [Required]
        public string name { get; set; }

        public CATEGORY ToEntity(CATEGORY category)
        {
            category.ID = this.id;
            category.NAME = this.name;

            return category;
        }

    }
}