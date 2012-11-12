using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kulumu.Models
{
    [MetadataType(typeof(CategoryValidation))]
    public partial class Category
    {

    }

    public class CategoryValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Веб-имя страницы (отображается в строке адреса)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }


        [DisplayName("Описание (вверху)")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Описание")]
        public string BottomDescription { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок описания")]
        public string BottomDescriptionTitle { get; set; }

    }
}