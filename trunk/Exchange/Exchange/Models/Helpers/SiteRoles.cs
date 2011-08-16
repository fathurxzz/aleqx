using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models.Helpers
{
    [Serializable]
    [Flags]
    public enum SiteRoles
    {
        /// <summary>
        /// Разработчик
        /// </summary>
        MasterAdmin = 0,

        AdminFrontOffice = 11,
        AdminCurrencyControler = 21,
        AdminBackOffice = 31,

        DealerFrontOffice = 111,
        DealerBackOffice = 311,

        DealerRegion = 12,



        AdminFrontOfficeBranch = 131,
        DealerFrontOfficeBranch = 132,
        DealerBackOfficeBranch = 331,


        CurrencyControlerBranch=23
    }
}