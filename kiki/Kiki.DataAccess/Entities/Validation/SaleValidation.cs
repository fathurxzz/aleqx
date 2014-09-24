using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiki.DataAccess.Entities
{
    [MetadataType(typeof(SaleValidation))]
    partial class Sale
    {

    }

    class SaleValidation
    {
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Заголовок (ENG)")]
        public string TitleEng { get; set; }

        [Display(Name = "Дата начала")]
        public string StartDate { get; set; }

        [Display(Name = "Дата окончания")]
        public string EndDate { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Описание (ENG)")]
        public string DescriptionEng { get; set; }

        [Display(Name = "Идентификатор")]
        public string Name { get; set; }

        [Display(Name = "Изображение")]
        public string ImageSource { get; set; }

        [Display(Name = "Текст")]
        public string Text { get; set; }

        [Display(Name = "Текст (ENG)")]
        public string TextEng { get; set; }
    }
}
