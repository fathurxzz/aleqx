using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Exchange.Models.Factories;

namespace Exchange.Models
{
    /// <summary>
    /// Посадова особа, яка підписує документ
    /// </summary>
    public partial class Signer
    {
        /// <summary>
        /// Ідентифікатор посадової особи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// П.І.Б посадової особи
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Посада
        /// </summary>
        public string Post { get; set; }

        /// <summary>
        /// Ідентифікатор відділення
        /// </summary>
        public int OfficeId { get; set; }


    }
}