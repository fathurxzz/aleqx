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

        [Display(Name = "Заголовок RU")]
        public string Title { get; set; }

        [Display(Name = "Заголовок EN")]
        public string TitleEn { get; set; }

        [Display(Name = "Заголовок UA")]
        public string TitleUa { get; set; }

        [Display(Name = "Предзаголовок RU")]
        public string TitleDescription { get; set; }

        [Display(Name = "Предзаголовок EN")]
        public string TitleDescriptionEn { get; set; }

        [Display(Name = "Предзаголовок UA")]
        public string TitleDescriptionUa { get; set; }

        [Display(Name = "Флажок (если нужен) RU")]
        public string HighlightedText { get; set; }

        [Display(Name = "Флажок (если нужен) EN")]
        public string HighlightedTextEn { get; set; }

        [Display(Name = "Флажок (если нужен) UA")]
        public string HighlightedTextUa { get; set; }

        [Display(Name = "Адрес локации RU")]
        public string LocationAddress { get; set; }
        
        [Display(Name = "Адрес локации EN")]
        public string LocationAddressEn { get; set; }
        
        [Display(Name = "Адрес локации UA")]
        public string LocationAddressUa { get; set; }
        
        [Display(Name = "Ссылка на карту адреса локации")]
        public string LocationAddressMapUrl { get; set; }

        [Display(Name = "Заголовок локации RU")]
        public string LocationTitle { get; set; }

        [Display(Name = "Заголовок локации EN")]
        public string LocationTitleEn { get; set; }

        [Display(Name = "Заголовок локации UA")]
        public string LocationTitleUa { get; set; }

        [Display(Name = "Описание мероприятия RU")]
        public string Description { get; set; }

        [Display(Name = "Описание мероприятия EN")]
        public string DescriptionEn { get; set; }

        [Display(Name = "Описание мероприятия UA")]
        public string DescriptionUa { get; set; }

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

        [Display(Name = "Описание действа RU")]
        public string Action { get; set; }

        [Display(Name = "Описание действа EN")]
        public string ActionEn { get; set; }

        [Display(Name = "Описание действа UA")]
        public string ActionUa { get; set; }

        [Display(Name = "Описание локации RU")]
        public string Location { get; set; }

        [Display(Name = "Описание локации EN")]
        public string LocationEn { get; set; }

        [Display(Name = "Описание локации UA")]
        public string LocationUa { get; set; }

        [Display(Name = "Описание арт-группы RU")]
        public string ArtGroup { get; set; }

        [Display(Name = "Описание арт-группы EN")]
        public string ArtGroupEn { get; set; }

        [Display(Name = "Описание арт-группы UA")]
        public string ArtGroupUa { get; set; }

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