using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using ConsoleApplication1.Helpers;
using ConsoleApplication1.Model;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    class Program
    {
        private static Regex _trimmer = new Regex(@"\s\s+");

        //private static readonly string[] _skipBrands = { "Aixam", "Alfa Romeo", "Asia", "Aston Martin", "Audi", "Austin", "Bentley", "BMW", "Cadillac", "Chevrolet", "Chrysler" };
        private static readonly string[] _skipBrands = { };
        private static List<string> skipBrands = new List<string>();

        private static double _totalDownloadDataSize;



        private static void GetCarsMap()
        {
            var map = new CarsMap
            {
                Brands = new List<Brand>()
            };

            //string selectedBrand = "Honda";
            //string selectedModel = "Honda Accord";


            var brandsUrl = "http://auto.ria.com/map/bu/";
            HtmlDocument doc = HtmlHelper.GetHtmlDocument(brandsUrl, ref _totalDownloadDataSize);
            Console.WriteLine("get all brands");
            var brandsPages = HtmlHelper.GetPages(doc);
            Console.WriteLine("total pages:" + brandsPages.Count);
            if (!brandsPages.Any())
            {
                var brandGroup = HtmlHelper.GetBrands(doc);
                foreach (var kvp in brandGroup)
                {
                    Console.WriteLine("brandGroup {0} of {1}", kvp.Value, brandGroup.Count);
                    map.Brands.Add(new Brand
                    {
                        Url = kvp.Key,
                        Title = kvp.Value,
                        Models = new List<Model.Model>()
                    });
                }
            }
            else
            {
                foreach (var page in brandsPages)
                {
                    Console.WriteLine("brandPage {0} of {1}", page, brandsPages.Count);
                    HtmlDocument d = HtmlHelper.GetHtmlDocument(page, ref _totalDownloadDataSize);
                    var brandGroup = HtmlHelper.GetBrands(d);
                    foreach (var kvp in brandGroup)
                    {
                        Console.WriteLine("brandPage {0} of {1} brandGroup {2} of {3}", page, brandsPages.Count, kvp.Value, brandGroup.Count);
                        map.Brands.Add(new Brand
                        {
                            Url = kvp.Key,
                            Title = kvp.Value,
                            Models = new List<Model.Model>()
                        });
                    }
                }
            }


            foreach (var brand in map.Brands
                //.Where(b => b.Title == selectedBrand)
                )
            {
                
                HtmlDocument brandDocument = HtmlHelper.GetHtmlDocument(brand.Url, ref _totalDownloadDataSize);
                Console.WriteLine("brand:" + brand.Title + " total_size:" + _totalDownloadDataSize);
                var modelsPages = HtmlHelper.GetPages(brandDocument);
                if (!modelsPages.Any())
                {
                    var modelGroup = HtmlHelper.GetBrands(brandDocument);
                    foreach (var kvp in modelGroup)
                    {
                        brand.Models.Add(new Model.Model
                        {
                            Url = kvp.Key,
                            Title = kvp.Value,
                            Cars = new List<Model.Car>()
                        });
                    }
                }
                else
                {
                    foreach (var page in modelsPages)
                    {
                        HtmlDocument modelDocument = HtmlHelper.GetHtmlDocument(page, ref _totalDownloadDataSize);
                        Console.WriteLine("brand:" + brand.Title + " page:"+page +" total_size:" + _totalDownloadDataSize);
                        var modelGroup = HtmlHelper.GetModels(modelDocument);
                        foreach (var kvp in modelGroup)
                        {
                            brand.Models.Add(new Model.Model
                            {
                                Url = kvp.Key,
                                Title = kvp.Value,
                                Cars = new List<Model.Car>()
                            });
                        }
                    }
                }
            }


            foreach (var brand in map.Brands
                //.Where(b => b.Title == selectedBrand)
                )
            {
                foreach (var model in brand.Models //.Where(m => m.Title == selectedModel)
                    )
                {
                    Console.WriteLine("brand:" + brand.Title+" model:" + model.Title + " url:" + model.Url + " total_size:" + _totalDownloadDataSize);
                    HtmlDocument modelDocument = HtmlHelper.GetHtmlDocument(model.Url, ref _totalDownloadDataSize);
                    var carsPages = HtmlHelper.GetPages(modelDocument);
                    if (!carsPages.Any())
                    {
                        var carGroup = HtmlHelper.GetCars(modelDocument);
                        foreach (var kvp in carGroup)
                        {
                            model.Cars.Add(new Model.Car
                            {
                                Url = kvp.Key,
                                Title = kvp.Value
                            });
                        }
                    }
                    else
                    {
                        foreach (var page in carsPages)
                        {
                            HtmlDocument carDocument = HtmlHelper.GetHtmlDocument(page, ref _totalDownloadDataSize);
                            var carGroup = HtmlHelper.GetCars(carDocument);
                            foreach (var kvp in carGroup)
                            {
                                model.Cars.Add(new Model.Car
                                {
                                    Url = kvp.Key,
                                    Title = kvp.Value
                                });
                            }
                        }
                    }

                }
            }


            var cnt = map.Brands              //.Where(b => b.Title == "Honda")
                .SelectMany(m => m.Models)    //.Where(m => m.Title == "Honda Accord")
                .SelectMany(c => c.Cars)
                .Count();



            Console.WriteLine("total cars:" + cnt);
            Console.ReadKey();





            var carsToJson = new List<object>();

            foreach (var brand in map.Brands)
            {
                foreach (var model in brand.Models)
                {
                    foreach (var c in model.Cars)
                    {
                        carsToJson.Add(new
                        {
                            brand = brand.Title,
                            model = model.Title,
                            modelFull = c.Title,
                            url = c.Url
                        });
                    }
                }
            }

            string json = JsonConvert.SerializeObject(carsToJson.ToArray());
            System.IO.File.WriteAllText("cars_map.json", json);
        }


        static void Main(string[] args)
        {

            GetCarsMap();

            return;


            HtmlNodeCollection nodes;
            var errorCnt = 0;
            //var loadOnlyThatBrands = args;
            //string[] loadOnlyThatBrands = {"Honda"};

            args = new[] { "Honda" };

            try
            {
                var downloadedBrandsJson = File.ReadAllText("downloadedbrands.json");
                if (!string.IsNullOrEmpty(downloadedBrandsJson))
                {
                    skipBrands = JsonConvert.DeserializeObject<List<string>>(downloadedBrandsJson);
                }





                var brandsUrl = "http://auto.ria.com/map/bu/";
                HtmlDocument doc = HtmlHelper.GetHtmlDocument(brandsUrl, ref _totalDownloadDataSize);
                var brandsPages = HtmlHelper.GetPages(doc);















                var brands = HtmlHelper.GetBrands(doc);

                foreach (var brand in brands.Where(b => !skipBrands.Contains(b.Value) && (args.Length == 0 || args.Contains(b.Value))))
                {
                    List<Car> carsByBrand = new List<Car>();

                    doc = HtmlHelper.GetHtmlDocument(brand.Key, ref _totalDownloadDataSize);
                    var modelsPages = HtmlHelper.GetPages(doc);


                    Dictionary<string, string> models = HtmlHelper.GetModels(doc);

                    var modelcount = 0;
                    foreach (var model in models)
                    {
                        List<Car> carsByBrandAndModel = new List<Car>();
                        modelcount++;
                        doc = HtmlHelper.GetHtmlDocument(model.Key, ref _totalDownloadDataSize);
                        Dictionary<string, string> cars = HtmlHelper.GetCars(doc);

                        var carcount = 0;
                        try
                        {

                            foreach (var car in cars)
                            {
                                carcount++;
                                var realCar = new Car
                                {
                                    attributes = new Dictionary<string, string>(),
                                    imagesUrl = new List<string>(),
                                    url = car.Key,
                                    brand = brand.Value,
                                    model = model.Value
                                };

                                doc = HtmlHelper.GetHtmlDocument(car.Key, ref _totalDownloadDataSize);
                                HtmlNode node = doc.DocumentNode.SelectSingleNode("//h1[@class='head-cars']");

                                // year
                                if (node != null && node.InnerHtml != null)
                                {
                                    var year = node.SelectSingleNode("//span[@class='year']");
                                    realCar.year = year.InnerText;
                                }

                                if (node != null && node.InnerText != null)
                                {
                                    realCar.modelFull = _trimmer.Replace(node.InnerText.Replace("\n", ""), " ").Trim();
                                }

                                // price
                                node = doc.DocumentNode.SelectSingleNode("//div[@class='price-seller']");
                                if (node != null && node.InnerHtml != null)
                                {
                                    var price = node.SelectSingleNode("//span[@class='price']");
                                    realCar.priceSeller = price.InnerText;
                                }

                                // price-at-rate
                                node = doc.DocumentNode.SelectSingleNode("//div[@class='price-at-rate']");
                                if (node != null && node.InnerText != null)
                                {
                                    realCar.priceAtRate = _trimmer.Replace(node.InnerText.Replace("\n", ""), " ").Trim();
                                }


                                // read attributes
                                node = doc.DocumentNode.SelectSingleNode("//div[@class='characteristic delimeter']");
                                if (node != null)
                                {
                                    nodes = node.SelectNodes(".//p[@class='item-param']");
                                    if (nodes != null)
                                    {
                                        HtmlHelper.ReadCarAttributes(ref realCar, nodes);
                                    }
                                }

                                // read attributes
                                node = doc.DocumentNode.SelectSingleNode("//div[@class='box-panel rocon']");
                                if (node != null)
                                {
                                    node = node.SelectSingleNode(".//dl[@class='unordered-list']");
                                    if (node != null)
                                    {
                                        nodes = node.SelectNodes(".//dd");
                                        if (nodes != null)
                                        {
                                            HtmlHelper.ReadCarAttributes(ref realCar, nodes);
                                        }
                                    }
                                }

                                // read attributes
                                node = doc.DocumentNode.SelectSingleNode("//div[@class='box-panel rocon']");
                                if (node != null)
                                {
                                    nodes = node.SelectNodes(".//p[@class='additional-data']");
                                    if (nodes != null)
                                    {
                                        HtmlHelper.ReadCarAttributes(ref realCar, nodes, true);
                                    }
                                }


                                // read description
                                node = doc.DocumentNode.SelectSingleNode("//div[@class='box-panel rocon']");
                                if (node != null)
                                {
                                    node = node.SelectSingleNode(".//p[@id='description']");
                                    if (node != null && node.InnerHtml != null)
                                    {
                                        realCar.description = node.InnerHtml;
                                    }
                                }

                                // read car id and date added
                                nodes = doc.DocumentNode.SelectNodes("//p[@class='item-param']");
                                if (nodes != null)
                                {
                                    foreach (var n in nodes)
                                    {
                                        if (n.InnerHtml != null)
                                        {
                                            if (n.InnerHtml.Contains("icon-id-item"))
                                            {
                                                var s = n.SelectSingleNode(".//strong");
                                                if (s != null)
                                                {
                                                    var value = _trimmer.Replace(s.InnerText.Replace("\n", ""), " ").Trim();
                                                    realCar.carId = value;
                                                }
                                            }
                                            else if (n.InnerHtml.Contains("final_page__add_date"))
                                            {
                                                var s = n.SelectSingleNode(".//strong");
                                                if (s != null)
                                                {
                                                    var value = _trimmer.Replace(s.InnerText.Replace("\n", ""), " ").Trim();
                                                    realCar.dateAdded = value;
                                                }
                                            }
                                        }
                                    }
                                }

                                // read photos
                                if (realCar.carId != null)
                                {
                                    var photolink = "http://auto.ria.com/old/auto_photo/megaphoto/" + realCar.carId + "/";

                                    doc = HtmlHelper.GetHtmlDocument(photolink, ref _totalDownloadDataSize);

                                    node = doc.DocumentNode.SelectSingleNode("//script[@type='text/javascript']");
                                    //nodes = doc.DocumentNode.SelectNodes("//a");

                                    if (node != null && node.InnerHtml != null && node.InnerHtml.Contains("window.Ria.Auto.AutoId"))
                                    {

                                        var xxx = node.InnerHtml.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                                        var key1 = "window.Ria =";
                                        var value1 = xxx[0].Substring(xxx[0].IndexOf(key1, StringComparison.OrdinalIgnoreCase) + key1.Length).Trim();
                                        value1 = _trimmer.Replace(value1.Replace("\n", ""), " ").Trim();
                                        var obj1 = JsonConvert.DeserializeObject<Ria>(value1);


                                        var key2 = "window.Ria.Auto.AutoId = ";
                                        var value2 = xxx[1].Substring(xxx[1].IndexOf(key2, StringComparison.OrdinalIgnoreCase) + key2.Length).Trim();
                                        value2 = _trimmer.Replace(value2.Replace("\n", ""), " ").Trim();

                                        var key3 = "window.Ria.Auto.PhotoData =";
                                        var value3 = xxx[3].Substring(xxx[3].IndexOf(key3, StringComparison.OrdinalIgnoreCase) + key3.Length).Trim();
                                        value3 = _trimmer.Replace(value3.Replace("\n", ""), " ").Trim();
                                        var obj3 = JsonConvert.DeserializeObject<PhotoData>("{photoDataItems:" + value3 + "}");


                                        foreach (var item in obj3.photoDataItems)
                                        {
                                            if (item.url.EndsWith(".jpg"))
                                            {
                                                item.url = item.url.Replace(".jpg", "fx.jpg");
                                            }
                                            if (item.url.EndsWith(".png"))
                                            {
                                                item.url = item.url.Replace(".jpg", "fx.png");
                                            }
                                            if (item.url.EndsWith(".jpeg"))
                                            {
                                                item.url = item.url.Replace(".jpeg", "fx.jpeg");
                                            }
                                            if (item.url.EndsWith(".gif"))
                                            {
                                                item.url = item.url.Replace(".gif", "fx.gif");
                                            }

                                            realCar.imagesUrl.Add("https://cdn.riastatic.com/photos/" + item.url);
                                        }
                                    }
                                }

                                carsByBrand.Add(realCar);
                                carsByBrandAndModel.Add(realCar);

                                Console.Clear();
                                Console.WriteLine("brand:{0} model:{1}  model {4} of {5}  car {2} of {3} errors:{6} downloadSize:{7}MB, {8}GB", brand.Value, model.Value, carcount, cars.Count, modelcount, models.Count, errorCnt, Math.Round(_totalDownloadDataSize / 1024 / 1024, 1), Math.Round(_totalDownloadDataSize / 1024 / 1024 / 1024, 3));
                                Console.WriteLine(realCar);
                                Console.WriteLine("");

                                //Console.WriteLine("--- attributes ---");
                                //foreach (var attribute in realCar.attributes)
                                //{
                                //    Console.WriteLine(attribute.Key + " " + attribute.Value);
                                //}
                                //Console.WriteLine("------------------");
                                //Console.WriteLine("");
                                Console.WriteLine("--- description ---");
                                Console.WriteLine(realCar.description);
                                Console.WriteLine("-------------------");
                                //Console.WriteLine("");
                                Console.WriteLine("images:{0}", realCar.imagesUrl.Count);
                                //Console.WriteLine("--- images ---");
                                //foreach (var url in realCar.imagesUrl)
                                //{
                                //    Console.WriteLine(url);
                                //}
                                //Console.WriteLine("--------------");

                                //Console.WriteLine("");
                                //Console.WriteLine("");


                            }
                        }
                        catch (Exception ex)
                        {
                            errorCnt++;
                            Console.WriteLine(ex.Message);
                        }


                        // save cars by model 
                        SaveCarsToFile(carsByBrandAndModel, brand.Value, model.Value);
                    }

                    SaveCarsToFile(carsByBrand, brand.Value);
                    skipBrands.Add(brand.Value);

                    string json = JsonConvert.SerializeObject(skipBrands.ToArray());
                    System.IO.File.WriteAllText("downloadedbrands.json", json);
                }

            }
            catch (Exception ex)
            {
                Console.Write("ERROR! " + ex.Message);
            }
        }

        private static void SaveCarsToFile(IEnumerable<Car> cars, string brandName, string modelName = null)
        {
            string path = "";
            if (modelName == null)
            {
                modelName = string.Empty;
                path = "brands\\";
            }
            else
            {
                modelName = "_" + modelName;
                path = "brands_models\\";
            }

            //object car = new {color = "red"};

            var carsToJson = new List<object>();

            foreach (var c in cars)
            {
                var attrToJson = new List<object>();

                foreach (var attribute in c.attributes)
                {
                    attrToJson.Add(new { key = attribute.Key, value = attribute.Value });
                }

                carsToJson.Add(new
                {
                    brand = c.brand,
                    model = c.model,
                    modelFull = c.modelFull,
                    year = c.year,
                    priceSeller = c.priceSeller,
                    priceAtRate = c.priceAtRate,
                    carId = c.carId,
                    dateAdded = c.dateAdded,
                    url = c.url,
                    description = c.description,
                    attributes = attrToJson,
                    imagesUrl = c.imagesUrl
                });
            }

            string json = JsonConvert.SerializeObject(carsToJson.ToArray());

            System.IO.File.WriteAllText(path + brandName + modelName + ".json", json);



        }
    }


    //class MyConverter : JsonConverter
    //{
    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        List<KeyValuePair<string, object>> list = value as List<KeyValuePair<string, object>>;
    //        writer.WriteStartArray();
    //        foreach (var item in list)
    //        {
    //            writer.WriteStartObject();
    //            writer.WritePropertyName(item.Key);
    //            writer.WriteValue(item.Value);
    //            writer.WriteEndObject();
    //        }
    //        writer.WriteEndArray();
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    //public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    //{
    //    //    // TODO...
    //    //}

    //    public override bool CanConvert(Type objectType)
    //    {
    //        return objectType == typeof(List<KeyValuePair<string, object>>);
    //    }
    //}
}
