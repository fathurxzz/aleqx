using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Api.DataSynchronization.Helpers;
using Shop.DataAccess.Repositories;

namespace Shop.Api.DataSynchronization.Export
{
    public class ExportToFile
    {
        public static string Execute(IShopRepository repository, int currentLangId, string categoryName)
        {


            var products = repository.GetProductsByCategory(categoryName).ToList();
            var attributes = repository.GetProductAttributes(categoryName).ToList();

            foreach (var productAttribute in attributes)
            {
                productAttribute.CurrentLang = currentLangId;
            }

            var sb = new StringBuilder();


            // Генерация заголовков begin
            var productFields = new Dictionary<string, string>()
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

            foreach (var field in productFields)
            {
                sb.Append(field.Key);
                sb.Append(";");
            }

            foreach (var productAttribute in attributes)
            {
                sb.Append(productAttribute.ExternalId);
                sb.Append(";");
            }

            sb.Append("\r\n");

            foreach (var field in productFields)
            {
                sb.Append(field.Value);
                sb.Append(";");
            }

            foreach (var productAttribute in attributes)
            {
                sb.Append(productAttribute.Title);
                sb.Append(";");
            }
            sb.Append("\r\n");
            // Генерация заголовков end


            foreach (var product in products)
            {
                product.CurrentLang = currentLangId;

                sb.Append(product.ExternalId);
                sb.Append(";");
                sb.Append(product.Name);
                sb.Append(";");
                sb.Append(product.Title);
                sb.Append(";");
                sb.Append(product.OldPrice);
                sb.Append(";");
                sb.Append(product.Price);
                sb.Append(";");
                sb.Append(product.IsNew);
                sb.Append(";");
                sb.Append(product.IsDiscount);
                sb.Append(";");
                sb.Append(product.IsTopSale);
                sb.Append(";");
                sb.Append(product.IsActive);
                sb.Append(";");



                var firstProductStock = product.ProductStocks.OrderBy(ps => ps.StockNumber).FirstOrDefault();
                if (firstProductStock != null)
                {

                    sb.Append(firstProductStock.StockNumber);
                    sb.Append(";");
                    sb.Append(firstProductStock.Size);
                    sb.Append(";");
                    sb.Append(firstProductStock.Color);
                    sb.Append(";");
                }
                else
                {
                    sb.Append(";");
                    sb.Append(";");
                    sb.Append(";");
                }





                foreach (var productAttribute in attributes)
                {
                    var result = new List<string>();

                    foreach (var pav in product.ProductAttributeValues)
                    {
                        pav.CurrentLang = currentLangId;
                        if (productAttribute.ProductAttributeValues.Contains(pav))
                        {
                            result.Add(pav.Title);
                        }
                    }

                    var res = DataSynchronizationHelper.ConvertToStringWithSeparators(result);
                    if (res != null)
                    {
                        sb.Append(res);
                    }

                    sb.Append(";");
                }

                sb.Append("\r\n");




                foreach (var ps in product.ProductStocks.OrderBy(ps => ps.StockNumber).Skip(1))
                {
                    sb.Append(product.ExternalId);
                    sb.Append(";");
                    sb.Append(product.Name);
                    sb.Append(";");
                    sb.Append(product.Title);
                    sb.Append(";");
                    sb.Append(product.OldPrice);
                    sb.Append(";");
                    sb.Append(product.Price);
                    sb.Append(";");
                    sb.Append(product.IsNew);
                    sb.Append(";");
                    sb.Append(product.IsDiscount);
                    sb.Append(";");
                    sb.Append(product.IsTopSale);
                    sb.Append(";");
                    sb.Append(product.IsActive);
                    sb.Append(";");



                    sb.Append(ps.StockNumber);
                    sb.Append(";");
                    sb.Append(ps.Size);
                    sb.Append(";");
                    sb.Append(ps.Color);
                    sb.Append(";");


                    foreach (var productAttribute in attributes)
                    {
                        var result = new List<string>();

                        foreach (var pav in product.ProductAttributeValues)
                        {
                            pav.CurrentLang = currentLangId;
                            if (productAttribute.ProductAttributeValues.Contains(pav))
                            {
                                result.Add(pav.Title);
                            }
                        }

                        var res = DataSynchronizationHelper.ConvertToStringWithSeparators(result);
                        if (res != null)
                        {
                            sb.Append(res);
                        }

                        sb.Append(";");
                    }

                    sb.Append("\r\n");

                }


            }

            return sb.ToString();
        }

    }
}
