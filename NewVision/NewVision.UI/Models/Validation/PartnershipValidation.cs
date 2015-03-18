using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewVision.UI.Models
{
    [MetadataType(typeof(PartnershipValidation))]
    partial class Partnership
    {

    }

    public class PartnershipValidation
    {
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Фото")]
        public string ImageSrc { get; set; }
    }
}