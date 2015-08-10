using ReportTypeSwitcher.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReportTypeSwitcher
{
    class Program
    {
        static void Main(string[] args)
        {
            //var myArgs = new[] {"PNL", "1", "myfolio", "8-1-2015", "12-1-2015", "ad4ebcda-b21b-4f99-a64e-4bcf5824a553"};
            //var myArgs = new[] { "PNL", "1", "myfolio"};
            //var myArgs = new[] { "ASSETMIX", "1", "myfolio"};
            //var myArgs = new[] { "ASSETMIXDETAIL", "1", "myfolio","w" };
            //var myArgs = new[] { "ASSETMIXHISTORY", "1", "myfolio" };
            //var myArgs = new[] { "CARRY", "1", "myfolio", "8-1-2015" };
            //var myArgs = new[] { "COREANALYTICS", "1", "1", "8-1-2015" };
            var myArgs = new[] { "DAILYPNL", "1", "myfolio1;myfolio2;myfolio3;", "8-1-2015" };

            Run(myArgs);
        }


        static void Run(string[] args)
        {
            try
            {
                //ReportArgs reportArgs = ArgsParser.Parse(args);
                var reports = ReportFactory.Reports;
                var report = GetReport(reports, args[0]);
                report.Execute(args.Skip(1).ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        private static IReport GetReport(IEnumerable<IReport> reports, string reportType)
        {
            try
            {
                return reports.Single(x => x.ReportType == reportType.ToUpper());
            }
            catch (Exception ex)
            {
                throw new Exception("cannot get " + reportType + " report type\r\n" + ex.Message);
            }
            
        }
    }
}
