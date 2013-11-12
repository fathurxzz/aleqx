using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CashMachine.Models
{

    [Table("Card")]
    public class Card
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Number { get; set; }
        public bool Locked { get; set; }
        public decimal Balance { get; set; }
        public string Pin { get; set; }
        public byte PinAttemptsCount { get; set; }
    }

    [Table("Operation")]
    public class Operation
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("CardId")]
        public virtual Card Card { get; set; }
        [ForeignKey("OperationTypeId")]
        public virtual OperationType OperationType { get; set; }
    }

    [Table("OperationType")]
    public class OperationType
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
    }

}