using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    /// <summary>
    /// Ознака операції
    /// </summary>
    public enum EOperSign
    {
        /// <summary>
        /// Валютна позиція
        /// </summary>
        CurrencyPosition = 1,

        /// <summary>
        /// Кошти клієнтів
        /// </summary>
        ClientCash = 2
    }
}