using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccuV.DataAccess;

namespace AccuVAPI.Operations
{
    public class SecurityOperations : IOperationStore
    {
        private IDataSession _session;

        public SecurityOperations(IDataSession session)
        {
            _session = session;
        }


    }
}
