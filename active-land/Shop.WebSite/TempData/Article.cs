using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.WebSite.TempData
{
    
    
    

    public class Article
    {
        public string Date { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ImageSource { get; set; }

        public static List<Article> GetArticles()
        {
            return new List<Article>
            {
                new Article
                {
                    Date = "17.07.2014",
                    Title = "Наш клуб в музыке",
                    Text = "На хит легендарной группы Queen &mdash; Bicycle Race были отобранысамые новые модели байков нашего магазина и самые новые модели девушек друзей нашего магазина :)",
                    ImageSource = "article-image.png"
                },
                new Article
                {
                    Date = "01.03.2014",
                    Title = "Проведен еще один мастер-класс!",
                    Text = "Прошедшие выходные пролетели незаметно &mdash; особено для тех, чьи дектки впервые оседлали велосипед под чутким руководством Григория Войтишина!",
                    ImageSource = "article-image.png"
                },
                new Article
                {
                    Date = "01.03.2014",
                    Title = "Проведен еще один мастер-класс!",
                    Text = "Прошедшие выходные пролетели незаметно &mdash; особено для тех, чьи дектки впервые оседлали велосипед под чутким руководством Григория Войтишина!",
                    ImageSource = "article-image.png"
                }
            };
        }
    }
}