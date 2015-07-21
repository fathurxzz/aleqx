using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewVision.UI.Models
{
    [MetadataType(typeof(AuthorValidation))]
    partial class Author
    {

    }
    public class AuthorValidation
    {
        [Display(Name = "Идентификатор")]
        public string Name { get; set; }

        [Display(Name = "Заголовок RU")]
        public string Title { get; set; }

        [Display(Name = "Заголовок EN")]
        public string TitleEn { get; set; }

        [Display(Name = "Заголовок UA")]
        public string TitleUa { get; set; }

        [Display(Name = "Описание RU")]
        public string Description { get; set; }

        [Display(Name = "Описание EN")]
        public string DescriptionEn { get; set; }

        [Display(Name = "Описание UA")]
        public string DescriptionUa { get; set; }

        [Display(Name = "Об артисте RU")]
        public string About { get; set; }

        [Display(Name = "Об артисте EN")]
        public string AboutEn { get; set; }

        [Display(Name = "Об артисте UA")]
        public string AboutUa { get; set; }

        [Display(Name = "Фото")]
        public string Photo { get; set; }
        
        [Display(Name = "Аватар")]
        public string Avatar { get; set; }

        [Display(Name = "Тэги")]
        public string Tags { get; set; }

        [Display(Name = "Порядок отображения")]
        public int SortOrder { get; set; }
    }
}