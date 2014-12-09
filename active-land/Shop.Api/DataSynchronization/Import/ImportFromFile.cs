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

        private static string ParseStrinfValue(string source)
        {
            if (source.StartsWith("\"") && source.EndsWith("\""))
            {
                source = source.Substring(1, source.Length - 2).Replace("\"\"", "\"");
            }
            return source;
        }
        

        private static List<ImportedProduct> ReadProductsFromFile(StreamReader file)
        {
            int rowCounter = 0;
            string rowExternalId = string.Empty;
            var products = new List<ImportedProduct>();
            //int counter = 0;

            string line;
            string oldId = string.Empty;
            ImportedProduct currentProduct = null;

            var fieldMapping = new Dictionary<string, int>();
            var attributeMapping = new Dictionary<string, int>();


            try
            {
                while ((line = file.ReadLine()) != null)
                {
                    rowCounter++;
                    string[] x = line.Split(new[] { ";" }, StringSplitOptions.None);
                    if (rowCounter == 1)
                    {
                        for (int i = 0; i < x.Length; i++)
                        {
                            if (string.IsNullOrEmpty(x[i]))
                                throw new Exception("Невозможно прочитать имя поля");

                            if (TransferData.ProductFields.ContainsKey(x[i]))
                            {
                                if (fieldMapping.ContainsKey(x[i]))
                                    throw new Exception("Дубль поля " + x[i] + ". Данное поле уже добавлено в коллекцию");
                                fieldMapping.Add(x[i], i);
                            }
                            else
                            {
                                if (attributeMapping.ContainsKey(x[i]))
                                    throw new Exception("Дубль атрибута " + x[i] + ". Данный атрибут уже добавлен в коллекцию");
                                attributeMapping.Add(x[i], i);
                            }
                        }
                    }
                    if (rowCounter > 2)
                    {
                        string newId = x[fieldMapping["ExternalId"]];
                        rowExternalId = newId;
                        if (newId != oldId)
                        {
                            if (currentProduct != null)
                            {
                                if (products.FirstOrDefault(p => p.ExternalId == currentProduct.ExternalId) == null)
                                {
                                    products.Add(currentProduct);
                                }
                                else
                                {
                                    throw new Exception(string.Format("Дублирование товара {0}", currentProduct.ExternalId));
                                }
                            }

                            var product = new ImportedProduct
                            {
                                ExternalId = ParseStrinfValue(x[fieldMapping["ExternalId"]]),
                                Name = ParseStrinfValue(x[fieldMapping["Name"]]),
                                Title = ParseStrinfValue(x[fieldMapping["Title"]]),
                                OldPrice = ConvertToDecimalValue(x[fieldMapping["OldPrice"]]),
                                Price = ConvertToDecimalValue(x[fieldMapping["Price"]]),
                                IsNew = ConvertToBooleanValue(x[fieldMapping["IsNew"]]),
                                IsDiscount = ConvertToBooleanValue(x[fieldMapping["IsDiscount"]]),
                                IsTopSale = ConvertToBooleanValue(x[fieldMapping["IsTopSale"]]),
                                IsActive = ConvertToBooleanValue(x[fieldMapping["IsActive"]]),

                                //SeoDescription = x[fieldMapping["SeoDescription"]],
                                //SeoKeywords = x[fieldMapping["SeoKeywords"]],
                                //SeoText = x[fieldMapping["SeoText"]],

                                ImportedProductStocks = new List<ImportedProductStock>(),
                                //ImportedProductAttibutes = new Dictionary<string, string>()
                                ImportedProductAttibutes = new List<ImportedProductAttribute>()
                            };

                            if (fieldMapping.ContainsKey("SeoDescription"))
                            {
                                product.SeoDescription = ParseStrinfValue(x[fieldMapping["SeoDescription"]]);
                            }
                            if (fieldMapping.ContainsKey("SeoKeywords"))
                            {
                                product.SeoKeywords = ParseStrinfValue(x[fieldMapping["SeoKeywords"]]);
                            }
                            if (fieldMapping.ContainsKey("SeoText"))
                            {
                                product.SeoText = ParseStrinfValue(x[fieldMapping["SeoText"]]);
                            }

                            if (!string.IsNullOrEmpty(x[fieldMapping["ProductStock.StockNumber"]]))
                            {
                                product.ImportedProductStocks.Add(new ImportedProductStock
                                {
                                    StockNumber = ParseStrinfValue(x[fieldMapping["ProductStock.StockNumber"]]),
                                    Size = ParseStrinfValue(x[fieldMapping["ProductStock.Size"]]),
                                    Color = ParseStrinfValue(x[fieldMapping["ProductStock.Color"]])
                                });
                            }




                            foreach (var attr in attributeMapping)
                            {
                                var importedAttributes = x[attributeMapping[attr.Key]];
                                string[] attributes = importedAttributes.Split(new[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
                                var importedProductAttribute = new ImportedProductAttribute
                                {
                                    ExternalId = attr.Key,
                                    Values = new List<string>()
                                };
                                foreach (var attribute in attributes)
                                {
                                    importedProductAttribute.Values.Add(ParseStrinfValue(attribute));
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
                                    StockNumber = ParseStrinfValue(x[fieldMapping["ProductStock.StockNumber"]]),
                                    Size = ParseStrinfValue(x[fieldMapping["ProductStock.Size"]]),
                                    Color = ParseStrinfValue(x[fieldMapping["ProductStock.Color"]])

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
                    if (products.FirstOrDefault(p => p.ExternalId == currentProduct.ExternalId) == null)
                    {
                        products.Add(currentProduct);
                    }
                    else
                    {
                        throw new Exception(string.Format("Дублирование товара {0}", currentProduct.ExternalId));
                    }
                }

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ошибка чтения файла. {0}", ex.Message + " строка:" + rowCounter + " ExternalId:" + rowExternalId));
            }
        }


        public static ImportResult Execute(IShopRepository repository, StreamReader file, string categoryName, int currentLangId)
        {

            var res = new ImportResult { ErrorCode = 0, ProductErrors = new Dictionary<string, string>()};
            //var products = new List<ImportedProduct>();
            try
            {
                bool justAdded = false;
                int productCount = 0;

                List<ImportedProduct> products = ReadProductsFromFile(file);

                List<int> productStockToDelete = new List<int>();
                foreach (var importedProduct in products)
                {
                    productCount++;
                    if (string.IsNullOrEmpty(importedProduct.ExternalId))
                    {
                        res.ReadProductFailedCount++;
                        continue;
                    }



                    var siteProduct = repository.GetProductByExternalId(importedProduct.ExternalId);

                    if (siteProduct == null)
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
                            justAdded = true;
                            res.NewProductCount++;
                        }
                        catch(Exception ex)
                        {
                            res.ProductErrors.Add("Не удалось добавить " + importedProduct.ExternalId, ex.Message);
                            res.AddProductFailedCount++;
                        }
                    }

                    siteProduct = repository.GetProductByExternalId(importedProduct.ExternalId);

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



                            foreach (var attributeGroup in importedProduct.ImportedProductAttibutes)
                            {
                                var attrToDelete = new List<int>();
                                var exId = attributeGroup.ExternalId;
                                var siteAttr = siteProduct.ProductAttributeValues.Where(pav => pav.ProductAttribute.ExternalId == exId).Select(a => a.Title).ToList();

                                var xx = siteAttr.Except(attributeGroup.Values).ToList(); //  items to delete
                                var xy = attributeGroup.Values.Except(siteAttr).ToList(); //  items to add

                                List<string> addedAttr = new List<string>();

                                foreach (var attr in siteProduct.ProductAttributeValues.Where(pav => pav.ProductAttribute.ExternalId == exId))
                                {
                                    if (xx.Contains(attr.Title))
                                    {
                                        //siteProduct.ProductAttributeValues.Remove(attr);
                                        attrToDelete.Add(attr.Id);
                                    }
                                }

                                foreach (var id in attrToDelete)
                                {
                                    var a = repository.GetProductAttributeValue(id);
                                    siteProduct.ProductAttributeValues.Remove(a);
                                }


                                var productAttributes = repository.GetProductAttributes(siteProduct.CategoryId);

                                var productAttribute = productAttributes.FirstOrDefault(pa => pa.ExternalId == exId);
                                if (productAttribute == null)
                                    throw new Exception("Атрибут с идентификатором " + exId + " не найден");
                                foreach (var attributeValue in productAttribute.ProductAttributeValues)
                                {
                                    if (xy.Contains(attributeValue.Title))
                                    {
                                        siteProduct.ProductAttributeValues.Add(attributeValue);
                                        addedAttr.Add(attributeValue.Title);
                                    }
                                }


                                var attrNotFoundOnSite = xy.Except(addedAttr);
                                foreach (string s in attrNotFoundOnSite)
                                {
                                    var newProductAttributeValue = new ProductAttributeValue
                                    {
                                        CurrentLang = currentLangId,
                                        Title = s,
                                        ProductAttribute = productAttribute,
                                    };
                                    newProductAttributeValue.Products.Add(siteProduct);

                                    repository.AddProductAttributeValue(newProductAttributeValue);
                                }


                            }


                            repository.SaveProduct(siteProduct);

                            if (!justAdded)
                            {
                                res.UpdatedProductCount++;
                            }
                            else
                            {
                                justAdded = false;
                            }


                        }
                        catch (Exception ex)
                        {
                            res.ProductErrors.Add("Не удалось обновить " + siteProduct.ExternalId, ex.Message);
                            res.UpdateProductFailedCount++;
                        }
                    }
                    
                    //else
                    //{
                        

                    //    try
                    //    {
                    //        // добавляем новый товар
                    //        var category = repository.GetCategory(categoryName);
                    //        var newProduct = new Product { Category = category };
                    //        newProduct.InjectFromImportProduct(importedProduct);
                    //        if (importedProduct.ImportedProductStocks != null)
                    //        {
                    //            foreach (var importedProductStock in importedProduct.ImportedProductStocks)
                    //            {
                    //                newProduct.ProductStocks.Add(new ProductStock
                    //                {
                    //                    StockNumber = importedProductStock.StockNumber,
                    //                    Color = importedProductStock.Color,
                    //                    Size = importedProductStock.Size
                    //                });

                    //            }
                    //        }
                    //        newProduct.SearchCriteriaAttributes = "";
                    //        repository.AddProduct(newProduct);
                    //        res.NewProductCount++;
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        res.ProductErrors.Add("Не удалось добавить " + importedProduct.ExternalId, ex.Message);
                    //        res.AddProductFailedCount++;
                    //    }
                    //}
                }

                foreach (var id in productStockToDelete)
                {
                    repository.DeleteProductStock(id);
                }



                res.ProductCount = products.Count;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = ex.Message;
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
