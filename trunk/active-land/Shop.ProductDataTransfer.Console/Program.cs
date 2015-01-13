using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Api.DataSynchronization.Import;
using Shop.Api.DataSynchronization.Model;
using Shop.Api.Repositories;
using Shop.DataAccess;
using Shop.DataAccess.Entities;
using Shop.DataAccess.EntityFramework;
using Shop.DataAccess.Repositories;

namespace Shop.ProductDataTransfer.Console
{
    class Program
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

        private static string ParseStringValue(string source)
        {
            if (source.StartsWith("\"") && source.EndsWith("\""))
            {
                source = source.Substring(1, source.Length - 2).Replace("\"\"", "\"");
            }
            return source;
        }


        private static void Main(string[] args)
        {
            Run(args);

            //string[] x = {"3", "2", "5", "9"};

            //var aaa = x.FirstOrDefault(v => v == "2")??x.FirstOrDefault();

            //System.Console.WriteLine(aaa);

        }

        private static void Run(string[] args)
        {
            int currentLangId = 1;
            IShopStore store = new ShopStore();

            IShopRepository repository = new ShopRepository(store);
            repository.LangId = currentLangId;


            string filename = args.Length == 0 ? "1 output.bikes.27.12.2014 13_15_56.csv" : args[0];

            int rowCounter = 0;
            string rowExternalId = string.Empty;
            var products = new List<ImportedProduct>();
            string oldId = string.Empty;
            ImportedProduct currentProduct = null;
            var fieldMapping = new Dictionary<string, int>();
            var attributeMapping = new Dictionary<string, int>();
            int failedProducts = 0;
            var failedProductsExternalIds = new List<string>();

            try
            {
                System.Console.WriteLine("Чтение файла {0}...", filename);
                var lines = System.IO.File.ReadLines(filename, Encoding.GetEncoding(1251)).ToList();
                System.Console.WriteLine("Всего строк: {0}", lines.Count);
                System.Console.WriteLine("Формирование списка товаров...");

                foreach (var line in lines)
                {
                    rowCounter++;
                    string[] x = line.Split(new[] { ";" }, StringSplitOptions.None);
                    if (rowCounter == 1)
                    {
                        System.Console.WriteLine("Чтение полей и атрибутов товара");
                        for (int i = 0; i < x.Length; i++)
                        {
                            if (string.IsNullOrEmpty(x[i]))
                                throw new Exception("Невозможно прочитать имя поля");

                            if (TransferData.ProductFields.ContainsKey(x[i]))
                            {
                                if (fieldMapping.ContainsKey(x[i]))
                                    throw new Exception("Дубль поля " + x[i] + ". Данное поле уже добавлено в коллекцию");
                                fieldMapping.Add(x[i], i);
                                System.Console.WriteLine("Добвлено поле {0}", x[i]);
                            }
                            else
                            {
                                if (attributeMapping.ContainsKey(x[i]))
                                    throw new Exception("Дубль атрибута " + x[i] + ". Данный атрибут уже добавлен в коллекцию");
                                attributeMapping.Add(x[i], i);
                                System.Console.WriteLine("Добвлен атрибут {0}", x[i]);
                            }
                        }

                        System.Console.WriteLine("Всего полей: {0}", fieldMapping.Count);
                        System.Console.WriteLine("Всего атрибутов: {0}", attributeMapping.Count);

                    }
                    if (rowCounter > 2)
                    {

                        //string newId = x[fieldMapping["ExternalId"]];

                        //rowExternalId = newId;

                        rowExternalId = x[fieldMapping["ExternalId"]];


                        ImportedProduct product = products.FirstOrDefault(p => p.ExternalId == rowExternalId);
                        if(product==null)
                        {
                            product = new ImportedProduct
                            {
                                ExternalId = ParseStringValue(x[fieldMapping["ExternalId"]]),
                                Name = ParseStringValue(x[fieldMapping["Name"]]),
                                Title = ParseStringValue(x[fieldMapping["Title"]]),
                                OldPrice = ConvertToDecimalValue(x[fieldMapping["OldPrice"]]),
                                Price = ConvertToDecimalValue(x[fieldMapping["Price"]]),
                                IsNew = ConvertToBooleanValue(x[fieldMapping["IsNew"]]),
                                IsDiscount = ConvertToBooleanValue(x[fieldMapping["IsDiscount"]]),
                                IsTopSale = ConvertToBooleanValue(x[fieldMapping["IsTopSale"]]),
                                //IsActive = ConvertToBooleanValue(x[fieldMapping["IsActive"]]),

                                //SeoDescription = x[fieldMapping["SeoDescription"]],
                                //SeoKeywords = x[fieldMapping["SeoKeywords"]],
                                //SeoText = x[fieldMapping["SeoText"]],

                                ImportedProductStocks = new List<ImportedProductStock>(),
                                //ImportedProductAttibutes = new Dictionary<string, string>()
                                ImportedProductAttibutes = new List<ImportedProductAttribute>()
                            };

                            if (fieldMapping.ContainsKey("SeoDescription"))
                            {
                                product.SeoDescription = ParseStringValue(x[fieldMapping["SeoDescription"]]);
                            }
                            if (fieldMapping.ContainsKey("SeoKeywords"))
                            {
                                product.SeoKeywords = ParseStringValue(x[fieldMapping["SeoKeywords"]]);
                            }
                            if (fieldMapping.ContainsKey("SeoText"))
                            {
                                product.SeoText = ParseStringValue(x[fieldMapping["SeoText"]]);
                            }

                            if (!string.IsNullOrEmpty(x[fieldMapping["ProductStock.StockNumber"]]))
                            {
                                product.ImportedProductStocks.Add(new ImportedProductStock
                                {
                                    StockNumber = ParseStringValue(x[fieldMapping["ProductStock.StockNumber"]]),
                                    Size = ParseStringValue(x[fieldMapping["ProductStock.Size"]]),
                                    Color = ParseStringValue(x[fieldMapping["ProductStock.Color"]]),
                                    IsAvailable = ConvertToBooleanValue(x[fieldMapping["ProductStock.IsAvailable"]]),
                                    Price = ConvertToDecimalValue(x[fieldMapping["Price"]]),
                                    OldPrice = ConvertToDecimalValue(x[fieldMapping["OldPrice"]])
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
                                    importedProductAttribute.Values.Add(ParseStringValue(attribute));
                                }
                                product.ImportedProductAttibutes.Add(importedProductAttribute);
                            }

                            products.Add(product);

                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(x[fieldMapping["ProductStock.StockNumber"]]))
                            {
                                product.ImportedProductStocks.Add(new ImportedProductStock
                                {
                                    StockNumber = ParseStringValue(x[fieldMapping["ProductStock.StockNumber"]]),
                                    Size = ParseStringValue(x[fieldMapping["ProductStock.Size"]]),
                                    Color = ParseStringValue(x[fieldMapping["ProductStock.Color"]]),
                                    IsAvailable = ConvertToBooleanValue(x[fieldMapping["ProductStock.IsAvailable"]]),
                                    Price = ConvertToDecimalValue(x[fieldMapping["Price"]]),
                                    OldPrice = ConvertToDecimalValue(x[fieldMapping["OldPrice"]])
                                });
                            }

                            foreach (var attr in attributeMapping)
                            {
                                var importedAttributes = x[attributeMapping[attr.Key]];
                                string[] attributes = importedAttributes.Split(new[] { "#" }, StringSplitOptions.RemoveEmptyEntries);


                                var importedProductAttribute = product.ImportedProductAttibutes.FirstOrDefault(ipa => ipa.ExternalId == attr.Key);
                                if (importedProductAttribute == null)
                                {
                                    importedProductAttribute = new ImportedProductAttribute
                                    {
                                        ExternalId = attr.Key,
                                        Values = new List<string>()
                                    };
                                    foreach (var attribute in attributes)
                                    {
                                        var value = ParseStringValue(attribute);
                                        importedProductAttribute.Values.Add(value);
                                    }
                                    product.ImportedProductAttibutes.Add(importedProductAttribute);
                                }
                                else
                                {
                                    foreach (var attribute in attributes)
                                    {
                                        var value = ParseStringValue(attribute);
                                        if (!importedProductAttribute.Values.Contains(value))
                                        {
                                            importedProductAttribute.Values.Add(value);
                                        }
                                    }
                                }
                                
                            }
                        }


                       
                    }
                }

                foreach (var importedProduct in products)
                {
                    var price = importedProduct.ImportedProductStocks.FirstOrDefault(p => p.IsAvailable);
                    importedProduct.Price = price != null ? price.Price : 0;
                    importedProduct.OldPrice = price != null ? price.OldPrice : 0;
                }

                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("Прочитано и создано товаров: {0}", products.Count);

                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Найдено дублей: {0}", failedProducts);
                foreach (var failedProductsExternalId in failedProductsExternalIds)
                {
                    System.Console.WriteLine(failedProductsExternalId);
                }
                System.Console.ForegroundColor = ConsoleColor.White;


                //foreach (var product in products)
                //{
                //    System.Console.WriteLine(product);
                //}

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ошибка чтения файла. {0}", ex.Message + " строка:" + rowCounter + " ExternalId:" + rowExternalId));
            }



            



            var categoryName = filename.Split(new[] { "." }, StringSplitOptions.None)[1];


            System.Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Старт процедуры апдейта товаров:");
            System.Console.ForegroundColor = ConsoleColor.White;




            var res = new ImportResult { ErrorCode = 0, ProductErrors = new Dictionary<string, string>() };


            try
            {
                bool justAdded = false;
                int productCount = 0;


                List<int> productStockToDelete = new List<int>();


                System.Console.WriteLine("Всего товаров: {0}", products.Count);

                foreach (var importedProduct in products)
                {



                    productCount++;
                    if (string.IsNullOrEmpty(importedProduct.ExternalId))
                    {
                        res.ReadProductFailedCount++;
                        continue;
                    }


                    System.Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("Обработка {0} из {1}", productCount, products.Count);
                    System.Console.ForegroundColor = ConsoleColor.White;

                    System.Console.Write("Ищем в базе товар с ExternalId: {0}...", importedProduct.ExternalId);
                    var siteProduct = repository.GetProductByExternalId(importedProduct.ExternalId);
                    System.Console.WriteLine("ОК");

                    if (siteProduct == null)
                    {
                        try
                        {
                            System.Console.Write(" Добавляем новый товар..");
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
                                        Size = importedProductStock.Size,
                                        IsAvailable = importedProductStock.IsAvailable
                                    });

                                }
                            }
                            newProduct.SearchCriteriaAttributes = "";
                            repository.AddProduct(newProduct);
                            justAdded = true;
                            res.NewProductCount++;
                            System.Console.WriteLine("ОК");


                            siteProduct = repository.GetProductByExternalId(importedProduct.ExternalId);
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine(" Не удалось добавить {0}", importedProduct.ExternalId);
                            res.ProductErrors.Add("Не удалось добавить " + importedProduct.ExternalId, ex.Message);
                            res.AddProductFailedCount++;
                        }
                    }


                    //var siteProduct = repository.FindProduct(importedProduct.Id);
                    if (siteProduct != null)
                    {
                        try
                        {
                            System.Console.Write(" Обновление данных товара...");
                            siteProduct.InjectFromImportProduct(importedProduct);
                            System.Console.WriteLine("ОК");
                            if (siteProduct.ProductStocks == null)
                            {
                                siteProduct.ProductStocks = new LinkedList<ProductStock>();
                            }


                            System.Console.Write(" Обновление данных артикулов...");
                            foreach (var productStock in siteProduct.ProductStocks)
                            {
                                var importedProductStock = importedProduct.ImportedProductStocks.FirstOrDefault(ips => ips.StockNumber == productStock.StockNumber && !ips.Imported);
                                if (importedProductStock != null)
                                {
                                    // update productStock in siteProduct.ProductStocks
                                    importedProductStock.Imported = true;
                                    productStock.Size = importedProductStock.Size;
                                    productStock.Color = importedProductStock.Color;
                                    productStock.IsAvailable = importedProductStock.IsAvailable;
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
                                    Size = importedProductStock.Size,
                                    IsAvailable = importedProductStock.IsAvailable
                                });
                                res.AddedArticles++;
                            }
                            System.Console.WriteLine("ОК");


                            System.Console.Write(" Обновление атрибутов...");
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
                            System.Console.WriteLine("ОК");

                            System.Console.Write(" Сохранение товара в базе...");
                            repository.SaveProduct(siteProduct);
                            System.Console.WriteLine("ОК");


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
                            System.Console.ForegroundColor = ConsoleColor.Red;
                            System.Console.WriteLine("Не удалось обновить {0}, {1}", siteProduct.ExternalId, ex.Message);
                            res.ProductErrors.Add("Не удалось обновить " + siteProduct.ExternalId, ex.Message);
                            res.UpdateProductFailedCount++;
                            System.Console.ForegroundColor = ConsoleColor.White;
                        }
                    }


                }


                System.Console.Write(" Удаление несвуществующих артикулов...");
                foreach (var id in productStockToDelete)
                {
                    repository.DeleteProductStock(id);
                }
                System.Console.WriteLine("ОК");


                res.ProductCount = products.Count;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = ex.Message;
                res.ErrorCode = 1;
            }
            finally
            {
                //file.Close();
            }





        }
    }
}
