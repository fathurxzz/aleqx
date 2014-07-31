using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    [MetadataType(typeof(ProductValidation))]
    partial class Product
    {

    }

    public class ProductValidation
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }

        //[Display(Name = "Порядок отображения")]
        //[Required(ErrorMessage = "Введите порядок отображения")]
        //public string SortOrder { get; set; }

        [Display(Name = "Идентификатор")]
        [Required(ErrorMessage = "Введите идентификатор")]
        public string Name { get; set; }

        [Display(Name = "Доступность на текущем языке")]
        public bool IsCorrectLang { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Описание для поисковиков")]
        public string SeoDescription { get; set; }

        [Display(Name = "Ключевые слова для поисковиков")]
        public string SeoKeywords { get; set; }

        [Display(Name = "Текст для поисковиков")]
        public string SeoText { get; set; }

        [Display(Name = "Новый")]
        public bool IsNew { get; set; }

        [Display(Name = "Акция")]
        public bool IsDiscount { get; set; }

        [Display(Name = "Хит продаж")]
        public bool IsTopSale { get; set; }

        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Display(Name = "Старая цена")]
        public decimal OldPrice { get; set; }

        
    }

}
