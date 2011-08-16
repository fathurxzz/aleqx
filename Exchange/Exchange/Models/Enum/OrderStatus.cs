using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models.Enum
{
    /// <summary>
    /// Статус розпорядження
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Сформована
        /// </summary>
        Created = 0,

        /// <summary>
        /// Відправлене у філію (для розпорядження)
        /// </summary>
        SentToBranch = 9,

        /// <summary>
        /// Відправлене у філію та на оплату (для розпорядження)
        /// </summary>
        SentToBranchAndPay = 10
    }
}