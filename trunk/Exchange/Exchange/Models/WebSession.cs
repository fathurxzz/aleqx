using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Exchange.Models.Helpers;

namespace Exchange.Models
{
    public static class WebSession
    {
        /// <summary>
        /// Текущий пользователь
        /// </summary>
        public static User CurrentUser
        {
            get
            {
                if (HttpContext.Current.Session["user"] == null)
                    HttpContext.Current.Session["user"] = new User();

                return (User)HttpContext.Current.Session["user"];
            }
            set
            {
                HttpContext.Current.Session["user"] = value;
            }
        }
        /// <summary>
        /// Текущая валюта
        /// </summary>
        public static int SelectedCurrency
        {
            get
            {
                if (HttpContext.Current.Session["currency"] == null)
                    HttpContext.Current.Session["currency"] = 0;
                return (int)HttpContext.Current.Session["currency"];
            }
            set
            {
                HttpContext.Current.Session["currency"] = value;
            }
        }

        public static string Course
        {
            get
            {
                if (HttpContext.Current.Session["course"] == null)
                    HttpContext.Current.Session["course"] = "all";
                return HttpContext.Current.Session["course"].ToString();
            }
            set
            {
                HttpContext.Current.Session["course"] = value;
            }
        }

        /// <summary>
        /// Мфо
        /// </summary>
        public static string Podrid
        {
            get
            {
                if (HttpContext.Current.Session["podrid"] == null)
                    HttpContext.Current.Session["podrid"] = "";
                return HttpContext.Current.Session["podrid"].ToString();
            }
            set
            {
                HttpContext.Current.Session["podrid"] = value;
            }
        }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session["username"] == null)
                    HttpContext.Current.Session["username"] = "";
                return HttpContext.Current.Session["username"].ToString();
            }
            set
            {
                HttpContext.Current.Session["username"] = value;
            }
        }


        /// <summary>
        /// Статус
        /// </summary>
        public static int Status
        {
            get
            {
                if (HttpContext.Current.Session["status"] == null)
                    HttpContext.Current.Session["status"] = -100;
                return (int)HttpContext.Current.Session["status"];
            }
            set
            {
                HttpContext.Current.Session["status"] = value;
            }
        }

        /// <summary>
        /// Статус трансакции
        /// </summary>
        public static int TransactionStatus
        {
            get
            {
                if (HttpContext.Current.Session["transaction_status"] == null)
                    HttpContext.Current.Session["transaction_status"] = -100;
                return (int)HttpContext.Current.Session["transaction_status"];
            }
            set
            {
                HttpContext.Current.Session["transaction_status"] = value;
            }
        }


        /// <summary>
        /// Операция
        /// </summary>
        public static EOperation Operation
        {
            get
            {
                if (HttpContext.Current.Session["oper"] == null)
                    HttpContext.Current.Session["oper"] = 0;
                return (EOperation)HttpContext.Current.Session["oper"];
            }
            set
            {
                HttpContext.Current.Session["oper"] = (int)value;
            }
        }
        /// <summary>
        /// Признак операции
        /// </summary>
        public static EOperSign OperSign
        {
            get
            {
                if (HttpContext.Current.Session["oper_sign"] == null)
                    HttpContext.Current.Session["oper_sign"] = 0;
                return (EOperSign)HttpContext.Current.Session["oper_sign"];
            }
            set
            {
                HttpContext.Current.Session["oper_sign"] = (int)value;
            }
        }
        /// <summary>
        /// Дата
        /// </summary>
        public static DateTime Date
        {
            get
            {
                if (HttpContext.Current.Session["date"] == null)
                    HttpContext.Current.Session["date"] = DateTime.Now.Date;
                return (DateTime)HttpContext.Current.Session["date"];
            }
            set
            {
                HttpContext.Current.Session["date"] = value;
            }
        }
        /// <summary>
        /// Начальная дата периода
        /// </summary>
        public static DateTime DateFrom
        {
            get
            {
                if (HttpContext.Current.Session["datefrom"] == null)
                    HttpContext.Current.Session["datefrom"] = DateTime.Now.Date;
                return (DateTime)HttpContext.Current.Session["datefrom"];
            }
            set
            {
                HttpContext.Current.Session["datefrom"] = value;
            }
        }
        /// <summary>
        /// Конечная дата периода
        /// </summary>
        public static DateTime DateTo
        {
            get
            {
                if (HttpContext.Current.Session["dateto"] == null)
                    HttpContext.Current.Session["dateto"] = DateTime.Now.Date;
                return (DateTime)HttpContext.Current.Session["dateto"];
            }
            set
            {
                HttpContext.Current.Session["dateto"] = value;
            }
        }

        /// <summary>
        /// Последнее время квитовки трансакций с заявками
        /// </summary>
        public static DateTime LastMatchingTime
        {
            get
            {
                if (HttpContext.Current.Session["matching_time"] == null)
                    HttpContext.Current.Session["matching_time"] = DateTime.Now;
                return (DateTime)HttpContext.Current.Session["matching_time"];
            }
            set
            {
                HttpContext.Current.Session["matching_time"] = value;
            }
        }


        public static ExchangeSettings ExchangeSettings
        {
            get
            {
                if (HttpContext.Current.Session["settings"] == null)
                    HttpContext.Current.Session["settings"] = ExchangeSettings.LoadSettings();
                return (ExchangeSettings)HttpContext.Current.Session["settings"];
            }
            set
            {
                HttpContext.Current.Session["settings"] = value;
            }
        }


        public static void SetValues(FilterValuesUpdater fvu, out List<CustomMessage> errorMessages)
        {
            errorMessages = new List<CustomMessage>();
            if (fvu.OperId.HasValue) Operation = (EOperation)fvu.OperId.Value;
            if (fvu.CurId.HasValue) SelectedCurrency = fvu.CurId.Value;
            if (fvu.Course != null) Course = fvu.Course;
            if (fvu.Status.HasValue) Status = fvu.Status.Value;
            if (fvu.TransactionStatus.HasValue) TransactionStatus = fvu.TransactionStatus.Value;
            if (fvu.Podrid != null) Podrid = fvu.Podrid;
            if (fvu.UserName != null) UserName = fvu.UserName;
            if (fvu.OperSign.HasValue) OperSign = (EOperSign) fvu.OperSign.Value;

            if (!string.IsNullOrEmpty(fvu.DateFrom))
            {
                DateTime dateFrom;
                if (DateTime.TryParse(fvu.DateFrom, out dateFrom))
                {
                    DateFrom = dateFrom;
                }
                else
                {
                    errorMessages.Add(new CustomMessage { Type = MessageType.Error, Text = Helper.GetResourceString("ConvertingDataError") });
                }
            }
            if (!string.IsNullOrEmpty(fvu.DateTo))
            {
                DateTime dateTo;
                if (DateTime.TryParse(fvu.DateTo, out dateTo))
                {
                    DateTo = dateTo;
                }
                else
                {
                    errorMessages.Add(new CustomMessage { Type = MessageType.Error, Text = Helper.GetResourceString("ConvertingDataError") });
                }
            }

            if (!string.IsNullOrEmpty(fvu.Date))
            {
                DateTime date;
                if (DateTime.TryParse(fvu.Date, out date))
                {
                    Date = date;
                }
                else
                {
                    errorMessages.Add(new CustomMessage { Type = MessageType.Error, Text = Helper.GetResourceString("ConvertingDataError") });
                }
            }
        }
    }
}