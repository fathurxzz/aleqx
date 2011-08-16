using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    /// <summary>
    /// Рівень відділення
    /// </summary>
    public enum OfficeLevel
    {
        /// <summary>
        /// Центральний рівень
        /// </summary>
        Centre=0,

        /// <summary>
        /// Філія
        /// </summary>
        Branch=1,

        /// <summary>
        /// Безбалансове відділення
        /// </summary>
        NonBalance=2
    }
}