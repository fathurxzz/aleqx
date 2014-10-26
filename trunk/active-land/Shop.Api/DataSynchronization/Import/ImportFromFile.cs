using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using Shop.Api.DataSynchronization.Model;
using Shop.DataAccess.Entities;
using Shop.DataAccess.Repositories;

namespace Shop.Api.DataSynchronization.Import
{
    public class ImportFromFile
    {
        private static decimal ConvertToDecimalValue(string value, decimal defaultValue = 0)
        {
            decimal result;
            return decimal.TryParse(value, out result) ? result : defaultValue;
        }

        private static bool ConvertToBooleanValue(string value, bool defaultValue = false)
        {
            bool result;
            return bool.TryParse(value, out result) ? result : defaultValue;
        }

        public static ImportResult Execute(IShopRepository repository, StreamReader file, string categoryName, int currentLangId)
        {
            var res = new ImportResult { ErrorCode = 0 };
            var products = new List<ImportedProduct>();
            int counter = 0;
            try
            {
                string line;
                string oldId = string.Empty;
                ImportedProduct currentProduct = null;

                var fieldMapping = new Dictionary<string, int>();
                var attributeMapping = new Dictionary<string, int>();


                while ((line = file.ReadLine()) != null)
                {
                    counter++;
                    string[] x = line.Split(new[] { ";" }, StringSplitOptions.None);
                    if (counter == 1)
                    {
                        for (int i = 0; i < x.Length; i++)
                        {
                            if(string.IsNullOrEmpty(x[i]))
                                throw new Exception("Cannot get field name");

                            if (TransferData.ProductFields.ContainsKey(x[i]))
                            {
                                fieldMapping.Add(x[i], i);
                            }
                            else
                            {
                                attributeMapping.Add(x[i], i);
                            }
                        }
                    }
                    if (counter > 2)
                    {
                        string newId = x[fieldMapping["ExternalId"]];
                        if (newId != oldId)
                        {
                            if (currentProduct != null)
                            {
                                products.Add(currentProduct);
                            }

                            var product = new ImportedProduct
                            {
                                ExternalId = x[fieldMapping["ExternalId"]],
                                Name = x[fieldMapping["Name"]],
                                Title = x[fieldMapping["Title"]],
                                OldPrice = ConvertToDecimalValue(x[fieldMapping["OldPrice"]]),
                                Price = ConvertToDecimalValue(x[fieldMapping["Price"]]),
                                IsNew = ConvertToBooleanValue(x[fieldMapping["IsNew"]]),
                                IsDiscount = ConvertToBooleanValue(x[fieldMapping["IsDiscount"]]),
                                IsTopSale = ConvertToBooleanValue(x[fieldMapping["IsTopSale"]]),
                                IsActive = ConvertToBooleanValue(x[fieldMapping["IsActive"]])
                            };

                            if (!string.IsNullOrEmpty(x[fieldMapping["ProductStock.StockNumber"]]))
                            {
                                var productStock = new ImportedProductStock
                                {
                                    StockNumber = x[fieldMapping["ProductStock.StockNumber"]],
                                    Size = x[fieldMapping["ProductStock.Size"]],
                                    Color = x[fieldMapping["ProductStock.Color"]]

                                };

                                product.ImportedProductStocks = new List<ImportedProductStock>
                                {
                                    productStock
                                };
                            }

                            currentProduct = product;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(x[fieldMapping["ProductStock.StockNumber"]]))
                            {
                                var productStock = new ImportedProductStock
                                {
                                    StockNumber = x[fieldMapping["ProductStock.StockNumber"]],
                                    Size = x[fieldMapping["ProductStock.Size"]],
                                    Color = x[fieldMapping["ProductStock.Color"]]

                                };
                                if (currentProduct != null)
                                {
                                    if (currentProduct.ImportedProductStocks == null)
                                    {
                                        currentProduct.ImportedProductStocks = new List<ImportedProductStock>();
                                    }
                                    currentProduct.ImportedProductStocks.Add(productStock);
                                }
                            }
                        }

                        oldId = newId;
                    }
                }

                if (currentProduct != null)
                {
                    products.Add(currentProduct);
                }






                

                foreach (var importedProduct in products)
                {

                    if (!string.IsNullOrEmpty(importedProduct.ExternalId))
                    {

                        var siteProduct = repository.GetProductByExternalId(importedProduct.ExternalId);
                        //var siteProduct = repository.FindProduct(importedProduct.Id);
                        if (siteProduct != null)
                        {

                            try
                            {
                                siteProduct.InjectFromImportProduct(importedProduct);
                                if (siteProduct.ProductStocks == null)
                                {
                                    siteProduct.ProductStocks = new LinkedList<ProductStock>();
                                }

                                if (importedProduct.ImportedProductStocks != null)
                                    foreach (var importedProductStock in importedProduct.ImportedProductStocks)
                                    {
                                        if (
                                            siteProduct.ProductStocks.FirstOrDefault(
                                                ps => ps.StockNumber == importedProductStock.StockNumber) == null)
                                        {
                                            siteProduct.ProductStocks.Add(new ProductStock
                                            {
                                                StockNumber = importedProductStock.StockNumber,
                                                Color = importedProductStock.Color,
                                                Size = importedProductStock.Size
                                            });
                                        }
                                    }

                                repository.SaveProduct(siteProduct);
                                res.UpdatedProductCount++;
                            }
                            catch
                            {
                                res.UpdateProductFailedCount++;
                            }
                        }
                        else
                        {
                            try
                            {
                                // добавляем новый товар
                                var category = repository.GetCategory(categoryName);
                                var newProduct = new Product {Category = category};
                                newProduct.InjectFromImportProduct(importedProduct);
                                if (importedProduct.ImportedProductStocks != null)
                                {
                                    foreach (var importedProductStock in importedProduct.ImportedProductStocks)
                                    {
                                        newProduct.ProductStocks.Add(new ProductStock
                                        {
                                            StockNumber = importedProductStock.StockNumber,
                                            Color = importedProductStock.Color,
                                            Size = importedProductStock.Size
                                        });

                                    }
                                }
                                newProduct.SearchCriteriaAttributes = "";
                                repository.AddProduct(newProduct);
                                res.NewProductCount++;
                            }
                            catch
                            {
                                res.AddProductFailedCount++;
                            }
                        }
                    }
                    else
                    {
                        res.ReadProductFailedCount++;
                    }
                }


                res.TotalRows = counter - 2;
                res.ProductCount = products.Count;


                res.ErrorMessage =
                    string.Format(
                        "Файл успешно загружен. Получено {0} строк. Всего товаров {2}. Обновлено {1} товаров. Добавлено {3} товаров. Не удалось добавить {4} товаров. не удалось обновить {5} товаров. Товаров с нераспознанным ExternalId {6}",
                        res.TotalRows, res.UpdatedProductCount, res.ProductCount, res.NewProductCount, res.AddProductFailedCount, res.UpdateProductFailedCount, res.ReadProductFailedCount);
            }
            catch (Exception ex)
            {
                res.ErrorMessage = string.Format("Ошибка чтения файла. {0}", ex.Message);
                res.ErrorCode = 1;
            }
            finally
            {
                file.Close();
            }

            return res;
        }
    }
}
