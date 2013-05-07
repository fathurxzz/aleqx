using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shitova.Models
{
    [MetadataType(typeof(ContentValidation))]
    public partial class Content
    {

    }

    public class ContentValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Текст")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Порядок отображения")]
        public int SortOrder { get; set; }

        [DisplayName("Описание (для поисковиков)")]
        public string SeoDescription { get; set; }

        [DisplayName("Ключевые слова (для поисковиков)")]
        public string SeoKeywords { get; set; }
    }
}