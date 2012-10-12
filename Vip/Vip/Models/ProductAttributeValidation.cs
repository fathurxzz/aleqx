using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vip.Models
{
    [MetadataType(typeof(CategoryAttributeValidation))]
    public partial class CategoryAttribute
    {

    }

    public class CategoryAttributeValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Идентификатор (отображается в браузере в строке адреса)")]
        public string Name { get; set; }


    }
}