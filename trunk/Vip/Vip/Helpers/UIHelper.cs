using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vip.Helpers
{
    public class Pager
    {
        public int TotalCount { get; private set; }
        public int Page { get; private set; }
        public int PageSize { get; private set; }

        public Pager(int totalCount, int pageSise,int page)
        {
            TotalCount = totalCount;
            PageSize = pageSise;
            Page = page;
        }
    }
}