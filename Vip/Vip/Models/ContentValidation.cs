using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vip.Models
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

        [DisplayName("Текст")]
        public string Text { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Заголовок описания")]
        public string DescriptionTitle { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Порядок отображения")]
        public int SortOrder { get; set; }

        [DisplayName("Описание (для поисковиков)")]
        public string SeoDescription { get; set; }

        [DisplayName("Ключевые слова (для поисковиков)")]
        public string SeoKeywords { get; set; }

    }
}