using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    [MetadataType(typeof(MainPageBannerValidation))]
    partial class MainPageBanner
    {

    }

    class MainPageBannerValidation
    {
        [Display(Name = "Изображение")]
        public string ImageSource { get; set; }
    }
}
