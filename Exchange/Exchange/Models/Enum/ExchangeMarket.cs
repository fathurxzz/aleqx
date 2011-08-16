using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    /// <summary>
    /// Валютний ринок, на якому реалізується заявка
    /// </summary>
    public enum ExchangeMarket
    {
        /// <summary>
        /// Внутрішній валютний ринок
        /// </summary>
        VVR=1,

        /// <summary>
        /// Міжбанківський валютний ринок України
        /// </summary>
        MVRU=2
    }

    
}