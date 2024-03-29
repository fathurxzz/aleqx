﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Excursions.Models;

namespace Excursions
{
    public static class ApplicationData
    {
        public static string NewCommentNotificationEmail
        {
            get { return GetApplicationData("newCommentNotificationEmail"); }
            set { SetApplicationData("newCommentNotificationEmail", value); }
        }

        public static string ContactsPanelText
        {
            get { return GetApplicationData("contactsPanelText"); }
            set { SetApplicationData("contactsPanelText", value); }
        }

        public static string FeedbackNotificationEmail
        {
            get { return GetApplicationData("feedbackNotificationEmail"); }
            set { SetApplicationData("feedbackNotificationEmail", value); }
        }

        public static string Email
        {
            get { return GetApplicationData("email"); }
            set { SetApplicationData("email", value); }
        }

        private static string GetApplicationData(string key)
        {
            using (var context = new SettingsStorage())
            {
                var result = context.ApplicationSettings.Where(a => a.Key == key).Select(a => a.Value).FirstOrDefault();
                return result;
            }
        }

        private static void SetApplicationData(string key, string value)
        {
            using (var context = new SettingsStorage())
            {
                var result = context.ApplicationSettings.Where(a => a.Key == key).Select(a => a).FirstOrDefault();
                if (result == null)
                {
                    var appSettings = new ApplicationSettings { Key = key, Value = value };
                    context.AddToApplicationSettings(appSettings);
                }
                else
                {
                    result.Value = value;
                }
                context.SaveChanges();
            }
        }
    }
}