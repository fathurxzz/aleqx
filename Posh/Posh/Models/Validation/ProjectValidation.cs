using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Posh.Models
{
    [MetadataType(typeof(ProjectValidation))]
    public partial class Project
    {

    }

    public class ProjectValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Идентификатор")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Порядок отображения")]
        public int SortOrder { get; set; }

        [DisplayName("Верхний текст")]
        public string TextTop { get; set; }

        [DisplayName("Нижний текст")]
        public string TextBottom { get; set; }
    }
}