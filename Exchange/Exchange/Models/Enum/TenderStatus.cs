using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    /// <summary>
    /// Статус заявки
    /// </summary>
    public enum TenderStatus
    {
        /// <summary>
        /// Часково сформована
        /// </summary>
        PartiallyCreated = -1,

        /// <summary>
        /// Сформована
        /// </summary>
        Created = 0,

        /// <summary>
        /// Відправлена (до ЦО)
        /// </summary>
        Sent = 2,

        /// <summary>
        /// Скасована
        /// </summary>
        Cancelled = 99,

        /// <summary>
        /// Вилучена
        /// </summary>
        ReadyForDelete = 66
    }
    
    public enum FilterStatus
    {
        Recived = -100,
        ReadyForExecute=2,
        NotMatched=3,
        Cancelled=4
    }
}