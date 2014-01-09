using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccuV.DataAccess.Contracts
{
    public class CloseoutpackageViewRequest
    {
        public string ProjectId { get; set; }
        public string[] MarketManager { get; set; }
        public string[] ConstructionManager { get; set; }
        public string[] Region { get; set; }
        public string[] Market { get; set; }
        public string[] Site { get; set; }
        public string[] Task { get; set; }
        public string GeneralContractor { get; set; }
        public int Page { get; set; }
        public string[] DocumentId { get; set; }
        public string[] DocumentType { get; set; }
        public int? NotApplicable { get; set; }
        public int? IsAdmin { get; set; }
    }
}
