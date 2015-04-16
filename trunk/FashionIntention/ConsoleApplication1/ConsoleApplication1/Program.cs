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
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    class GetHtmlResult
    {
        public string ResultHtml;
        public bool Success;
        public string ErrorMessage;
    }

    class Auto
    {
        public string BaseUrl;
        public string Auto_id;
    }

    class Ria
    {
        public Auto Auto;
    }

    class PhotoData
    {
        public List<PhotoDataItem> photoDataItems;
    }

    class PhotoDataItem
    {
        public string photo_id;
        public string url;
    }




    class Program
    {
        private static double _totalDownloadDataSize;

        private static HtmlDocument GetHtmlDocument(string url)
        {
            GetHtmlResult htmlResult = GetHtml(url);
            if (!htmlResult.Success)
            {
                throw new Exception(htmlResult.ErrorMessage);
            }
            var decodedHtml = System.Web.HttpUtility.HtmlDecode(htmlResult.ResultHtml);
            var doc = new HtmlDocument();
            doc.LoadHtml(decodedHtml);
            return doc;
        }

        private static GetHtmlResult GetHtml(string url)
        {
            var result = new GetHtmlResult();
            try
            {
                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(url);

                IWebProxy proxy = myWebRequest.Proxy;
                if (proxy != null)
                {
                    string proxyuri = proxy.GetProxy(myWebRequest.RequestUri).ToString();
                    myWebRequest.UseDefaultCredentials = true;
                    myWebRequest.Proxy = new WebProxy(proxyuri, false);
                    myWebRequest.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                }

                using (HttpWebResponse response = (HttpWebResponse)myWebRequest.GetResponse())
                {
                    //if (response.StatusCode == HttpStatusCode.OK)
                    //{
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader readStream = null;
                        if (response.CharacterSet == null)
                        {
                            readStream = new StreamReader(responseStream);
                        }
                        else
                        {
                            readStream = new StreamReader(responseStream, Encoding.GetEncoding(response.CharacterSet));
                        }
                        result.ResultHtml = readStream.ReadToEnd();
                        _totalDownloadDataSize += result.ResultHtml.Length;
                        response.Close();
                        readStream.Close();
                        result.Success = true;
                    }
                    //}
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        result.ErrorMessage = "file not found " + url;
                    }
                    else
                    {
                        result.ErrorMessage = ex.Message + "response.StatusCode:" + resp.StatusCode;
                    }
                }
                else
                {
                    result.ErrorMessage = ex.Message + "ex.Status:" + ex.Status;
                }
            }

            return result;
        }

        private static string _baseUrl = "http://auto.ria.com";

        private static Dictionary<string, string> GetRiaEntity(HtmlDocument htmlDocument, bool isCar = false)
        {
            var result = new Dictionary<string, string>();
            HtmlNode node = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='content']");
            HtmlNodeCollection nodes = node.SelectNodes(".//a");
            foreach (var n in nodes)
            {
                if (n.HasAttributes)
                {
                    if (n.Attributes[0] != null && (isCar || !n.Attributes[0].Value.Contains(".html")))
                    {
                        result.Add(_baseUrl + n.Attributes[0].Value, n.InnerText);
                    }
                }
            }
            return result;
        }

        private static Dictionary<string, string> GetBrands(HtmlDocument htmlDocument)
        {
            return GetRiaEntity(htmlDocument);
        }

        private static Dictionary<string, string> GetModels(HtmlDocument htmlDocument)
        {
            return GetRiaEntity(htmlDocument);
        }

        private static Dictionary<string, string> GetCars(HtmlDocument htmlDocument)
        {
            return GetRiaEntity(htmlDocument, true);
        }


        private static void ReadCarAttributes(ref Car realCar, IEnumerable<HtmlNode> nodes, bool swapKeyValues = false)
        {
            foreach (var n in nodes)
            {
                var s = n.SelectSingleNode(".//strong");
                var keyValue = trimmer.Replace(n.InnerText.Replace("\n", ""), " ").Replace("Указать","").Trim();
                if (s != null)
                {
                    var value = trimmer.Replace(s.InnerText.Replace("\n", ""), " ").Trim();
                    var key = !string.IsNullOrEmpty(value) ? keyValue.Replace(value, "") : keyValue;
                    key = key.Trim();
                    if (string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                    {
                        realCar.attributes.Add(value, value);
                    }
                    else if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                    {
                        if (swapKeyValues)
                        {
                            realCar.attributes.Add(value, key);
                        }
                        else
                        {
                            realCar.attributes.Add(key, value);
                        }
                    }
                }
                else
                {
                    realCar.attributes.Add(keyValue, keyValue);
                }
            }
        }


        static readonly Regex trimmer = new Regex(@"\s\s+");



        //private static readonly string[] _skipBrands = { "Aixam", "Alfa Romeo", "Asia", "Aston Martin", "Audi", "Austin", "Bentley", "BMW", "Cadillac", "Chevrolet", "Chrysler" };
        private static readonly string[] _skipBrands = { };

        static void Main(string[] args)
        {
            HtmlNodeCollection nodes;

            var errorCnt = 0;


            try
            {



                var brandsUrl = "http://auto.ria.com/map/bu/";

                HtmlDocument doc = GetHtmlDocument(brandsUrl);
                var brands = GetBrands(doc);


                foreach (var brand in brands.Where(b => !_skipBrands.Contains(b.Value)))
                {
                    List<Car> carsByBrand = new List<Car>();

                    doc = GetHtmlDocument(brand.Key);
                    Dictionary<string, string> models = GetModels(doc);

                    var modelcount = 0;
                    foreach (var model in models)
                    {
                        List<Car> carsByBrandAndModel = new List<Car>();
                        modelcount++;
                        doc = GetHtmlDocument(model.Key);
                        Dictionary<string, string> cars = GetCars(doc);

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

                                doc = GetHtmlDocument(car.Key);
                                HtmlNode node = doc.DocumentNode.SelectSingleNode("//h1[@class='head-cars']");

                                // year
                                if (node != null && node.InnerHtml != null)
                                {
                                    var year = node.SelectSingleNode("//span[@class='year']");
                                    realCar.year = year.InnerText;
                                }

                                if (node != null && node.InnerText != null)
                                {
                                    realCar.modelFull = trimmer.Replace(node.InnerText.Replace("\n", ""), " ").Trim();
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
                                    realCar.priceAtRate = trimmer.Replace(node.InnerText.Replace("\n", ""), " ").Trim();
                                }


                                // read attributes
                                node = doc.DocumentNode.SelectSingleNode("//div[@class='characteristic delimeter']");
                                if (node != null)
                                {
                                    nodes = node.SelectNodes(".//p[@class='item-param']");
                                    if (nodes != null)
                                    {
                                        ReadCarAttributes(ref realCar, nodes);
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
                                            ReadCarAttributes(ref realCar, nodes);
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
                                        ReadCarAttributes(ref realCar, nodes, true);
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
                                                    var value = trimmer.Replace(s.InnerText.Replace("\n", ""), " ").Trim();
                                                    realCar.carId = value;
                                                }
                                            }
                                            else if (n.InnerHtml.Contains("final_page__add_date"))
                                            {
                                                var s = n.SelectSingleNode(".//strong");
                                                if (s != null)
                                                {
                                                    var value = trimmer.Replace(s.InnerText.Replace("\n", ""), " ").Trim();
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

                                    doc = GetHtmlDocument(photolink);

                                    node = doc.DocumentNode.SelectSingleNode("//script[@type='text/javascript']");
                                    //nodes = doc.DocumentNode.SelectNodes("//a");

                                    if (node != null && node.InnerHtml != null && node.InnerHtml.Contains("window.Ria.Auto.AutoId"))
                                    {

                                        var xxx = node.InnerHtml.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                                        var key1 = "window.Ria =";
                                        var value1 = xxx[0].Substring(xxx[0].IndexOf(key1, StringComparison.OrdinalIgnoreCase) + key1.Length).Trim();
                                        value1 = trimmer.Replace(value1.Replace("\n", ""), " ").Trim();
                                        var obj1 = JsonConvert.DeserializeObject<Ria>(value1);


                                        var key2 = "window.Ria.Auto.AutoId = ";
                                        var value2 = xxx[1].Substring(xxx[1].IndexOf(key2, StringComparison.OrdinalIgnoreCase) + key2.Length).Trim();
                                        value2 = trimmer.Replace(value2.Replace("\n", ""), " ").Trim();

                                        var key3 = "window.Ria.Auto.PhotoData =";
                                        var value3 = xxx[3].Substring(xxx[3].IndexOf(key3, StringComparison.OrdinalIgnoreCase) + key3.Length).Trim();
                                        value3 = trimmer.Replace(value3.Replace("\n", ""), " ").Trim();
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
                                Console.WriteLine("--- attributes ---");
                                foreach (var attribute in realCar.attributes)
                                {
                                    Console.WriteLine(attribute.Key + " " + attribute.Value);
                                }
                                Console.WriteLine("------------------");
                                Console.WriteLine("");
                                Console.WriteLine("--- description ---");
                                Console.WriteLine(realCar.description);
                                Console.WriteLine("-------------------");
                                Console.WriteLine("");
                                Console.WriteLine("--- images ---");
                                foreach (var url in realCar.imagesUrl)
                                {
                                    Console.WriteLine(url);
                                }
                                Console.WriteLine("--------------");

                                Console.WriteLine("");
                                Console.WriteLine("");


                            }
                        }
                        catch (Exception ex)
                        {
                            errorCnt++;
                            Console.WriteLine(ex.Message);
                        }


                        // save cars by model 
                        SaveCarsToFile(carsByBrand, brand.Value, model.Value);
                    }

                    SaveCarsToFile(carsByBrand, brand.Value);
                }

            }
            catch (Exception ex)
            {
                Console.Write("ERROR! " + ex.Message);
            }
        }

        private static void SaveCarsToFile(IEnumerable<Car> cars, string brandName, string modelName = null)
        {
            if (modelName == null)
            {
                modelName = string.Empty;
            }
            else
            {
                modelName = "_" + modelName;
            }

            var carsToJson = new List<object>();
            foreach (var c in cars)
            {
                var attrToJson = new List<object>();
                foreach (var attribute in c.attributes)
                {
                    attrToJson.Add(new { attr = attribute.Key.Trim() + attribute.Value.Trim() });
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

            System.IO.File.WriteAllText(brandName + modelName + ".json", json);
        }
    }
}
