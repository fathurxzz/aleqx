using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AccuV.DataAccess;
using AccuV.DataAccess.Contracts;

namespace AccuV.UI.Controllers.Api
{
    public class CloseoutPackageController : ApiController
    {
        private IDataSession _session;

        public CloseoutPackageController(IDataSession session)
        {
            _session = session;
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<controller>
        public async Task<IEnumerable<IDictionary<string, string>>> Post([FromBody]CloseoutpackageViewRequest request)
        {
            var result = await _session.GetCloseoutPackageViewData(request);

            return result.Select(item => item.ToDictionary(k => k.Key, v => FormatField(v)));
        }

        private string FormatField(object field)
        {
            if (field == null || field is DBNull)
                return null;
            if (field is DateTime)
                return ((DateTime) field).ToString("MM/dd/yyyy");
            return field.ToString();
        }
    }
}