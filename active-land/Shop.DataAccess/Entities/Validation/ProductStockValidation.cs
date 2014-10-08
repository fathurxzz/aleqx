using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    [MetadataType(typeof(ProductStockValidation))]
    partial class ProductStock
    {

    }

    public class ProductStockValidation
    {
        [Display(Name = "Артикул")]
        [Required(ErrorMessage = "Введите артикул")]
        public string StockNumber { get; set; }

        [Display(Name = "Размер")]
        [Required(ErrorMessage = "Введите артикул")]
        public string Size { get; set; }

        [Display(Name = "Цвет")]
        [Required(ErrorMessage = "Введите артикул")]
        public string Color { get; set; }

        [Display(Name = "Доступен")]
        [Required(ErrorMessage = "Введите артикул")]
        public bool IsAvailable { get; set; }
    }
}
