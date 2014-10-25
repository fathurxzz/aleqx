using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.DataAccess.Entities;

namespace Shop.Api.DataSynchronization.Import
{
    public class ImportFromFile
    {
        public static ImportResult Execute(StreamReader file)
        {
            var res = new ImportResult { ErrorCode = 0 };
            var products = new List<Product>();
            int counter = 0;
            try
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {

                    counter++;
                    string[] x = line.Split(new[] { ";" }, StringSplitOptions.None);

                    if (counter > 2)
                    {
                        var product = new Product
                        {
                            ExternalId = x[0],
                            Id = int.Parse(x[1]),
                            Name = x[2]
                        };

                        products.Add(product);
                    }
                }

                res.ErrorMessage = string.Format("Файл успешно загружен. Получено {0} товаров", counter);
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
