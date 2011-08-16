using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Exchange.Models.Enum;
using Exchange.Models.Views;

namespace Exchange.Models.Helpers
{

    public class SumPair
    {


        public decimal Buy { get; set; }
        public decimal Sell { get; set; }

        public SumPair(decimal sum, EOperation oper)
        {
            switch (oper)
            {
                case EOperation.Buy:
                    Buy = sum;
                    break;
                case EOperation.Sell:
                    Sell = sum;
                    break;
            }
        }
    }

    public static partial class Helper
    {
        public static IEnumerable<TenderTotalsSummary> GetTenderTotalSumByCurrency(this IEnumerable<Tender> tList)
        {
            var h = new Hashtable();
            foreach (var t in tList)
            {
                var key = t.CurId;
                if (h[key] != null)
                {
                    var tmp = (SumPair)h[key];
                    if (t.Operation == EOperation.Buy)
                        tmp.Buy += t.Rest;
                    else
                        tmp.Sell += t.Rest;
                    h[key] = tmp;
                }
                else
                    h.Add(key, new SumPair(t.Rest, t.Operation));
            }

            /*foreach (DictionaryEntry entry in h)
            {
                var sumPair = (SumPair) entry.Value;
                totals.Add(new TenderTotalsSummary{CurId = (int)entry.Key,AmountBuy = sumPair.Buy,AmountSell = sumPair.Sell});
            }*/

            return (from DictionaryEntry entry in h
                    let sumPair = (SumPair)entry.Value
                    select new TenderTotalsSummary
                               {
                                   CurId = (int)entry.Key,
                                   AmountBuy = sumPair.Buy,
                                   AmountSell = sumPair.Sell
                               }).ToList();
        }

        public static string TenderIdsToString(this IEnumerable<Tender> tList)
        {
            return tList.Aggregate(string.Empty, (current, tender) => current + tender.Id + ",");
        }

        public static bool ValidateTendersBeforeExecute(this IEnumerable<Tender> tList, ExecuteMethod executeMethod, out string message)
        {
            message = string.Empty;
            if (executeMethod == ExecuteMethod.Single || executeMethod == ExecuteMethod.Multiply || (executeMethod == ExecuteMethod.ByCourse && tList.Count() > 0))
            {
                if (tList.Select(t => t.CurId).Distinct().ToList().Count() > 1)
                {
                    message = "Список заявок к выполнению содержит заявки с разными валютами";
                    return false;
                }

                if (tList.Select(t => t.Operation).Distinct().ToList().Count() > 1)
                {
                    message = "Список заявок к выполнению содержит заявки с разными операциями";
                    return false;
                }

                if (tList.Select(t => t.OperSign).Distinct().ToList().Count() > 1)
                {
                    message = "Список заявок к выполнению содержит заявки с разными типами операций";
                    return false;
                }
            }
            return true;
        }

        public static IEnumerable<Tender> ApplyCentreStatusFilter(this IEnumerable<Tender> tList, FilterStatus status)
        {
            switch (status)
            {
                case FilterStatus.Recived:
                    return tList.Where(t => t.Status == TenderStatus.Sent).ToList();

                case FilterStatus.ReadyForExecute:
                    return tList.Where(t => t.Rest > 0 && t.ExecuteSign && t.Status == TenderStatus.Sent).ToList();

                case FilterStatus.Cancelled:
                    return tList.Where(t => t.Status == TenderStatus.Cancelled).ToList();
            }
            return tList;
        }
    }
}