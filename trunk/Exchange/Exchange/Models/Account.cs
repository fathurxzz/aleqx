using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Exchange.Models.Helpers;

namespace Exchange.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Account
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int OperId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OfficeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OfficeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CurId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Nls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int OperSign { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Podrid { get; set; }
    }
}