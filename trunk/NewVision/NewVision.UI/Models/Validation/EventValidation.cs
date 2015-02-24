using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewVision.UI.Models
{
    [MetadataType(typeof(EventValidation))]
    partial class Event
    {
         
    }

    public class EventValidation
    {
        
        [Display(Name = "Дата")]
        public System.DateTime Date { get; set; }

        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Описание заголовка")]
        public string TitleDescription { get; set; }

        [Display(Name = "Выделенный текст")]
        public string HighlightedText { get; set; }

        [Display(Name = "Адрес")]
        public string LocationAddress { get; set; }

        //[Display(Name = "Заголовок")]
        //public string LocationTitle { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Способ заказа билетов")]
        public int TicketOrderType { get; set; }

        [Display(Name = "Тип контента превью")]
        public int PreviewContentType { get; set; }

        [Display(Name = "Изображение или видео")]
        public string PreviewContentSrc { get; set; }

        [Display(Name = "Действо")]
        public string Action { get; set; }

        [Display(Name = "Локация")]
        public string Location { get; set; }

        [Display(Name = "Арт-группа")]
        public string ArtGroup { get; set; }

        [Display(Name = "Длительность действа")]
        public string Duration { get; set; }

        [Display(Name = "Количество антрактов")]
        public string IntervalQuantity { get; set; }

        [Display(Name = "Цены на билеты")]
        public string Price { get; set; }
    }
}