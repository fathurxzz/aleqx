using System.Collections.Generic;

namespace AccuVAPI.Contracts
{
    public class SiteFilterSet
    {
        public int Page { get; set; }
        public string ProjectId { get; set; }
        public List<string> MarketManager { get; set; }
        public List<string> ConstructionManager { get; set; }
        public List<string> Region { get; set; }
        public List<string> Market { get; set; }
        public List<string> Site { get; set; }
        public List<string> Task { get; set; }
    }
}
