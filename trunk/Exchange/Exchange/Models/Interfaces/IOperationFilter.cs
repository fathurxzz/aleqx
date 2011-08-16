using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exchange.Models
{
    public interface IOperationFilter
    {
        EOperation Operation { get; set; }
        string OperName { get; set; }
    }
}
