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


        private static List<ImportedProduct> ReadProductsFromFile(StreamReader file)
        {
            var products = new List<ImportedProduct>();
            int counter = 0;

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
                        if (string.IsNullOrEmpty(x[i]))
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
                            IsActive = ConvertToBooleanValue(x[fieldMapping["IsActive"]]),
                            ImportedProductStocks = new List<ImportedProductStock>(),
                            //ImportedProductAttibutes = new Dictionary<string, string>()
                            ImportedProductAttibutes = new List<ImportedProductAttribute>()
                        };

                        if (!string.IsNullOrEmpty(x[fieldMapping["ProductStock.StockNumber"]]))
                        {
                            product.ImportedProductStocks.Add(new ImportedProductStock
                            {
                                StockNumber = x[fieldMapping["ProductStock.StockNumber"]],
                                Size = x[fieldMapping["ProductStock.Size"]],
                                Color = x[fieldMapping["ProductStock.Color"]]
                            });
                        }


                        foreach (var attr in attributeMapping)
                        {
                            var importedAttributes = x[attributeMapping[attr.Key]];
                            string[] attributes = importedAttributes.Split(new[] {"#"}, StringSplitOptions.RemoveEmptyEntries);
                            var importedProductAttribute = new ImportedProductAttribute
                            {
                                ExternalId = attr.Key
                            };
                            foreach (var attribute in attributes)
                            {
                                importedProductAttribute.Values.Add(attribute);
                            }
                            product.ImportedProductAttibutes.Add(importedProductAttribute);
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

            return products;
        }


        public static ImportResult Execute(IShopRepository repository, StreamReader file, string categoryName, int currentLangId)
        {

            var res = new ImportResult { ErrorCode = 0 };
            //var products = new List<ImportedProduct>();
            try
            {
                List<ImportedProduct> products = ReadProductsFromFile(file);
                List<int> productStockToDelete = new List<int>();
                foreach (var importedProduct in products)
                {
                    if (string.IsNullOrEmpty(importedProduct.ExternalId))
                    {
                        res.ReadProductFailedCount++;
                        continue;
                    }

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



                            foreach (var productStock in siteProduct.ProductStocks)
                            {
                                var importedProductStock = importedProduct.ImportedProductStocks.FirstOrDefault(ips => ips.StockNumber == productStock.StockNumber && !ips.Imported);
                                if (importedProductStock != null)
                                {
                                    // update productStock in siteProduct.ProductStocks
                                    importedProductStock.Imported = true;
                                    productStock.Size = importedProductStock.Size;
                                    productStock.Color = importedProductStock.Color;
                                    res.UpdatedArticles++;
                                }
                                else
                                {
                                    // remove productStock from siteProduct.ProductStocks
                                    //repository.DeleteProductStock(productStock.Id);
                                    productStockToDelete.Add(productStock.Id);
                                    res.DeletedArticles++;
                                }
                            }



                            foreach (var importedProductStock in importedProduct.ImportedProductStocks.Where(ips => !ips.Imported))
                            {
                                siteProduct.ProductStocks.Add(new ProductStock
                                {
                                    StockNumber = importedProductStock.StockNumber,
                                    Color = importedProductStock.Color,
                                    Size = importedProductStock.Size
                                });
                                res.AddedArticles++;
                            }


                            //if (importedProduct.ImportedProductStocks != null)
                            //{
                            //    foreach (var importedProductStock in importedProduct.ImportedProductStocks)
                            //    {
                            //        if (siteProduct.ProductStocks.FirstOrDefault(ps => ps.StockNumber == importedProductStock.StockNumber) == null)
                            //        {
                            //            siteProduct.ProductStocks.Add(new ProductStock
                            //            {
                            //                StockNumber = importedProductStock.StockNumber,
                            //                Color = importedProductStock.Color,
                            //                Size = importedProductStock.Size
                            //            });
                            //        }
                            //    }
                            //}


                            // TODO: add updating product attributes
                            foreach (var productAttributeValue in siteProduct.ProductAttributeValues)
                            {
                                var productAttributeExternalId = productAttributeValue.ProductAttribute.ExternalId;

                                foreach (var importedProductAttibute in importedProduct.ImportedProductAttibutes.Where(pa => pa.ExternalId == productAttributeExternalId))
                                {
                                    importedProductAttibute.Imported = true;


                                }
                            }

                            //repository.GetProductAttribute()




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
                            res.NewProductCount++;
                        }
                        catch
                        {
                            res.AddProductFailedCount++;
                        }
                    }
                }

                foreach (var id in productStockToDelete)
                {
                    repository.DeleteProductStock(id);
                }



                res.ProductCount = products.Count;
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
