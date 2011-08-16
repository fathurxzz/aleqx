using System.Collections.Generic;
using System.Linq;
using Exchange.Models.Enum;

namespace Exchange.Models.Helpers
{
    public static partial class Helper
    {
        public static IEnumerable<Status> GetStatuses(IEnumerable<Tender> tList)
        {
            var sList = new List<Status>{new Status
                                                {
                                                    Id = -100,
                                                    Name = GetResourceString("All"),
                                                    Selected = WebSession.Status == -100
                                                }};

            sList.AddRange(tList.Select(t => new Status
                                                {
                                                    Id = (int)t.Status,
                                                    Name = t.StatusName,
                                                    Selected = t.Status == (TenderStatus)WebSession.Status
                                                }).Distinct().ToList());

            return sList;
        }

        public static IEnumerable<Status> GetCentreStatuses()
        {
            var statuses = new List<Status>
                       {
                           new Status
                               {
                                   Id = (int)FilterStatus.Recived,
                                   Name = GetResourceString(FilterStatus.Recived.ToString()),
                                   Selected = WebSession.Status == (int)FilterStatus.Recived
                               },
                           new Status
                               {
                                   Id = (int)FilterStatus.ReadyForExecute,
                                   Name = GetResourceString(FilterStatus.ReadyForExecute.ToString()),
                                   Selected = WebSession.Status == (int)FilterStatus.ReadyForExecute
                               },
                       };

            if (WebSession.Operation == EOperation.Sell)
                statuses.Add(new Status
                                 {
                                     Id = (int)FilterStatus.NotMatched,
                                     Name = GetResourceString(FilterStatus.NotMatched.ToString()),
                                     Selected = WebSession.Status == (int)FilterStatus.NotMatched
                                 });

            statuses.Add(new Status
            {
                Id = (int)FilterStatus.Cancelled,
                Name = GetResourceString("m" + FilterStatus.Cancelled),
                Selected = WebSession.Status == (int)FilterStatus.Cancelled
            });

            return statuses;
        }

        public static IEnumerable<Status> GetTransactionStatuses()
        {
            return new List<Status>
                               {
                                   new Status
                                       {
                                           Id = (int) FilterStatus.Recived,
                                           Name = GetResourceString("All"),
                                           Selected = WebSession.TransactionStatus==(int) FilterStatus.Recived
                                       },
                                   new Status
                                       {
                                           Id = (int) FilterStatus.Cancelled,
                                           Name = GetResourceString("statusDeleted"),
                                           Selected = WebSession.TransactionStatus==(int) FilterStatus.Cancelled
                                       },
                                   new Status
                                       {
                                           Id = (int) FilterStatus.NotMatched,
                                           Name = GetResourceString(FilterStatus.NotMatched.ToString()),
                                           Selected = WebSession.TransactionStatus==(int) FilterStatus.NotMatched
                                       }


                               };
        }
    }
}