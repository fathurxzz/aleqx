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

        [Display(Name = "Предзаголовок")]
        public string TitleDescription { get; set; }

        [Display(Name = "Флажок (если нужен)")]
        public string HighlightedText { get; set; }

        [Display(Name = "Адрес локации")]
        public string LocationAddress { get; set; }

        [Display(Name = "Заголовок локации")]
        public string LocationTitle { get; set; }

        [Display(Name = "Описание мероприятия")]
        public string Description { get; set; }

        [Display(Name = "Тип заказа билетов")]
        public int TicketOrderType { get; set; }

        [Display(Name = "Видео или Фото?")]
        public int PreviewContentType { get; set; }

        [Display(Name = "Изображения превью")]
        public int PreviewContentImages { get; set; }

        [Display(Name = "Изображение")]
        public string PreviewContentImageSrc { get; set; }
        
        [Display(Name = "Адрес видеоролика")]
        public string PreviewContentVideoSrc { get; set; }

        [Display(Name = "Описание действа")]
        public string Action { get; set; }

        [Display(Name = "Описание локации")]
        public string Location { get; set; }

        [Display(Name = "Описание арт-группы")]
        public string ArtGroup { get; set; }

        [Display(Name = "Длительность действа")]
        public string Duration { get; set; }

        [Display(Name = "Количество антрактов")]
        public string IntervalQuantity { get; set; }

        [Display(Name = "Цены на билеты")]
        public string Price { get; set; }

        [Display(Name = "Загрузка в ленту картинок")]
        public string ContentImages { get; set; }
        
    }
}