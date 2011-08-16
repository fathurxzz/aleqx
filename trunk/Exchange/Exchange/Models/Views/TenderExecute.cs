using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exchange.Models.Helpers;

namespace Exchange.Models.Views
{
    /// <summary>
    /// Модель представления формы выполнения заявок
    /// </summary>
    public class TenderExecute
    {
        public IEnumerable<Tender> Tenders { get; set; }
        public string TenderIds { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public decimal DocSum { get; set; }
        public ExecuteMethod ExecuteMethod { get; set; }
        //public int SelectedCurrency { get; set; }
        public IEnumerable<SelectListItem> ExchangeMarket { get; set; }
        public int OperId { get; set; }
        public int OperSign { get; set; }
        public int CurId { get; set; }
        public EOperation Operation { get; set; }
        public decimal Course { get; set; }
        public IEnumerable<Order> ExistedOrders { get; set; }
        public List<decimal> Courses { get; set; }
    }

    /// <summary>
    /// Метод выполнения заявок
    /// </summary>
    public enum ExecuteMethod
    {
        /// <summary>
        /// По одной заявке
        /// </summary>
        Single,

        /// <summary>
        /// Пакетное выполнение
        /// </summary>
        Multiply,

        /// <summary>
        /// Пакетное выполнение (заявки с разнами курсами в разные распоряжения)
        /// </summary>
        ByCourse
    }
}