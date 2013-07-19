using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Helpers
{
    public static class TextHelper
    {
        public static string CartPrefix = "В корзине";

        public static string GetQuantitySufix(int quantity)
        {
            string sufix;
            switch (quantity)
            {
                case 1:
                case 21:
                    sufix = "товар";
                    break;
                case 2:
                case 3:
                case 4:
                case 22:
                case 23:
                case 24:
                    sufix = "товарa";
                    break;
                default:
                    sufix = "товаров";
                    break;
            }
            return sufix;
        }
    }
}