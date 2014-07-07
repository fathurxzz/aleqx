using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Leo.Models
{

    [MetadataType(typeof(SpecialContentValidation))]
    public partial class SpecialContent
    {

    }


    public class SpecialContentValidation
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }

        [Display(Name = "Текст страницы")]
        public string Text { get; set; }

        [Display(Name = "Фон страницы")]
        public string PageImageSource { get; set; }

        [Display(Name = "Фон текстового блока")]
        public string ContentImageSource { get; set; }

    }

}