using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewVision.UI.Helpers
{
    public class SiteContentHelper
    {
        public static string[] TicketOrderType = { "Заказать билет", "Вход свободный", "Получить пригласительный" , "Мест нет"};
        public static string[] TicketOrderTypeKeys = { "order", "free", "invite", "noSeats"};
        public static string[] PreviewContentType = { "Фото", "Видео" };
        public static string[] PreviewContentTypeKeys = { "image", "video" };
        public static string[] TitlePosition = { "Вверху", "Внизу" };
        public static string[] TitlePositionKeys = { "top", "bottom" };
    }
}