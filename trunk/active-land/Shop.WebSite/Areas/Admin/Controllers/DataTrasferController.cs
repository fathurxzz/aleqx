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
            _repository.LangId = CurrentLangId;
            var categories = _repository.GetCategories();
            ViewBag.Message = message;
            ViewBag.ErrorCode = errorCode;
            return View(categories);
        }

        public void Export(string categoryName)
        {
            _repository.LangId = CurrentLangId;
            string content = ExportToFile.Execute(_repository, CurrentLangId, categoryName);
            SaveToFile(content, categoryName);
        }

        public ActionResult Import(HttpPostedFileBase fileUpload)
        {
            _repository.LangId = CurrentLangId;
            var result = new ImportResult { ErrorCode = 1, ErrorMessage = "Не выбран файл для загрузки" };
            if (fileUpload != null)
            {
                var reader = new StreamReader(fileUpload.InputStream, System.Text.Encoding.GetEncoding(1251));
                var categoryName = fileUpload.FileName.Split(new[] {"."}, StringSplitOptions.None)[1];
                result = ImportFromFile.Execute(_repository, reader, categoryName, CurrentLangId);
            }
            return RedirectToAction("Index", new { message = result.ErrorMessage, errorCode = result.ErrorCode });
        }

        private void SaveToFile(string text, string categoryName)
        {
            string fileName = "output." + categoryName + "." + DateTime.Now + ".csv";

            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("Content-Length", text.Length.ToString());
            Response.ContentType = "text/plain";
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AppendHeader("content-disposition", "attachment;filename=\"" + fileName + "\"");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding(1251);
            Response.Write(text);
            Response.End();
        }
    }
}
