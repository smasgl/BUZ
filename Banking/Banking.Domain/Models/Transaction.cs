namespace Banking.Domain.Models
{
    public class Transaction
    {
        public Transaction(decimal amount, string note, DateTime time)
        {
            Amount = amount;
            Note = note;
            Date = time;
        }

        public decimal Amount { get; }
        public string Note { get; }
        public DateTime Date { get; set; }
    }
}
