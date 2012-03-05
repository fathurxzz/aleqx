using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    [MetadataType(typeof(BrandValidation))]
    public partial class Brand
    {
         
    }

    public class BrandValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Идентификатор")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Название")]
        public string Title { get; set; }

        [DisplayName("Логотип")]
        public string Logo { get; set; }


        [DisplayName("Описание (для поисковиков)")]
        public string SeoDescription { get; set; }


        [DisplayName("Ключевые слова (для поисковиков)")]
        public string SeoKeywords { get; set; }

    }
}