using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Exchange.Models.Helpers;

namespace Exchange.Models.Factories
{
    public class MessageFactory
    {
        private readonly IDbConnection _conn;
        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
        }

        public MessageFactory(IDbConnection connection)
        {
            _conn = connection;
        }

        public bool Send(Message m)
        {
            return Save(m);
        }



        public bool Update(IEnumerable<Message> messages)
        {
            return messages.All(Update);
        }

        public bool Update(Message message)
        {
            try
            {
                _conn.ExecuteNonQuery("update tx_conversations set is_read=? where id=?", message.Read, message.Id);
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
            return true;
        }
        
        public IEnumerable<Recepient> GetRecepients(string mask)
        {
            if(string.IsNullOrEmpty(mask))
                return new List<Recepient>();
            string query =
                string.Format(
@"
select 
    t1.id, 
    t1.name, 
    t2.name as office_name,
    1 as type
from tx_user t1 
join tx_office t2 on t2.office_id=t1.office_id 
where t1.name like '%{0}%'
union
select
	office_id as id,
	podrid as name,
	name as office_name,
    2 as type
from tx_office
where name like '%{0}%' or podrid like '%{0}%'",
                    mask);
            return _conn.ReadAsList(r => 
                new Recepient
                    {
                        Id = r.GetValue<int>("id"),
                        Name = r.GetValue<string>("name"),
                        OfficeName = r.GetValue<string>("office_name"),
                        Type = r.GetValue<RecipientType>("type")
                    }, query).Take(20);
        }

        public IEnumerable<Message> GetUnreadMessages()
        {
            string query = "exec tx_getUnreadMessages ?,?";
            return _conn.ReadAsList(r => 
                new Message
                    {
                        Id = r.GetValue<int>("id"),
                        Date = r.GetValue<DateTime>("date"),
                        OfficeId = r.GetValue<int>("office_id"),
                        OfficeName = r.GetValue<string>("office_name"),
                        OfficeNameRcv = r.GetValue<string>("office_rcv_name"),
                        OfficeRcv = r.GetValue<int>("office_rcv"),
                        ParentId = r.GetValue<int?>("office_rcv"),
                        Read = r.GetValue<bool>("is_read"),
                        TenderId = r.GetValue<int?>("tender_id"),
                        Text = r.GetValue<string>("text"),
                        UserId = r.GetValue<int>("user_id"),
                        UserName = r.GetValue<string>("user_name"),
                        UserRcv = r.GetValue<int>("user_rcv"),
                        UserNameRcv = r.GetValue<string>("user_rcv_name")
                    }, 
                    query, WebSession.CurrentUser.Id, WebSession.CurrentUser.OfficeId);
        }

        public IEnumerable<Message> GetMessagePreviews()
        {
            string query = "tx_getMessagePreviewList ?";
            return _conn.ReadAsList(r => new Message
                                             {
                                                 Date = r.GetValue<DateTime>("date"),
                                                 OfficeId = r.GetValue<int>("office_id"),
                                                 OfficeName = r.GetValue<string>("office_name"),
                                                 Text = r.GetValue<string>("text"),
                                                 UserId = r.GetValue<int>("user_id"),
                                                 UserName = r.GetValue<string>("user_name"),
                                                 UnreadCount = r.GetValue<int>("unread")
                                             }, query, WebSession.CurrentUser.Id);
        }


        public IEnumerable<Message> GetMessagesByUser(int userId)
        {
            string query = "tx_getMessagesByUser ?,?";
            return _conn.ReadAsList(r => new Message
                                             {
                                                 Id = r.GetValue<int>("id"),
                                                 Date = r.GetValue<DateTime>("date"),
                                                 OfficeId = r.GetValue<int>("office_id"),
                                                 OfficeName = r.GetValue<string>("office_name"),
                                                 OfficeNameRcv = r.GetValue<string>("office_rcv_name"),
                                                 OfficeRcv = r.GetValue<int>("office_rcv"),
                                                 ParentId = r.GetValue<int?>("office_rcv"),
                                                 Read = r.GetValue<bool>("is_read"),
                                                 TenderId = r.GetValue<int?>("tender_id"),
                                                 Text = r.GetValue<string>("text"),
                                                 UserId = r.GetValue<int>("user_id"),
                                                 UserName = r.GetValue<string>("user_name"),
                                                 UserRcv = r.GetValue<int>("user_rcv"),
                                                 UserNameRcv = r.GetValue<string>("user_rcv_name")
                                             }, query, userId, WebSession.CurrentUser.Id);
        }


        private bool Save(Message m)
        {
            using (var tran = _conn.BeginTransaction())
            {
                try
                {

                    int id = Helper.GetId(_conn, "tx_conversations");
                    string query =
                        "insert into tx_conversations(id,text,office_id,office_rcv,parent_id,is_read,user_id,user_rcv,date,tender_id,archived)values(?,?,?,?,?,?,?,?,?,?,?)";
                    _conn.ExecuteNonQuery(query,
                                          id,
                                          m.Text,
                                          m.OfficeId,
                                          m.OfficeRcv,
                                          m.ParentId.With(DbType.Int32),
                                          0,
                                          m.UserId,
                                          m.UserRcv,
                                          m.Date,
                                          m.TenderId.With(DbType.Int32),
                                          0
                        );
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    _errorMessage = ex.Message;
                }
            }
            return false;
        }


    }
}