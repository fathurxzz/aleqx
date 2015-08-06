using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTypeSwitcher.Reports
{
    class PortAttribution :  IReport
    {
        public string ReportType
        {
            get
            {
                return "PORTATTRIBUTION";
            }
        }

        public void Execute(string[] args)
        {
            Console.WriteLine(ReportType);
        }
    }
}
