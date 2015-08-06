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
            try
            {
                //ReportArgs reportArgs = ArgsParser.Parse(args);
                var reports = ReportFactory.Reports;
                var report = reports.Single(x => x.ReportType == args[0].ToUpper());
                report.Execute(args.Skip(1).ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
