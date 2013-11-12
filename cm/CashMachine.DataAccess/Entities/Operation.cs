namespace CashMachine.DataAccess.Entities
{
    public enum OperationType
    {
        BalanceCheck = 1, 
        Withdraw = 2
    }

    public partial class Operation
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime Date { get; set; }
        public int CardId { get; set; }
        public OperationType OperationType { get; set; }
        public virtual Card Card { get; set; }
    }
}
