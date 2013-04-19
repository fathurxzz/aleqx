using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kulumu.Models
{
    [MetadataType(typeof(ProductValidation))]
    public partial class Product
    {

    }

    public class ProductValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Файл")]
        public string ImageSource { get; set; }

        [DisplayName("Скидка")]
        public string Discount { get; set; }

        [DisplayName("Скидка (текст)")]
        public string DiscountText { get; set; }

        [DisplayName("Цена")]
        public string Price { get; set; }

        [DisplayName("Состав")]
        public string Structure { get; set; }

        [DisplayName("Плотность")]
        public string Consistence { get; set; }

        [DisplayName("Производитель")]
        public string Producer { get; set; }

        [DisplayName("Высота ворса")]
        public string Nap { get; set; }
    }
}