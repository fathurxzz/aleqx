using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    /// <summary>
    /// Статус угоди
    /// </summary>
    public enum DealStatus
    {
        /// <summary>
        /// Сформована
        /// </summary>
        Created = 0,

        /// <summary>
        /// Відправлена
        /// </summary>
        Sent = 2
    }
}