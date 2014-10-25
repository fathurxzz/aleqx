using System;
using System.Collections.Generic;
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
            //var products = _repository.GetAllProducts().ToList();
            var products = repository.GetProductsByCategory(categoryName).ToList();

            var attributes = repository.GetProductAttributes(categoryName).ToList();
            foreach (var productAttribute in attributes)
            {
                productAttribute.CurrentLang = currentLangId;
            }

            var sb = new StringBuilder();


            for (int i = 0; i < 15; i++)
            {
                switch (i)
                {
                    case 0:
                        sb.Append("ExternalId");
                        break;
                    case 1:
                        sb.Append("Id");
                        break;
                    case 2:
                        sb.Append("Name");
                        break;
                    case 3:
                        sb.Append("Title");
                        break;
                    case 4:
                        sb.Append("Category.Name");
                        break;
                    case 5:
                        sb.Append("OldPrice");
                        break;
                    case 6:
                        sb.Append("Price");
                        break;
                    case 7:
                        sb.Append("IsNew");
                        break;
                    case 8:
                        sb.Append("IsDiscount");
                        break;
                    case 9:
                        sb.Append("IsTopSale");
                        break;
                    case 10:
                        sb.Append("IsActive");
                        break;
                    case 11:
                        sb.Append("ProductStock.StockNumber");
                        break;
                    case 12:
                        sb.Append("ProductStock.Size");
                        break;
                    case 13:
                        sb.Append("ProductStock.Color");
                        break;
                    case 14:
                        sb.Append("ProductStock.IsAvailable");
                        break;

                }
                sb.Append(";");

            }

            foreach (var productAttribute in attributes)
            {
                sb.Append(productAttribute.Id);
                sb.Append(";");
            }

            sb.Append("\r\n");


            for (int i = 0; i < 15; i++)
            {
                switch (i)
                {
                    case 0:
                        sb.Append("Внешний Id");
                        break;
                    case 1:
                        sb.Append("Внутренний Id");
                        break;
                    case 2:
                        sb.Append("Id в строке адреса");
                        break;
                    case 3:
                        sb.Append("Заголовок");
                        break;
                    case 4:
                        sb.Append("Категория");
                        break;
                    case 5:
                        sb.Append("Старая цена");
                        break;
                    case 6:
                        sb.Append("Цена");
                        break;
                    case 7:
                        sb.Append("Новый");
                        break;
                    case 8:
                        sb.Append("Скидка");
                        break;
                    case 9:
                        sb.Append("Хит продаж");
                        break;
                    case 10:
                        sb.Append("Активный");
                        break;
                    case 11:
                        sb.Append("Артикул");
                        break;
                    case 12:
                        sb.Append("Размер");
                        break;
                    case 13:
                        sb.Append("Цвет");
                        break;
                    case 14:
                        sb.Append("Наличие");
                        break;

                }
                sb.Append(";");

            }

            foreach (var productAttribute in attributes)
            {
                sb.Append(productAttribute.Title);
                sb.Append(";");
            }

            sb.Append("\r\n");
            //Product currentProduct = null;
            //Product oldProduct = null;
            foreach (var product in products)
            {
                product.CurrentLang = currentLangId;




                sb.Append(product.ExternalId);
                sb.Append(";");
                sb.Append(product.Id);
                sb.Append(";");
                sb.Append(product.Name);
                sb.Append(";");
                sb.Append(product.Title);
                sb.Append(";");
                sb.Append(product.Category.Name);
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
                // 
                sb.Append(";");
                sb.Append(";");
                sb.Append(";");
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


                foreach (var ps in product.ProductStocks)
                {
                    sb.Append(product.ExternalId);
                    sb.Append(";");
                    sb.Append(product.Id);
                    sb.Append(";");
                    sb.Append(product.Name);
                    sb.Append(";");
                    sb.Append(product.Title);
                    sb.Append(";");
                    sb.Append(product.Category.Name);
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
                    sb.Append(ps.IsAvailable);
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

                        //foreach (var s in result)
                        //{
                        //    sb.Append(s);
                        //}

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
