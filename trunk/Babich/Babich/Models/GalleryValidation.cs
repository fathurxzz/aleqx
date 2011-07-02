using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Babich.Models
{
    [MetadataType(typeof(GalleryValidation))]
    public partial class Gallery
    {

    }
    public class GalleryValidation
    {
        [DisplayName("Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "* Введите порядок отображения (целое число)")]
        [DisplayName("Порядок отбражения")]
        public string SortOrder { get; set; }

    }
}