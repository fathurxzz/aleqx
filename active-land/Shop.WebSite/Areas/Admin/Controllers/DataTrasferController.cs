using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Repositories;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class DataTrasferController : AdminController
    {
        //
        // GET: /Admin/DataTrasfer/

        public DataTrasferController(IShopRepository repository)
            : base(repository)
        {

        }

        public ActionResult Index()
        {
            var categories = _repository.GetCategories();
            return View(categories);
        }


        public void Export(string categoryName)
        {
            //var products = _repository.GetAllProducts().ToList();
            var products = _repository.GetProductsByCategory(categoryName).ToList();

            var attributes = _repository.GetProductAttributes(categoryName).ToList();
            foreach (var productAttribute in attributes)
            {
                productAttribute.CurrentLang = CurrentLangId;
            }

            var sb = new StringBuilder();


            for (int i = 0; i < 11; i++)
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

                }
                sb.Append(";");

            }

            foreach (var productAttribute in attributes)
            {
                sb.Append(productAttribute.Id);
                sb.Append(";");
            }

            sb.Append("\r\n");


            for (int i = 0; i < 11; i++)
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

                }
                sb.Append(";");

            }

            foreach (var productAttribute in attributes)
            {
                sb.Append(productAttribute.Title);
                sb.Append(";");
            }

            sb.Append("\r\n");

            foreach (var product in products)
            {
                product.CurrentLang = CurrentLangId;
                for (int i = 0; i < 11; i++)
                {
                    switch (i)
                    {
                        case 0:
                            sb.Append(product.ExternalId);
                            break;
                        case 1:
                            sb.Append(product.Id);
                            break;
                        case 2:
                            sb.Append(product.Name);
                            break;
                        case 3:
                            sb.Append(product.Title);
                            break;
                        case 4:
                            sb.Append(product.Category.Name);
                            break;
                        case 5:
                            sb.Append(product.OldPrice);
                            break;
                        case 6:
                            sb.Append(product.Price);
                            break;
                        case 7:
                            sb.Append(product.IsNew);
                            break;
                        case 8:
                            sb.Append(product.IsDiscount);
                            break;
                        case 9:
                            sb.Append(product.IsTopSale);
                            break;
                        case 10:
                            sb.Append(product.IsActive);
                            break;

                    }
                    sb.Append(";");
                    
                }

                foreach (var productAttribute in attributes)
                {
                     var result = new List<string>();

                    foreach (var pav in product.ProductAttributeValues)
                    {
                        pav.CurrentLang = CurrentLangId;
                        if (productAttribute.ProductAttributeValues.Contains(pav))
                        {
                            result.Add(pav.Title);
                        }
                    }

                    //foreach (var s in result)
                    //{
                    //    sb.Append(s);
                    //}

                    var res = ConvertToStringWithSeparators(result);
                    if (res != null)
                    {
                        sb.Append(res);
                    }

                    sb.Append(";");
                }


                sb.Append("\r\n");
            }
            string text = sb.ToString();
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("Content-Length", text.Length.ToString());
            Response.ContentType = "text/plain";
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("content-disposition", "attachment;filename=\"output.txt\"");
            //Response.AppendHeader("content-disposition", "attachment;filename=\"output.xls\"");
            Response.Write(text);
            Response.End();
        }


        private string ConvertToStringWithSeparators(List<string> source)
        {
            if (!source.Any())
            {
                return null;
            }
            var sb = new StringBuilder();
            for (int i = 0; i < source.Count(); i++)
            {
                if (i == source.Count() - 1)
                {
                    sb.Append(source[i]);
                }
                else
                {
                    sb.Append(source[i]+"#");
                }
            }
            return sb.ToString();
        }

    }



    
}
