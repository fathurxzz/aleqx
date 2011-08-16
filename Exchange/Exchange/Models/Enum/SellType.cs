using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    /// <summary>
    /// Вид продажу
    /// </summary>
    public enum SellType
    {
        /// <summary>
        /// Зверхобов'язковий продаж
        /// </summary>
        StrongRequirementSale = 1,

        /// <summary>
        /// Обов'язковий продаж
        /// </summary>
        RequirementSale = 2
    }
}