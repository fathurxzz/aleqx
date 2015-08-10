using ReportTypeSwitcher.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTypeSwitcher
{
    class ReportFactory
    {
        public static IEnumerable<IReport> Reports
        {
            get
            {
                return new[] 
                { 
                    (IReport)new PortAttribution(), 
                    new BondAttribution(),
                    new Pnl(),
                    new AssetMix(),
                    new AssetMixDetail(),
                    new AssetMixHistory(),
                    new Carry(),
                    new CoreAnalytics(),
                    new DailyPnl(),
                };
            }
        }
    }
}
