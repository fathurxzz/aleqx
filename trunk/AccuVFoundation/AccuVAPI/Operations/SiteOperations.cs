using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccuV.DataAccess.Entities;

namespace AccuVAPI.Operations
{
    public interface ISiteOperations : IOperationStore
    {
        IQueryable<Site> Sites { get; }
    }

    public class SiteOperations
    {

    }
}
