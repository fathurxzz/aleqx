﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Api.DataSynchronization.Model
{
    public static class TransferData
    {
        public static Dictionary<string, string> ProductFields = new Dictionary<string, string>
        {
            {"ExternalId", "Внешний Id"},
            {"Name", "Id в строке адреса"},
            {"Title", "Заголовок"},
            {"OldPrice", "Старая цена"},
            {"Price", "Цена"},
            {"IsNew", "Новый"},
            {"IsDiscount", "Скидка"},
            {"IsTopSale", "Хит продаж"},
            {"IsActive", "Активный"},
            {"ProductStock.StockNumber", "Артикул"},
            {"ProductStock.Size", "Размер"},
            {"ProductStock.Color", "Цвет"}
        };

    }
}
