using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vip.Models
{
    [MetadataType(typeof(ProjectValidation))]
    public partial class Project
    {
         
    }
    public class ProjectValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Веб-имя страницы")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Порядок отображения")]
        public int SortOrder { get; set; }

        [DisplayName("Изображение")]
        public int ImageSource { get; set; }

        [DisplayName("Заголовок описания")]
        public int DescriptionTitle { get; set; }

        [DisplayName("Описание")]
        public int Description { get; set; }

        [DisplayName("Менеджер")]
        public int Manager { get; set; }


    }
}