using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exchange.Models
{
    public interface ICurrencyFilter
    {
        int CurId { get; set; }
        string CurDef { get; set; }
        string CurName { get; set; }
    }
}
