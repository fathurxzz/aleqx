using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace SiteExtensions
{
    public class PostData : Dictionary<string, Dictionary<string, string>>
    {

    }

    public class PostCheckboxesData : Dictionary<int, bool>
    {

    }

    public static class FormCollectionExtender
    {
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
    }
}
