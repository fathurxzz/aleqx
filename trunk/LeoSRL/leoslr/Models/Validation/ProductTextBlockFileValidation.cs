using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Leo.Models
{
    [MetadataType(typeof(ProductTextBlockFileValidation))]
    public partial class ProductTextBlockFile
    {

    }

    public class ProductTextBlockFileValidation
    {
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Превью файла")]
        public string ImageSource { get; set; }

        [Display(Name = "Файл")]
        public string FileName { get; set; }

    }
}