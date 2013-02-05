using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Filimonov.Models
{
    [MetadataType(typeof(ProjectValidation))]
    public partial class Project
    {

    }

    public class ProjectValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Веб-имя страницы (отображается в строке адреса) вводите латинскими символами")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Заголовок описания")]
        public string DescriptiontTitle { get; set; }

        [DisplayName("Текст описания")]
        public string Description { get; set; }

        [DisplayName("Порядок отображения")]
        public string SortOrder { get; set; }

        [DisplayName("Изображение")]
        public string ImageSource { get; set; }
    }
}