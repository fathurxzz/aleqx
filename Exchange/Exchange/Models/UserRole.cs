using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Exchange.Models.Helpers;

namespace Exchange.Models
{
    public class UserRole
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Возвращает перечень ролей, который текущий пользователь имеет право назначать другим пользователям
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static IEnumerable<UserRole> GetRolesByCurrentUserPermissions(User currentUser, IDbConnection conn)
        {
            string query = "select * from tx_user_role";
            IList<UserRole> userRoles = conn.ReadAsList(r => new UserRole { Id = r.GetValue<int>("role_id"), Name = r.GetValue<string>("role_name") }, query);

            var resultSiteRoles = new List<SiteRoles>();

            if (currentUser.UserRoleIds.Contains((int)SiteRoles.MasterAdmin))
            {
                return userRoles.Select(ur => ur).Where(ur => ur.Id.In(
                    new[]
                    {
                        (int)SiteRoles.MasterAdmin,
                        (int)SiteRoles.AdminFrontOffice, 
                        (int)SiteRoles.AdminCurrencyControler, 
                        (int)SiteRoles.AdminBackOffice,
                        (int)SiteRoles.DealerFrontOffice,
                        (int)SiteRoles.DealerBackOffice,
                        (int)SiteRoles.DealerRegion,
                        (int)SiteRoles.AdminFrontOfficeBranch,
                        (int)SiteRoles.DealerFrontOfficeBranch,
                        (int)SiteRoles.DealerBackOfficeBranch,
                        (int)SiteRoles.CurrencyControlerBranch
                    }));
            }
            if (currentUser.UserRoleIds.Contains((int)SiteRoles.AdminFrontOffice))
            {
                resultSiteRoles.Add(SiteRoles.AdminFrontOffice);
                resultSiteRoles.Add(SiteRoles.DealerFrontOffice);
                resultSiteRoles.Add(SiteRoles.DealerRegion);
                resultSiteRoles.Add(SiteRoles.AdminFrontOfficeBranch);
                resultSiteRoles.Add(SiteRoles.DealerFrontOfficeBranch);
                

                /*return userRoles.Select(ur => ur).Where(ur => ur.Id.In(
                    new[] 
                    {
                        (int)SiteRoles.AdminFrontOffice, 
                        (int)SiteRoles.DealerFrontOffice,
                        (int)SiteRoles.DealerRegion,
                        (int)SiteRoles.AdminFrontOfficeBranch,
                        (int)SiteRoles.DealerFrontOfficeBranch
                    }));*/
            }

            if (currentUser.UserRoleIds.Contains((int)SiteRoles.AdminCurrencyControler))
            {
                resultSiteRoles.Add(SiteRoles.AdminCurrencyControler);
                resultSiteRoles.Add(SiteRoles.CurrencyControlerBranch);

                /*
                return userRoles.Select(ur => ur).Where(ur => ur.Id.In(
                    new[] 
                    {
                        (int)SiteRoles.AdminCurrencyControler, 
                        (int)SiteRoles.CurrencyControlerBranch
                    }));*/
            }

            if (currentUser.UserRoleIds.Contains((int)SiteRoles.DealerRegion))
            {
                resultSiteRoles.Add(SiteRoles.AdminFrontOfficeBranch);
                resultSiteRoles.Add(SiteRoles.DealerFrontOfficeBranch);
                /*
                return userRoles.Select(ur => ur).Where(ur => ur.Id.In(
                    new[]
                        {
                            (int)SiteRoles.AdminFrontOfficeBranch,
                            (int)SiteRoles.DealerFrontOfficeBranch
                        }));*/
            }

            if (currentUser.UserRoleIds.Contains((int)SiteRoles.AdminBackOffice))
            {
                resultSiteRoles.Add(SiteRoles.AdminBackOffice);
                resultSiteRoles.Add(SiteRoles.DealerBackOffice);
            }

            int[] roleIds = resultSiteRoles.Select(r => (int)r).ToArray();

            return userRoles.Select(ur => ur).Where(ur => ur.Id.In(roleIds));
        }

        public override string ToString()
        {
            return Id + ":" + Name;
        }
    }
}