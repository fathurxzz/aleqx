using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    public class TotalVolumeInfoEntity
    {
        public int CurId { get; set; }
        public decimal Amount { get; set; }
        public decimal AverageCourse{ get; set; }
    }
}