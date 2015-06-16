using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewVision.UI.Models
{
    [MetadataType(typeof(ContentValidation))]
    partial class Content
    {

    }

    public class ContentValidation
    {
        [Display(Name = "Заголовок пункта меню RU")]
        public string MenuTitle { get; set; }

        [Display(Name = "Заголовок пункта меню EN")]
        public string MenuTitleEn { get; set; }

        [Display(Name = "Заголовок пункта меню UA")]
        public string MenuTitleUa { get; set; }

        [Display(Name = "Заголовок RU")]
        public string Title { get; set; }

        [Display(Name = "Заголовок EN")]
        public string TitleEn { get; set; }

        [Display(Name = "Заголовок UA")]
        public string TitleUa { get; set; }

        [Display(Name = "Текст RU")]
        public string Text { get; set; }

        [Display(Name = "Текст EN")]
        public string TextEn { get; set; }

        [Display(Name = "Текст UA")]
        public string TextUa { get; set; }

        [Display(Name = "Идентификатор")]
        public string Name { get; set; }

        [Display(Name = "Фото")]
        public string ImageSrc { get; set; }

        [Display(Name = "Порядок отображения")]
        public string SortOrder { get; set; }
    }
}