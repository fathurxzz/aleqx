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

        public static ImportResult Execute(IShopRepository repository, StreamReader file, string categoryName,  int currentLangId)
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
                                Name = x[1],
                                Title = x[2],
                                OldPrice = ConvertToDecimalValue(x[3]),
                                Price = ConvertToDecimalValue(x[4]),
                                IsNew = ConvertToBooleanValue(x[5]),
                                IsDiscount = ConvertToBooleanValue(x[6]),
                                IsTopSale = ConvertToBooleanValue(x[7]),
                                IsActive = ConvertToBooleanValue(x[8])
                            };

                            if (!string.IsNullOrEmpty(x[9]))
                            {
                                var productStock = new ImportedProductStock
                                {
                                    StockNumber = x[9],
                                    Size = x[10],
                                    Color = x[11]

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
                            if (!string.IsNullOrEmpty(x[9]))
                            {
                                var productStock = new ImportedProductStock
                                {
                                    StockNumber = x[9],
                                    Size = x[10],
                                    Color = x[11]

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






                //var siteProducts = repository.GetProductsByCategory(categoryName);

                int updatedProducts = 0;

                foreach (var importedProduct in products)
                {
                    var siteProduct = repository.GetProductByExternalId(importedProduct.ExternalId);
                    if (siteProduct != null)
                    {
                        if(siteProduct.InjectFromImportProduct(importedProduct))
                        {
                            repository.SaveProduct(siteProduct);
                            updatedProducts++;
                        }
                    }
                    else
                    {
                        // добавляем новый товар
                        var product = new Product();
                        product.InjectFromImportProduct(importedProduct);
                        repository.AddProduct(product);
                    }
                }


                res.ErrorMessage = string.Format("Файл успешно загружен. Получено {0} строк. Всего товаров {2}. Обновлено {1} товаров", counter, updatedProducts, products.Count);
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
