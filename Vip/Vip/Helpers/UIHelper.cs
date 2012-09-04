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
        public int PageCount { get; private set; }
        public List<int> Sequence { get; private set; }
        private int _k;
        private const int Range = 4;

        public Pager(int totalCount, int pageSize, int page)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            Page = page;
            PageCount = totalCount / pageSize;
            PageCount += ((totalCount % pageSize) > 0) ? 1 : 0;

            Sequence = new List<int>();

            _k = -1;

            for (int i = 0; i < PageCount; i++)
            {
                if (IsValueInRange(PageCount, i, Range, page))
                    Sequence.Add(i);
                else if (i == PageCount-2)
                {
                    if (!Sequence.Contains(_k))
                        Sequence.Add(_k);
                }
                else
                {
                    if (!Sequence.Contains(_k))
                        Sequence.Add(_k);
                }
            }

            for (int i = 0; i < Sequence.Count; i++)
            {
                if (Sequence[i] == -2)
                    Sequence[i] = -1;
            }
        }

        private bool IsValueInRange(int count, int value, int range, int currentPosition)
        {
            if (value == 0 || value == count - 1)
                return true;

            if (value >= (currentPosition - range) && value <= (currentPosition + range))
            {
                _k = -2;
                return true;
            }
            return false;
        }
    }
}