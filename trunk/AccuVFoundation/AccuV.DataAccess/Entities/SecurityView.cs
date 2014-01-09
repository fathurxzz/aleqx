using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccuV.DataAccess.Entities
{
    public partial class SecurityView
    {
        public string ProjectId {get;set;}
        public int ModuleId { get; set; }
        public string ModuleDescription{ get; set; }
        public string UsrId{ get; set; }
        public string UsrDesc { get; set; }
        public int TypeActivityId { get; set; }
        public string TypeActivity { get; set; }
        public int ActivityId { get; set; }
        public string ActivityDescription { get; set; }
        public string SiteNumber { get; set; }
        public string TaskId { get; set; }
        public int AccessCreate { get; set; }
        public int AccessRead { get; set; }
        public int AccessUpdate { get; set; }
        public int AccessDelete { get; set; }
        public int AccessApprove { get; set; }
        public int AccessAdmin { get; set; }

    }
}
