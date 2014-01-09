using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexiusFusion.DataAccess.Entities
{
    public class PerDiem
    {
        public PerDiem()
        {
            ReportEmployees = new List<ReportEmployee>();
        }

        public int PerDiemId { get; set; }
        public int Value { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<ReportEmployee> ReportEmployees { get; set; }
    }
}
