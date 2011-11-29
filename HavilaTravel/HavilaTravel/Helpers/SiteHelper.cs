using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HavilaTravel.Helpers
{
    public static class SiteHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Значение</param>
        /// <param name="values">Перечень значений, в которые должно входить тестируемое значение</param>
        /// <returns>true, если values содержит value</returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">Значение</param>
        /// <param name="minValue">Минимальное значение</param>
        /// <param name="maxValue">Максимальное значение</param>
        /// <returns>true, если values содержит value</returns>
        public static bool Between<T>(this T value, T minValue, T maxValue) where T : IComparable<T>
        {
            if (value == null)
                return false;

            if (minValue == null)
                throw new ArgumentNullException("minValue");

            if (maxValue == null)
                throw new ArgumentNullException("maxValue");

            bool result = (value.CompareTo(minValue) >= 0) & (value.CompareTo(maxValue) <= 0);

            return maxValue.CompareTo(minValue) >= 0 ? result : !result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns>Возвращает произвольный элемент коллекции</returns>
        public static T GetRandomItem<T>(this IQueryable<T> source)
        {
            var count = source.Count();
            if (count == 0)
                return source.FirstOrDefault();
            var index = new Random().Next(0, count);
            return source.ToArray()[index];
        }

        public static T GetRandomItem<T>(this List<T> source)
        {
            var count = source.Count();
            if (count == 0)
                return source.FirstOrDefault();
            var index = new Random().Next(0, count);
            return source.ToArray()[index];
        }

    }
}