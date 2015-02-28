using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewVision.UI.Helpers
{
    public class SiteContentHelper
    {
        public static string[] TicketOrderType = { "Заказать билет", "Вход свободный", "Получить пригласительный" };
        public static string[] TicketOrderTypeKeys = { "order", "free", "invite" };
        public static string[] PreviewContentType = { "Изображение", "Видео" };
        public static string[] PreviewContentTypeKeys = { "image", "video" };
    }
}