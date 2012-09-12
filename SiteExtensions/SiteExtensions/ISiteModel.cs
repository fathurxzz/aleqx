using System.Collections.Generic;

namespace SiteExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISiteModel
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        string Title { get; set; }
        
        /// <summary>
        /// Описание для поисковиков
        /// </summary>
        string SeoDescription { get; set; }
        
        /// <summary>
        /// Ключевые слова для поисковиков
        /// </summary>
        string SeoKeywords { get; set; }
        
        /// <summary>
        /// Меню
        /// </summary>
        Menu Menu { get; set; }

        /// <summary>
        /// Главная страница
        /// </summary>
        bool IsHomePage { get; set; }
    }
}
