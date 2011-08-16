using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Exchange.Models.Helpers;

namespace Exchange.Models
{
    public partial class User : BaseUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OfficeId { get; set; }
        public DateTime LastActivityDate { get; set; }
        public string TabNum { get; set; }
        public bool IsAutorized { get; set; }
        public string Podrid { get; set; }
        public string Phone { get; set; }
        public string OfficeName { get; set; }
        public string ParentOfficeName { get; set; }
        public string UserCode { get; set; }
        public DateTime? DateClose { get; set; }
        public string UserIdOdb { get; set; }
        public OfficeLevel OfficeLevel { get; set; }
        public int ParentOfficeId { get; set; }

        public string Language { get; set; }
        public IList<int> UserRoleIds = new List<int>();
        public string OfficeOkpo { get; set; }

        public IList<UserCurrency> CurrencyList { get; set; }
        public IList<int> ModeratorOfficeIdList { get; set; }


        public User()
        {
            /*
            if (HttpContext.Current.Request.ClientCertificate.IsPresent)
            {
                var cert = new Certificate(HttpContext.Current.Request);
                Podrid = cert.Podrid;
                TabNum = cert.TabNum;
                Name = cert.UserName;
                UserCode = cert.Podrid + cert.TabNum;

                if (base.GetSiteRoles(Podrid, TabNum).Count > 0)
                {
                    GetUser(UserCode, Name, Podrid, TabNum);
                }
            }*/
        }

        
        public User(int userId)
        {
            using (var conn = DbConnector.Connection)
            {
                GetUser(conn, userId);
            }
        }

        public User(int userId, IDbConnection conn)
        {
            GetUser(conn, userId);
        }

        private void GetUser(IDbConnection conn, int userId)
        {
            string query = "exec tx_getUserById ?";
            using (IDataReader dr = conn.ExecuteReader(query, userId))
            {
                while (dr.Read())
                {
                    InitUser(dr);
                }
            }
            query = "select role_id from tx_user_role_mapping where user_id=?";
            UserRoleIds = conn.ReadAsList(r => r.GetValue<int>("role_id"), query, userId);

            query = "select * from tx_user_currency_mapping where user_id=?";
            CurrencyList = conn.ReadAsList(r => new UserCurrency {CurId = r.GetValue<int>("cur_id"), AllowedProcessing = r.GetValue<bool>("processing")}, query, userId);

            if(UserRoleIds.Contains((int)SiteRoles.DealerRegion))
            {
                query = "select office_id from tx_moderator_mapping where user_id=?";
                ModeratorOfficeIdList = conn.ReadAsList(r => r.GetValue<int>(0), query, userId);
            }
        }

        private void InitUser(IDataRecord dr)
        {
            Id = dr.GetValue<int>("id");
            Name = dr.GetValue<string>("name").Trim();
            Phone = dr.GetValue("phone", string.Empty).Trim();
            TabNum = dr.GetValue<string>("tab_num").Trim();
            Podrid = dr.GetValue<string>("podrid").Trim();
            OfficeId = dr.GetValue<int>("office_id");
            OfficeName = dr.GetValue<string>("office_name");
            ParentOfficeName = dr.GetValue<string>("parent_office_name");
            LastActivityDate = dr.GetValue<DateTime>("last_activity_date");
            UserIdOdb = dr.GetValue<string>("user_id_odb");
            OfficeLevel = dr.GetValue<OfficeLevel>("office_level");
            ParentOfficeId = dr.GetValue<int>("parent_office_id");
            IsAutorized = dr.GetValue<bool>("is_autorized");
            UserCode = dr.GetValue<string>("user_code");
            Language = dr.GetValue<string>("language");
            DateClose = dr.GetValue<DateTime?>("date_close");
            OfficeOkpo = dr.GetValue<string>("okpo");
        }

        protected override List<int> GetSiteRoles(string podrid, string tabNum)
        {
            var result = new List<int> { /*(int)SiteRoles.AdminBranch,*/ (int)SiteRoles.DealerFrontOfficeBranch };
            SiteRoleIds = result;
            return result;
        }

        private void GetUser(string userCode, string userName, string podrid, string tabNum)
        {

            using (var conn = DbConnector.Connection)
            {
                var userId = conn.ExecuteScalar<int?>("select user_id from tx_user where user_code=?", userCode);
                string query;
                if (!userId.HasValue)
                {
                    OfficeLevel officeLevel = (OfficeLevel)SiteRoleIds.First() - 1;
                    query = "select office_id from tx_office where podrid=? and office_level=?";
                    var officeId = conn.ExecuteScalar<int>(query, podrid, (int)officeLevel);
                    userId = CreateUser(conn, userName, tabNum, userCode, officeId);
                }

                GetUser(conn, userId.Value);

                query = "update tx_user set last_activity_date=getdate() where id=?";
                conn.ExecuteNonQuery(query, userId.Value);
            }
        }

        private static int CreateUser(IDbConnection conn, string userName, string tabNum, string userCode, int officeId)
        {
            using (IDbTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    int userId = Helper.GetId(conn, "tx_user");
                    string query = "insert into tx_user(id,name,office_id,tab_num,parent_office_id,user_code)VALUES(?,?,?,?,?,?)";
                    conn.ExecuteNonQuery(query, userId, userName, officeId, tabNum, officeId, userCode);
                    return userId;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Проверяет соответствие ролей пользователя списку передаваемых ролей
        /// </summary>
        /// <param name="roles">Список ролей</param>
        /// <returns>Возвращает true если пользователь имеет хотя-бы одну роль, которая содержится в списке ролей параметра</returns>
        public bool HasRole(IEnumerable<SiteRoles> roles)
        {
            return roles.Any(role => UserRoleIds.Contains((int) role));
        }
    }
}