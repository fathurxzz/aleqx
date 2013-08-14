using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Listelli.Models
{
    [MetadataType(typeof(DesignerValidation))]
    public partial class Designer
    {

    }

    public class DesignerValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Идентификатор")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Имя")]
        public string DesignerName { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Имя в родительном падеже  например, - \"Михаила Задорнова\"")]
        public string DesignerNameF { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Изображение")]
        public string ImageSource { get; set; }
    }
}