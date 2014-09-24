using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiki.DataAccess.Entities
{
    [MetadataType(typeof(ContentValidation))]
    partial class Content
    {

    }

    public class ContentValidation
    {
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Заголовок (ENG)")]
        public string TitleEng { get; set; }

        [Display(Name = "Заголовок меню")]
        public string MenuTitle { get; set; }

        [Display(Name = "Заголовок меню (ENG)")]
        public string MenuTitleEng { get; set; }

        [Display(Name = "Идентификатор")]
        public string Name { get; set; }

        [Display(Name = "Порядок отображения")]
        public int  SortOrder { get; set; }

        [Display(Name = "Изображение")]
        public string ImageSource { get; set; }

        [Display(Name = "Описание для поисковиков")]
        public string SeoDescription { get; set; }

        [Display(Name = "Ключевые слова для поисковиков")]
        public string SeoKeywords { get; set; }

        [Display(Name = "Текст для поисковиков")]
        public string SeoText { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Текст (ENG)")]
        public string TextEng { get; set; }
    }
}
