using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Posh.Models
{
    [MetadataType(typeof(ContentValidation))]
    public partial class Content
    {

    }

    public class ContentValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Идентификатор")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок в шапке обозревателя")]
        public string PageTitle { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Порядок отображения")]
        public int SortOrder { get; set; }

        [DisplayName("Описание (для поисковиков)")]
        public string SeoDescription { get; set; }

        [DisplayName("Ключевые слова (для поисковиков)")]
        public string SeoKeywords { get; set; }

        [DisplayName("Содержимое")]
        public string Text { get; set; }

        [DisplayName("Заголовок текста для поисковиков")]
        public string SeoTitle { get; set; }

        [DisplayName("Текст для поисковиков")]
        public string SeoText { get; set; }

        [DisplayName("Основная страница")]
        public string MainPage { get; set; }

        [DisplayName("Статическая страница")]
        public string Static { get; set; }


    }
}