using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using Shop.Api.DataSynchronization.Export;
using Shop.Api.DataSynchronization.Import;
using Shop.DataAccess;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Areas.Admin.Models;
using Shop.WebSite.Models;

namespace Shop.WebSite.Areas.Admin.Controllers
{
    public class DataTrasferController : AdminController
    {
        protected static readonly ILog Log = LogManager.GetLogger(typeof(CatalogueModel));
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
            foreach (var category in categories)
            {
                category.CurrentLang = CurrentLangId;
            }
            return View(new DataTransferModel{Categories = categories,ImportResult = new ImportResult{ErrorCode = -1}} );
        }


        private void RefreshSearchData()
        {
            try
            {

                Log.DebugFormat("{0} start updating search metadata", DateTime.Now);
                _repository.LangId = CurrentLangId;
                var products = _repository.GetAllProducts().ToList();

                var totalProducst = products.Count;
                var productsCount = 0;
                foreach (var p in products)
                {
                    productsCount++;
                    var product = _repository.GetProduct(p.Id);
                    string searchCriteriaAttributes = "";
                    foreach (var productAttributeValue in product.ProductAttributeValues)
                    {
                        searchCriteriaAttributes += productAttributeValue.ProductAttributeId + "-" + productAttributeValue.Id + ";";
                    }
                    product.SearchCriteriaAttributes = searchCriteriaAttributes;
                    _repository.SaveProduct(product);
                    Log.DebugFormat("{0} product search metadata updated {1} of {2}", DateTime.Now, productsCount, totalProducst);
                }
                Log.DebugFormat("{0} finish updating search metadata", DateTime.Now);
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("updating search metadata error {0}" , ex.Message);
            }
        }


        public ActionResult RefreshData()
        {

            RefreshSearchData();
            
            return RedirectToAction("Index");
        }

        public void Export(string categoryName)
        {
            _repository.LangId = CurrentLangId;
            string content = ExportToFile.Execute(_repository, CurrentLangId, categoryName);
            SaveToFile(content, categoryName);
        }


        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public ActionResult Import(HttpPostedFileBase fileUpload)
        {
            Log.DebugFormat("{0} start importing products ", DateTime.Now);
            _repository.LangId = CurrentLangId;
            var categories = _repository.GetCategories().ToList();
            var result = new ImportResult { ErrorCode = 1, ErrorMessage = "Не выбран файл для загрузки" };
            if (fileUpload != null)
            {
                var reader = new StreamReader(fileUpload.InputStream, System.Text.Encoding.GetEncoding(1251));
                var categoryName = fileUpload.FileName.Split(new[] {"."}, StringSplitOptions.None)[1];
                result = ImportFromFile.Execute(_repository, reader, categoryName, CurrentLangId);
            }

            if (result.ErrorCode == 0)
            {
                RefreshSearchData();
                

                Log.DebugFormat("{0} finish importing products ", DateTime.Now);
            }
            Cache.Default.Clear();

            return View("Index", new DataTransferModel {Categories = categories, ImportResult = result});
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
