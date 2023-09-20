using Banking.Domain.Interfaces;
using System.Text;

namespace Banking.Domain.Models
{
    public class BankAccount : IBankAccount
    {
        private static int AccountCounter = 1;

        public string Number { get; }

        public string Owner { get; }

        public decimal Balance => _Transactions.Sum(x => x.Amount);

        private readonly List<Transaction> _Transactions;

        public BankAccount(string owner, decimal initialBalance)
        {
            Owner = owner;
            Number = $"AT42 1234 5678 {AccountCounter:0000}";
            _Transactions = new List<Transaction>();

            MakeDeposit(initialBalance, "Initial Balance");

            AccountCounter++;
        }

        public void MakeDeposit(decimal amount, string note, DateTime? date = null)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposite amount must be greater than zero.");
            }

            date ??= DateTime.Now;

            var depositeTransaction = new Transaction(amount, note, date.Value);
            _Transactions.Add(depositeTransaction);
        }

        public void MakeWithdrawal(decimal amount, string note, DateTime? date = null)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Widthdrawal amount must be greater than zero.");
            }

            date ??= DateTime.Now;

            var widthdrawalTransaction = new Transaction(-amount, note, date.Value);
            _Transactions.Add(widthdrawalTransaction);
        }

        public string ExportTransactionsAsCsv(DateTime from, DateTime to)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Date;Amount;Note");

            decimal deposites = 0;
            decimal withdrawals = 0;

            foreach (var transaction in _Transactions)
            {
                if (transaction.Date >= from && transaction.Date <= to)
                {
                    sb.AppendFormat("{0};{1};{2}", transaction.Date, transaction.Amount, transaction.Note);
                    sb.AppendLine();

                    if (transaction.Amount < 0)
                        withdrawals += transaction.Amount;
                    else
                        deposites += transaction.Amount;
                }
            }

            sb.AppendLine();

            sb.AppendFormat("{0};{1};{2}", DateTime.Now, deposites, "Deposites").AppendLine();
            sb.AppendFormat("{0};{1};{2}", DateTime.Now, withdrawals, "Withdrawals").AppendLine();
            sb.AppendFormat("{0};{1};{2}", DateTime.Now, withdrawals + deposites, "Sum").AppendLine();

            return sb.ToString();
        }

        public override string ToString()
        {
            return $"Bank Account\n-------------\n\tOwner: {Owner}\n\tIBAN: {Number}\n\tBalance: {Balance:c2}";
        }

        public IEnumerable<Transaction> GetTransactions(DateTime? from = null, DateTime? to = null)
        {
            from ??= DateTime.MinValue;
            to ??= DateTime.MaxValue;

            return _Transactions.Where(x => x.Date >= from && x.Date <= to);
        }
    }
}
