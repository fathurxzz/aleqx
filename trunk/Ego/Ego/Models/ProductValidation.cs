using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ego.Models
{
    [MetadataType(typeof(ProductValidation))]
    public partial class Product
    {

    }

    public class ProductValidation
    {
        [DisplayName("Изображение")]
        public string ImageSource { get; set; }


        [DisplayName("Превью")]
        public string PreviewImageSource { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Порядок отображения")]
        public int SortOrder { get; set; }
    }
}