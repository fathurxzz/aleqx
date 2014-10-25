using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Shop.Api.DataSynchronization.Export;
using Shop.Api.DataSynchronization.Import;
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

        public ActionResult Index(string errorCode, string message)
        {
            var categories = _repository.GetCategories();
            ViewBag.Message = message;
            ViewBag.ErrorCode = errorCode;
            return View(categories);
        }

        public void Export(string categoryName)
        {
            string content = ExportToFile.Execute(_repository, CurrentLangId, categoryName);
            SaveToFile(content);
        }

        public ActionResult Import(HttpPostedFileBase fileUpload)
        {
            ImportResult result = new ImportResult { ErrorCode = 1, ErrorMessage = "Не выбран файл для загрузки" };
            if (fileUpload != null)
            {
                StreamReader reader = new StreamReader(fileUpload.InputStream, System.Text.Encoding.GetEncoding(1251));
                result = ImportFromFile.Execute(reader);
            }
            return RedirectToAction("Index", new { message = result.ErrorMessage, errorCode = result.ErrorCode });
        }

        private void SaveToFile(string text)
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("Content-Length", text.Length.ToString());
            Response.ContentType = "text/plain";
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("content-disposition", "attachment;filename=\"output." + DateTime.Now + ".csv\"");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding(1251);
            Response.Write(text);
            Response.End();
        }
    }
}
