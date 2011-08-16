using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Exchange.Models.Entities
{
    public abstract class TenderDeal 
    {
        /// <summary>
        /// Ідинтифікатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ідентифікатор користувача, що сформував документ
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// Ім'я користувача, що сформував документ
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Ім'я користувача, що сформував документ
        /// </summary>
        public string UserPhone { get; set; }


        public int CurId { get; set; }
        
        public string CurName { get; set; }

        public string CurDef { get; set; }
       

        /// <summary>
        /// Операція
        /// </summary>
        public EOperation Operation { get; set; }

        /// <summary>
        /// Назва операції
        /// </summary>
        public string OperName { get; set; }

        /// <summary>
        /// Ознака операції
        /// </summary>
        public EOperSign OperSign { get; set; }

        /// <summary>
        /// Ознака операції
        /// </summary>
        public string OperSignName { get; set; }

        /// <summary>
        /// Сума валюти
        /// </summary>
        public decimal DocSum { get; set; }

        


        /// <summary>
        /// Назва клієнта
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Код ЄДРПОУ клієнта
        /// </summary>
        public string ClientCode { get; set; }

        /// <summary>
        /// Ідентифікатор клієнта
        /// </summary>
        public string ClientSubjId { get; set; }

        /// <summary>
        /// Код контрагента клієнта
        /// </summary>
        public string ClientCntrCode { get; set; }

        /// <summary>
        /// Додаткова комісія (грн.)
        /// </summary>
        public decimal? ExtraCommissionSum { get; set; }

        /// <summary>
        /// Дата створення
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Рахунок
        /// </summary>
        public string Nls { get; set; }

        /// <summary>
        /// Комісія (коефіцієнт)
        /// </summary>
        public decimal Commission { get; set; }

        /// <summary>
        /// МФО відділення
        /// </summary>
        public string Podrid { get; set; }

        /// <summary>
        /// Назва філії
        /// </summary>
        public string OfficeName { get; set; }

        /// <summary>
        /// Назва філії, до якої належить безбалансове відділення, якщо відділення таким є
        /// </summary>
        public string ParentOfficeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClientUahPodrid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClientUahNls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClientCurrPodrid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClientCurrNls { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        public decimal Course { get; set; }

        /// <summary>
        /// Код філії
        /// </summary>
        public int OfficeId { get; set; }

        /// <summary>
        /// Код філії, до якої належить безбалансове відділення, якщо відділення таким є
        /// </summary>
        public int ParentOfficeId { get; set; }

        /// <summary>
        /// Назва статусу
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// Ознака участі в аукціоні
        /// </summary>
        public bool Auction { get; set; }

    }
}