using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Excel;

namespace ImportFromExcel
{
    class Program
    {
        private const double C1 = 0.232978;
        private const double C2 = 0.05113;
        private const double C3 = 0.0007;
        private const double C4 = 0.11997;




        static IEnumerable<double[]> GetValuesFromFile(string fileName, int columns)
        {
            columns = columns + 3;
            FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read);
            string extension = Path.GetExtension(fileName);
            IExcelDataReader excelReader = null;
            if (extension == ".xls")
                excelReader = Factory.CreateReader(stream, ExcelFileType.Binary);
            if (extension == ".xlsx")
                excelReader = Factory.CreateReader(stream, ExcelFileType.OpenXml);

            if (excelReader == null)
                return null;

            var result = new List<double[]>();
            int cnt = 0;
            while (excelReader.Read())
            {
                cnt++;
                var x = new double[columns];
                for (int i = 0; i < columns; i++)
                {
                    if (i == columns - 1)
                    {
                        x[i] = 0;
                    }
                    else if (i == columns - 2)
                    {
                        x[i] = cnt;
                    }
                    else if (i == columns - 3)
                    {
                        x[i] = 0;
                    }
                    else
                    {
                        string rowValue = excelReader.GetString(i);
                        if (!string.IsNullOrEmpty(rowValue))
                            rowValue = rowValue.Replace(".", ",");
                        x[i] = Convert.ToDouble(rowValue);
                    }
                }
                result.Add(x);
            }

            excelReader.Close();
            stream.Close();
            return result;
        }

        static void Main(string[] args)
        {
            string fileName = args.Length == 0 ? "book1.xls" : args[0];

            var iList = GetValuesFromFile(fileName, 3);


            Console.Write("enter x1(min) x1(max)");
            string[] s = Console.ReadLine().Split(new[] { " " }, StringSplitOptions.None);
            double x1Min = Convert.ToDouble(s[0]);
            double x1Max = Convert.ToDouble(s[1]);

            Console.Write("enter x2(min) x2(max)");
            s = Console.ReadLine().Split(new[] { " " }, StringSplitOptions.None);
            double x2Min = Convert.ToDouble(s[0]);
            double x2Max = Convert.ToDouble(s[1]);

            Console.Write("enter x3(min) x3(max)");
            s = Console.ReadLine().Split(new[] { " " }, StringSplitOptions.None);
            double x3Min = Convert.ToDouble(s[0]);
            double x3Max = Convert.ToDouble(s[1]);







            List<double[]> result = iList.Where(x => x[0] >= x1Min && x[0] <= x1Max && x[1] >= x2Min && x[1] <= x2Max && x[2] >= x3Min && x[2] <= x3Max).ToList();




            foreach (double[] x in result)
            {
                x[3] = C1 * x[0] + C2 * x[1] + C3 * x[2] + C4;
            }


            Console.WriteLine("total entries that match the condition: {0}", result.Count);
            

            result = result.OrderByDescending(x => x[3]).ToList();

            foreach (double[] x in result)
            {
                Console.WriteLine("position: {4}\t x1: {0}\t x2: {1}\t x3: {2}\t y: {3}", x[0], x[1], x[2], x[3], x[4]);
            }


            Console.ReadKey();
            Console.WriteLine("calculating result provided: x1(min),x2(min),x3(min)");

            foreach (double[] x in result)
            {
                x[5] = x[0] + x[1] + x[2];
            }

            result = result.OrderBy(x => x[5]).ToList();
            foreach (double[] x in result)
            {
                Console.WriteLine("position: {4}\t x1: {0}\t x2: {1}\t x3: {2}\t y: {3}\t x1x2x3: {5}", x[0], x[1], x[2], x[3], x[4], x[5]);
            }

            Console.ReadKey();
            Console.WriteLine("calculating result provided: x1(min),x2(min)");

            foreach (double[] x in result)
            {
                x[5] = x[0] + x[1];
            }

            result = result.OrderBy(x => x[5]).ToList();
            foreach (double[] x in result)
            {
                Console.WriteLine("position: {4}\t x1: {0}\t x2: {1}\t x3: {2}\t y: {3}\t x1x2: {5}", x[0], x[1], x[2], x[3], x[4], x[5]);
            }

            Console.ReadKey();
            Console.WriteLine("calculating result provided: x1(min),x3(min)");

            foreach (double[] x in result)
            {
                x[5] = x[0] + x[2];
            }

            result = result.OrderBy(x => x[5]).ToList();
            foreach (double[] x in result)
            {
                Console.WriteLine("position: {4}\t x1: {0}\t x2: {1}\t x3: {2}\t y: {3}\t x1x3: {5}", x[0], x[1], x[2], x[3], x[4], x[5]);
            }


            Console.ReadKey();
            Console.WriteLine("calculating result provided: x1(min)");

            result = result.OrderBy(x => x[0]).ToList();
            foreach (double[] x in result)
            {
                Console.WriteLine("position: {4}\t x1: {0}\t x2: {1}\t x3: {2}\t y: {3}", x[0], x[1], x[2], x[3], x[4]);
            }


            Console.ReadKey();
            Console.WriteLine("calculating result provided: x2(min)");

            result = result.OrderBy(x => x[1]).ToList();
            foreach (double[] x in result)
            {
                Console.WriteLine("position: {4}\t x1: {0}\t x2: {1}\t x3: {2}\t y: {3}", x[0], x[1], x[2], x[3], x[4]);
            }

            Console.ReadKey();
            Console.WriteLine("calculating result provided: x3(min)");

            result = result.OrderBy(x => x[2]).ToList();
            foreach (double[] x in result)
            {
                Console.WriteLine("position: {4}\t x1: {0}\t x2: {1}\t x3: {2}\t y: {3}", x[0], x[1], x[2], x[3], x[4]);
            }
            Console.ReadKey();

            /*
            foreach (double[] x in iList)
            {
                Console.WriteLine("{3} x1: {0}\t x2: {1}\t x3: {2}", x[0], x[1], x[2], cnt);
                
                cnt++;
                var y = C1 * x[0] + C2 * x[1] + C3 * x[2] + C4;
                if (y > yMax)
                {
                    yMax = y;
                    pos = cnt;
                }
            }
            */






            //Console.WriteLine("y(max)={0} pos={1}", yMax, pos);
        }
    }
}
