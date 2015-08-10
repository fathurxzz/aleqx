﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportTypeSwitcher.Parsers;

namespace ReportTypeSwitcher.Reports
{
    class Pnl :  IReport
    {
        readonly ArgsParserSequence _sequence = new ArgsParserSequence();
        public Pnl()
        {
            _sequence.AddParser(ReportArguments.Overwrite, new BoolParser());
            _sequence.AddParser(ReportArguments.Portfolio, new StringParser());
            _sequence.AddParser(ReportArguments.Token, new GuidParser());
            _sequence.AddParser(ReportArguments.DateRange, new BondAttributionDateRangeParser(new DateTimeParser()));
        }

        public string ReportType
        {
            get { return "PNL"; }
        }
        

        public void Execute(string[] args)
        {
            Console.WriteLine(ReportType + "|" + new ReportArgs(_sequence.ParseArgs(args)));
        }

    }
}
