using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTypeSwitcher.Reports
{
    class Pnl :  IReport
    {

        public string ReportType
        {
            get { return "PNL"; }
        }

        public void Execute(string[] args)
        {
            Console.WriteLine(ReportType);
        }

    }
}
