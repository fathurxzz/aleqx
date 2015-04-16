using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Car
    {
        public string brand;
        public string year;
        public string model;
        public string modelFull;
        public string priceSeller;
        public string priceAtRate;
        public Dictionary<string, string> attributes;
        public string carId;
        public string dateAdded;
        public string url;
        public List<string> imagesUrl;

        public string description;

        public override string ToString()
        {
            return String.Format("year:{0}\r\nmodel:{1}\r\npriceSeller:{2}\r\npriceAtRate:{3}", year, model, priceSeller, priceAtRate);
        }
    }
}
