using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kulumu.Models
{
    [MetadataType(typeof(ContentValidation))]
    public partial class Content
    {

    }

    public class ContentValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Заголовок в шапке браузера")]
        public string PageTitle { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Заголовок описания")]
        public string DescriptionTitle { get; set; }

        [DisplayName("Описание (для поисковиков)")]
        public string SeoDescription { get; set; }

        [DisplayName("Ключевые слова (для поисковиков)")]
        public string SeoKeywords { get; set; }

    }
}