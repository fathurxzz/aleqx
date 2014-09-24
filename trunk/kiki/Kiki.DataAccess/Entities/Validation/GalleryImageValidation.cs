using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiki.DataAccess.Entities
{
    [MetadataType(typeof(GalleryImageValidation))]
    partial class GalleryImage
    {

    }

    class GalleryImageValidation
    {
        [Display(Name = "Изображение")]
        public string ImageSource { get; set; }
    }
}
