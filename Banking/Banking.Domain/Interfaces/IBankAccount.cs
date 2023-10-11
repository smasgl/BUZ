using Banking.Domain.Models;

namespace Banking.Domain.Interfaces
{
    public interface IBankAccount
    {
        decimal Balance { get; }
        string Number { get; }
        string Owner { get; }

        string ExportTransactionsAsCsv(DateTime from, DateTime to);
        string ExportTransactionsAsJson(DateTime from, DateTime to);
        IEnumerable<Transaction> GetTransactions(DateTime? from = null, DateTime? to = null);
        void MakeDeposit(decimal amount, string note, DateTime? date = null);
        void MakeWithdrawal(decimal amount, string note, DateTime? date = null);
        string ToString();
    }
}