using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Helpers
{
    public class PostData : Dictionary<string, Dictionary<string, string>>
    {

    }

    public class PostCheckboxesData : Dictionary<int, bool>
    {

    }

    //public class PostDataScalar :Dictionary<int,string>



    public static class FormCollectionExtender
    {
        public static PostData ProcessPostData(this FormCollection form, string prefix = "", params string[] excludeFields)
        {
            PostData result = new PostData();

            foreach (string key in form.Keys)
            {
                if (!string.IsNullOrEmpty(prefix) && !key.StartsWith(prefix))
                    continue;

                if (excludeFields == null || !excludeFields.Contains(key))
                {
                    string[] item = key.Split('_');
                    string itemId = item[1];
                    string fieldName = item[0];
                    if (!result.ContainsKey(itemId))
                        result[itemId] = new Dictionary<string, string>();
                    if (form[key] == "true,false")
                        result[itemId][fieldName] = "true";
                    else
                        result[itemId][fieldName] = form[key];
                }
            }
            return result;
        }

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