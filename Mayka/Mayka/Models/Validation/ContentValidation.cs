using System.ComponentModel.DataAnnotations;

namespace Mayka.Models
{
    [MetadataType(typeof(ContentValidation))]
    public partial class Content
    {


    }

    public class ContentValidation
    {

        public int Id { get; set; }

        [Display(Name = "Адрес страницы в браузуре")]
        [Required(ErrorMessage = "Введите адрес")]
        public string Name { get; set; }

        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }

        [Display(Name = "Заголовок пункта меню")]
        [Required(ErrorMessage = "Введите заголовок пункта меню")]
        public string MenuTitle { get; set; }

        [Display(Name = "Порядок отображения")]
        [Required]
        public int SortOrder { get; set; }

        [Display(Name = "Текст страницы")]
        public string Text { get; set; }

        [Display(Name = "Описание для поисковиков")]
        
        public string SeoDescription { get; set; }

        [Display(Name = "Ключевые слова для поисковиков")]
        
        public string SeoKeywords { get; set; }

        [Display(Name = "Тип контента страницы")]
        [Required]
        public byte ContentType { get; set; }
    }
}