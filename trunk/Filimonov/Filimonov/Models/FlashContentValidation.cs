using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Filimonov.Models
{
    [MetadataType(typeof(FlashContentValidation))]
    public partial class FlashContent
    {
        public string DirectoryName { get; set; }
    }

    public class FlashContentValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Изображение")]
        public string ImageSource { get; set; }
    }
}