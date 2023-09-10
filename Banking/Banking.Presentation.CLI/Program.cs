using Banking.Domain.Interfaces;
using Banking.Domain.Models;

namespace Banking.Presentation.CLI
{
    internal class Program
    {
        static void Main()
        {
            // Sicherstellen, dass € zeichen auch ausgegeben wird
            System.Console.OutputEncoding = System.Text.Encoding.Unicode;

            var bankAccount = new BankAccount("Martin Burtscher", 3500.50m);
            Run(bankAccount);
        }

        static void Run(IBankAccount bankAccount)
        {
            bankAccount.MakeDeposit(500.20m, "Geld von einem Freund");
            bankAccount.MakeWithdrawal(22.50m, "Einkaufen Spar");

            System.Console.WriteLine("Balance of {0}({1}): {2:c}", bankAccount.Owner, bankAccount.Number, bankAccount.Balance);

            var exeFolder = Path.GetDirectoryName(Environment.ProcessPath);
            if (exeFolder == null)
                return;

            var exportFilePath = Path.Combine(exeFolder, bankAccount.Owner.Replace(' ', '_') + ".csv");

            var transactionHistory = bankAccount.ExportTransactionsAsCsv(DateTime.MinValue, DateTime.MaxValue);
            File.WriteAllText(exportFilePath, transactionHistory);
        }
    }
}