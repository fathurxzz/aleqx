using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SiteExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public class PostData : Dictionary<string, Dictionary<string, string>>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class PostCheckboxesData : Dictionary<int, bool>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public static class FormCollectionExtender
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="prefix"></param>
        /// <param name="excludeFields"></param>
        /// <returns></returns>
        public static PostCheckboxesData ProcessPostCheckboxesData(this FormCollection form, string prefix = "", params string[] excludeFields)
        {
            PostCheckboxesData result = new PostCheckboxesData();
            foreach (string key in form.Keys)
            {
                if (!string.IsNullOrEmpty(prefix) && !key.StartsWith(prefix))
                    continue;

                if (excludeFields == null || !excludeFields.Contains(key))
                {
                    string[] item = key.Split('_');
                    int itemId = Convert.ToInt32(item[1]);

                    if (form[key] == "false" || form[key] == "true,false")
                    {
                        if (!result.ContainsKey(itemId))
                        {
                            bool value = form[key] == "true,false";
                            result.Add(itemId, Convert.ToBoolean(value));
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Designed to get checked checkboxes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="form"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public static T[] GetArray<T>(this FormCollection form, string prefix)
        {
            string[] items = form.AllKeys.Where(k => k.StartsWith(prefix)).ToArray();
            List<T> res = new List<T>();
            foreach (string key in items)
            {
                if (form[key] == "true,false")
                    res.Add((T)Convert.ChangeType(key.Replace(prefix, string.Empty), typeof(T)));
            }
            T[] result = res.ToArray();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        /// <param name="prefix"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(this FormCollection form, string prefix)
        {
            string[] items = form.AllKeys.Where(k => k.StartsWith(prefix)).ToArray();
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();
            foreach (var key in items)
            {
                TKey dicKey = (TKey)Convert.ChangeType(key.Replace(prefix, string.Empty), typeof(TKey));
                TValue value = default(TValue);
                if (typeof(TValue) == typeof(bool))
                    value = (TValue)(object)(form[key] == "true,false");
                else
                    value = (TValue)Convert.ChangeType(form[key], typeof(TValue));
                result.Add(dicKey, value);
            }
            return result;
        }
    }


}
