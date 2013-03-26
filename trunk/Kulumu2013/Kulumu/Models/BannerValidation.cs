using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kulumu.Models
{
    [MetadataType(typeof(BannerValidation))]
    public partial class Banner
    {

    }

    public class BannerValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Ссылка")]
        public string Link { get; set; }

        [DisplayName("Текст описания")]
        public string Description { get; set; }

        [DisplayName("Изображение")]
        public string ImageSource { get; set; }

        [DisplayName("Цена")]
        public string Price { get; set; }
    }
}