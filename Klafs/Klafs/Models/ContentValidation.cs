using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Klafs.Models
{
    [MetadataType(typeof(ContentValidation))]
    public partial class Content
    {
        
    }

    [Bind(Exclude = "Id")]
    public class ContentValidation
    {
        [Required(ErrorMessage = "*")]
        [DisplayName("Техническое имя страницы")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Название пункта меню")]
        public string MenuTitle { get; set; }

        [DisplayName("Заголовок в шапке браузера")]
        public string PageTitle { get; set; }

        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Надпись на красной плашке")]
        public string Sign { get; set; }

        [DisplayName("Надпись под красной плашкой")]
        public string Sign2 { get; set; }

        [DisplayName("Описание пункта меню")]
        public string Description { get; set; }

        [Required(ErrorMessage = "*")]
        [DisplayName("Порядок отбражения")]
        public string SortOrder { get; set; }

        [DisplayName("Содержимое страницы")]
        public string Text { get; set; }

        [DisplayName("Ключевые слова этой страницы")]
        public string SeoKeywords { get; set; }

        [DisplayName("Описание страницы")]
        public string SeoDescription { get; set; }

        [DisplayName("Текст для поисковиков")]
        public string SeoText { get; set; }

    }


}