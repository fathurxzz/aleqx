using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccuV.DataAccess.Entities
{
    public class Module
    {
        public Module()
        {
            Activities = new List<Activity>();
        }

        public int ModuleId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }
}
