using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiki.DataAccess.Entities
{
    [MetadataType(typeof(ArticleValidation))]
    partial class Article
    {

    }

    public class ArticleValidation
    {
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Заголовок (ENG)")]
        public string TitleEng { get; set; }

        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Display(Name = "Идентификатор")]
        public string Name { get; set; }

        [Display(Name = "Изображение")]
        public string ImageSource { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Текст (ENG)")]
        public string TextEng { get; set; }
    }
}
