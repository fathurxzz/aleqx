using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiki.DataAccess.Entities
{
    [MetadataType(typeof(ServiceItemValidation))]
    partial class ServiceItem
    {

    }

    class ServiceItemValidation
    {
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Заголовок (ENG)")]
        public string TitleEng { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Цена")]
        public string Price { get; set; }

        [Display(Name = "Описание (ENG)")]
        public string DescriptionEng { get; set; }

        [Display(Name = "Порядок отображения")]
        public int SortOrder { get; set; }
    }
}
