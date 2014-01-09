using System.Collections.Generic;
using AccuV.DataAccess.Entities;

namespace AccuVAPI.Contracts
{
    public class SiteResultSet
    {
        public int Total { get; set; }
        public IEnumerable<Site> Sites { get; set; }
    }
}