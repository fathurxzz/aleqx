using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kulumu.Models
{
    [MetadataType(typeof(ArticleValidation))]
    public partial class Article
    {

    }

    public class ArticleValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Веб-имя (отображается в строке адреса, должно быть уникальным)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Краткое описание (отображается в списке новостей)")]
        public string Description { get; set; }

        [DisplayName("Текст")]
        public string Text { get; set; }

        [DisplayName("Старая цена")]
        public string OldPrice { get; set; }

        [DisplayName("Новая цена")]
        public string NewPrice { get; set; }

        [DisplayName("Изображение")]
        public string ImageSource { get; set; }
    }
}