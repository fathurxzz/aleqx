using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CashMachine.Models
{
    public class WithdrawForm
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Сумма")]
        public decimal Amount { get; set; }
    }
}