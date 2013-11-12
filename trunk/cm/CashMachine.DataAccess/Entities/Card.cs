using System.Collections.Generic;

namespace CashMachine.DataAccess.Entities
{
    public partial class Card
    {
        public Card()
        {
            this.Operations = new List<Operation>();
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public bool Locked { get; set; }
        public decimal Balance { get; set; }
        public string Pin { get; set; }
        public int PinAttemptsCount { get; set; }
        public virtual ICollection<Operation> Operations { get; set; }
    }
}
