using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AccuV.DataAccess;
using AccuV.DataAccess.Contracts;
using AccuV.DataAccess.Entities;

using Newtonsoft.Json;

namespace AccuVAPI.Operations
{
    public interface IMarketManagersOperations : IOperationStore
    {
        IEnumerable<Manager> GetMarketManagers(IEnumerable<Activity> activities, string projectId);
    }

    public class MarketManagersOperations:IMarketManagersOperations
    {
        private readonly IDataSession _session;

        public MarketManagersOperations(IDataSession session)
        {
            _session = session;
        }

        public IQueryable<Manager> Managers
        {
            get
            {
                return _session.Managers.AsNoTracking();
            }
        }

        public IEnumerable<Manager>  GetMarketManagers(IEnumerable<Activity> activities,string projectId)
        {
            return _session.Managers.AsNoTracking();
        }
    }
}
