using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

                while ((line = file.ReadLine()) != null)
                {
                    counter++;
                    string[] x = line.Split(new[] { ";" }, StringSplitOptions.None);
                    if (counter > 2)
                    {
                        string newId = x[0];
                        if (newId != oldId)
                        {
                            if (currentProduct != null)
                            {
                                products.Add(currentProduct);
                            }


                            var product = new ImportedProduct
                            {
                                ExternalId = x[0],
                                //Id = int.Parse(x[1]),
                                Name = x[2],
                                Title = x[3],
                                OldPrice = ConvertToDecimalValue(x[4]),
                                Price = ConvertToDecimalValue(x[5]),
                                IsNew = ConvertToBooleanValue(x[6]),
                                IsDiscount = ConvertToBooleanValue(x[7]),
                                IsTopSale = ConvertToBooleanValue(x[8]),
                                IsActive = ConvertToBooleanValue(x[9])
                            };

                            if (!string.IsNullOrEmpty(x[10]))
                            {
                                var productStock = new ImportedProductStock
                                {
                                    StockNumber = x[10],
                                    Size = x[11],
                                    Color = x[12]

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
                            if (!string.IsNullOrEmpty(x[10]))
                            {
                                var productStock = new ImportedProductStock
                                {
                                    StockNumber = x[10],
                                    Size = x[11],
                                    Color = x[12]

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






                int updatedProducts = 0;
                int newProducts = 0;
                int failedAddingProduct = 0;
                int failedUpdatingProduct = 0;

                foreach (var importedProduct in products)
                {
                    var siteProduct = repository.GetProductByExternalId(importedProduct.ExternalId);
                    //var siteProduct = repository.FindProduct(importedProduct.Id);
                    if (siteProduct != null)
                    {

                        try
                        {
                            if (siteProduct.ProductStocks == null)
                            {
                                siteProduct.ProductStocks = new LinkedList<ProductStock>();
                            }

                            if (importedProduct.ImportedProductStocks != null)
                                foreach (var importedProductStock in importedProduct.ImportedProductStocks)
                                {
                                    if (siteProduct.ProductStocks.FirstOrDefault(ps => ps.StockNumber == importedProductStock.StockNumber) == null)
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
                            updatedProducts++;
                        }
                        catch
                        {
                            failedAddingProduct++;
                        }
                    }
                    else
                    {
                        try
                        {
                            // добавляем новый товар
                            var category = repository.GetCategory(categoryName);
                            var newProduct = new Product { Category = category };
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
                            newProducts++;
                        }
                        catch
                        {
                            failedAddingProduct++;
                        }
                    }
                }


                res.ErrorMessage =
                    string.Format(
                        "Файл успешно загружен. Получено {0} строк. Всего товаров {2}. Обновлено {1} товаров. Добавлено {3} товаров. Не удалось добавить {4} товаров. не удалось обновить {5} товаров",
                        counter, updatedProducts, products.Count, newProducts, failedAddingProduct, failedUpdatingProduct);
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
