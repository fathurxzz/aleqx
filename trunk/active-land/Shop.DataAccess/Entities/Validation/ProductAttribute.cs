using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    [MetadataType(typeof(ProductAttributeValidation))]
    partial class ProductAttribute
    {

    }

    public class ProductAttributeValidation
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }

        [Display(Name = "Заголовок значения")]
        public string UnitTitle { get; set; }

        [Display(Name = "Статический")]
        public bool IsStatic { get; set; }

        [Display(Name = "Отображать в кратком описании")]
        public bool DisplayOnPreview { get; set; }

        [Display(Name = "Фильтровать по данному атрибуту")]
        public bool IsFilterable { get; set; }

        [Display(Name = "Доступность на текущем языке")]
        public bool IsCorrectLang { get; set; }

    }
}
