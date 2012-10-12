using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vip.Helpers
{
    public static class LinqHelper
    {
        public static bool In<T>(this T value, params T[] values)
        {
            if (value == null)
                return false;
            foreach (var val in values)
            {
                if (value.Equals(val))
                    return true;
            }
            return false;
        }

        public static T RandomElement<T>(this IEnumerable<T> source,
                                 Random rng)
        {
            T current = default(T);
            int count = 0;
            foreach (T element in source)
            {
                count++;
                if (rng.Next(count) == 0)
                {
                    current = element;
                }
            }
            if (count == 0)
            {
                throw new InvalidOperationException("Sequence was empty");
            }
            return current;
        }

    }




}