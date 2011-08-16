using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Exchange.Models.Helpers;

namespace Exchange.Models.Factories
{
    public class UserFactory
    {
        private readonly IDbConnection _conn;

        public UserFactory(IDbConnection connection)
        {
            _conn = connection;
        }

        public IEnumerable<UserPresentation> GetUsers()
        {
            return GetUsers(WebSession.CurrentUser.OfficeId);
        }

        public IEnumerable<UserPresentation> GetUsers(int officeId)
        {
            var uList = new List<UserPresentation>();
            const string query = "exec tx_getUsersByOfficeId ?";
            _conn.ReadToCollection(uList, r => UserPresentation.InitUserPresentation(new UserPresentation(), r), query, officeId);
            return uList;
        }

        /// <summary>
        /// Инициализация пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        public User GetUser(int userId)
        {
            var user = new User(userId, _conn);
            return user.Id > 0 ? user : null;
        }






        private void UpdateUser(User user)
        {
            string query = "update tx_user set office_id=?, phone=?, user_id_odb=?, parent_office_id=?, is_autorized=?, language=?, date_close=? where id=?";
            _conn.ExecuteNonQuery(query, user.OfficeId, user.Phone, user.UserIdOdb, user.ParentOfficeId, user.IsAutorized, user.Language, user.DateClose.With(DbType.DateTime), user.Id);
        }

        public void SaveChanges(User user)
        {
            UpdateUser(user);
            UpdateUserRights(user);
            UpdateUserCurrency(user);
            UpdateModeratorOfficeIdList(user);
        }

        public void SaveChanges(User user, bool updateMainData, bool updateUserRights, bool updateUserCurrency, bool updateModeratorOfficeIdList)
        {
            if (updateMainData)
            {
                UpdateUser(user);
            }
            if (updateUserRights)
            {
                UpdateUserRights(user);
            }
            if(updateUserCurrency)
            {
                UpdateUserCurrency(user);
            }
            if(updateModeratorOfficeIdList)
            {
                UpdateModeratorOfficeIdList(user);
            }
        }

        private void UpdateModeratorOfficeIdList(User user)
        {
            if(user.ModeratorOfficeIdList==null)
                return;
            string query = "delete from tx_moderator_mapping where user_id=?";
            _conn.ExecuteNonQuery(query, user.Id);

            foreach (var officeId in user.ModeratorOfficeIdList)
            {
                query = "insert into tx_moderator_mapping (user_id,office_id)values(?,?)";
                _conn.ExecuteNonQuery(query, user.Id, officeId);
            }
        }

        private void UpdateUserCurrency(User user)
        {
            if(user.CurrencyList==null)
                return;
            string query = "delete from tx_user_currency_mapping where user_id=?";
            _conn.ExecuteNonQuery(query, user.Id);

            foreach (var currency in user.CurrencyList)
            {
                query = "insert into tx_user_currency_mapping (user_id,cur_id,processing)values(?,?,?)";
                _conn.ExecuteNonQuery(query, user.Id, currency.CurId, Convert.ToInt32(currency.AllowedProcessing));
            }
        }

        private void UpdateUserRights(User user)
        {
            string query = "delete from tx_user_role_mapping where user_id=?";
            _conn.ExecuteNonQuery(query, user.Id);

            foreach (int roleId in user.UserRoleIds)
            {
                query = "insert into tx_user_role_mapping (user_id,role_id)values(?,?)";
                _conn.ExecuteNonQuery(query, user.Id, roleId);
            }
        }

        public IList<Moderator> GetModerators()
        {
            string query = "exec tx_getModerators ?";
            return _conn.ReadAsList(r => Moderator.InitModerator(new Moderator(), r), query, (int)SiteRoles.DealerRegion);
        }

    }
}