using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exchange.Models.Helpers
{
    public static class FormCollectionExtender
    {
        public static PostData ProcessPostData(this FormCollection form, string controlIdPrefix)
        {
            var result = new PostData();
            foreach (string key in form.Keys.Cast<string>().Where(key => key.StartsWith(controlIdPrefix)))
            {
                string[] item = key.Split('_');
                string itemId = item[1];
                result.Add(itemId, form[key] == "true,false" ? "true" : form[key]);
            }
            return result;
        }

        public static FormAction GetFormAction(this FormCollection form)
        {
            foreach (string key in form.Keys.Cast<string>().Where(key => key.StartsWith("fa")))
            {
                switch (key)
                {
                    case "faSend":
                        return FormAction.Send;
                    case "faDelete":
                        return FormAction.Delete;
                    case "faCancel":
                        return FormAction.Cancel;
                    case "faExecute":
                        return FormAction.Execute;
                    case "faSendToBranch":
                        return FormAction.SendToBranch;
                    case "faSendToBranchAndPayment":
                        return FormAction.SendToBranchAndPayment;
                    case "faBackToCreatedStatus":
                        return FormAction.BackToCreatedStatus;
                    case "faUpdate":
                        return FormAction.Update;
                }
            }
            throw new IndexOutOfRangeException("Cannot get action");
        }

        //public static int[] CollectObjectIds(this FormCollection form, string objectControlPrefix = "cb")
        //{
        //    PostData postData = form.ProcessPostData(objectControlPrefix);
        //    return postData.GetTenderIds().ToArray();
        //}

        public static T[] CollectObjectIds<T>(this FormCollection form, string objectControlPrefix = "cb")
        {
            PostData postData = form.ProcessPostData(objectControlPrefix);
            return postData.GetIds<T>().ToArray();
        }

    }

    public class PostData : Dictionary<string, string>
    {
        public List<int> GetTenderIds()
        {
            return (from key in this where Convert.ToBoolean(key.Value) select Convert.ToInt32(key.Key)).ToList();
        }

        public List<T> GetIds<T>()
        {
            return (from key in this where Convert.ToBoolean(key.Value) select (T)Convert.ChangeType(key.Key, typeof(T))).ToList();
        }
    }



    public enum FormAction
    {
        Send,
        Delete,
        Cancel,
        Execute,
        SendToBranch,
        SendToBranchAndPayment,
        Print,
        BackToCreatedStatus,
        Update
    }
}