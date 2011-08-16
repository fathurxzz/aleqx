using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exchange.Models
{
    public interface IOperSignFilter
    {
        EOperSign OperSign { get; set; }
        string OperSignName { get; set; }
    }
}
