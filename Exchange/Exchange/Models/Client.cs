using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Exchange.Models.Helpers;

namespace Exchange.Models
{
    public class Client
    {
        public string ClientCode { get;  set; }
        public string ClientName { get;  set; }
        public int ContrCode { get; set; }
        public int SubjId { get; set; }

        public static IEnumerable<Client> GetClientsFromOdb(string podrid, string okpo, string contrCode)
        {
            if (WebSession.CurrentUser.OfficeOkpo == okpo)
            {
                var client = new Client
                {
                    SubjId = 0,
                    ClientName = WebSession.CurrentUser.OfficeName,
                    ContrCode = 0,
                    ClientCode = okpo
                };
                return new List<Client> { client };
            }

            var query = string.Format("select sbj_id,name,cntr_code,okpo from cln_subj where dep_id={0} ", podrid);
            if (!string.IsNullOrEmpty(contrCode.Trim()))
                query = query + string.Format("and cntr_code={0} ", contrCode);
            if (!string.IsNullOrEmpty(okpo.Trim()))
                query = query + string.Format("and okpo ='{0}'", okpo);
            using (IDbConnection conn = DbConnector.GetCustomConnection("HPClients"))
            {
                return conn.ReadAsList(r => new Client
                {
                    SubjId = r.GetValue<int>("sbj_id"),
                    ClientName = r.GetValue<string>("name").Trim(),
                    ContrCode = r.GetValue("cntr_code", 0),
                    ClientCode = r.GetValue<string>("okpo").Trim()
                }, query);
            }
        }
    }
}