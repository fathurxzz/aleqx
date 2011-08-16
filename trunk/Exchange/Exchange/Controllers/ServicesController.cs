using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using Exchange.Models;
using Exchange.Models.Helpers;

namespace Exchange.Controllers
{
    public class ServicesController : Controller
    {
        //
        // GET: /Services/

        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public ActionResult CheckingData()
        {
            using (var conn = DbConnector.Connection )
            {
                var messages = new List<CustomMessage>();

                var limitTime = new DateTime(
                    DateTime.Now.Year, 
                    DateTime.Now.Month, 
                    DateTime.Now.Day, 
                    WebSession.ExchangeSettings.CheckingCourseMorTime.Hour, 
                    WebSession.ExchangeSettings.CheckingCourseMorTime.Minute, 
                    WebSession.ExchangeSettings.CheckingCourseMorTime.Second);

                if (DateTime.Now > limitTime)
                {
                    int unfilledCourseMorCnt = UnfilledCourseMorCount(conn);
                    if (unfilledCourseMorCnt > 0)
                        messages.Add(new CustomMessage
                                         {Text = "Есть сделки с незаполненым курсом MOR", Type = MessageType.Warning});
                }

                return View("CustomMessages", messages);
            }
        }

        private int UnfilledCourseMorCount(IDbConnection conn)
        {
            string query = "select count(1) as cnt from tx_deal where course_mor is null and status=?";
            return conn.ExecuteScalar<int>(query,(int)DealStatus.Sent);
        }


    }
}
