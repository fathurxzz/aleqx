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
            return View();
        }


        public void Export()
        {
            var products = _repository.GetAllProducts().ToList();

            var sb = new StringBuilder();


            for (int i = 0; i < 10; i++)
            {
                switch (i)
                {
                    case 0:
                        sb.Append("Id");
                        break;
                    case 1:
                        sb.Append("Name");
                        break;
                    case 2:
                        sb.Append("Title");
                        break;
                    case 3:
                        sb.Append("Category.Name");
                        break;
                    case 4:
                        sb.Append("OldPrice");
                        break;
                    case 5:
                        sb.Append("Price");
                        break;
                    case 6:
                        sb.Append("IsNew");
                        break;
                    case 7:
                        sb.Append("IsDiscount");
                        break;
                    case 8:
                        sb.Append("IsTopSale");
                        break;
                    case 9:
                        sb.Append("IsActive");
                        break;

                }
                sb.Append(";");

            }
            sb.Append("\r\n");

            foreach (var product in products)
            {
                product.CurrentLang = CurrentLangId;
                for (int i = 0; i < 10; i++)
                {
                    switch (i)
                    {
                        case 0:
                            sb.Append(product.Id);
                            break;
                        case 1:
                            sb.Append(product.Name);
                            break;
                        case 2:
                            sb.Append(product.Title);
                            break;
                        case 3:
                            sb.Append(product.Category.Name);
                            break;
                        case 4:
                            sb.Append(product.OldPrice);
                            break;
                        case 5:
                            sb.Append(product.Price);
                            break;
                        case 6:
                            sb.Append(product.IsNew);
                            break;
                        case 7:
                            sb.Append(product.IsDiscount);
                            break;
                        case 8:
                            sb.Append(product.IsTopSale);
                            break;
                        case 9:
                            sb.Append(product.IsActive);
                            break;

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
            Response.AppendHeader("content-disposition", "attachment;filename=\"output.txt\"");
            Response.Write(text);
            Response.End();
        }

    }
}
