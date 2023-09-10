using Banking.Domain.Interfaces;
using Banking.Domain.Models;
using Banking.Presentation.CLI.Services;

namespace Banking.Presentation.CLI
{
    internal class Program
    {
        public static List<BankAccount> BankAccounts { get; set; } = new();

        static void Main()
        {
            // Sicherstellen, dass € zeichen auch ausgegeben wird
            System.Console.OutputEncoding = System.Text.Encoding.Unicode;

            string[] menu = { "Neues Konto anlegen", "Alle verfügbaren Konten anzeigen", "Transaktionen eines Kontos anzeigen", "Einzahlung / Auszahlung durchführen", "Csv Export erstellen und an einen vom Benutzer eingegebenen Pfad speichern" };

            while (true)
            {
                ConsoleService.Header();
                ChosenMenu(ConsoleService.Choose(menu));
            }
        }

        static void CreateAccount()
        {
            BankAccounts.Add(ConsoleService.CreateBankAccount());
            Console.WriteLine("A new account has been added.");
        }

        static void AllAccounts()
        {
            if (BankAccounts.Count <= 0)
            {
                Console.WriteLine("No accounts where found!");
                return;
            }
            ConsoleService.ShowAllBankaccounts(BankAccounts);
        }

        static void AccountTransaction()
        {
            if (BankAccounts.Count <= 0)
            {
                Console.WriteLine("No accounts where found!");
                return;
            }
            BankAccount bankAccount = ConsoleService.ChooseBankaccount(BankAccounts);
            ConsoleService.ShowAllTransactions(bankAccount.GetTransactions(DateTime.MinValue, DateTime.MaxValue).ToList());
        }

        static void CreateTransaction()
        {
            string[] transactionOption = { "Deposit", "Withdraw", "Exit" };
            string option = ConsoleService.Choose(transactionOption);

            if (BankAccounts.Count <= 0)
            {
                Console.WriteLine("No accounts where found!");
                return;
            }
            BankAccount bankAccount = ConsoleService.ChooseBankaccount(BankAccounts);


            switch (option)
            {
                case "Deposit":
                    var deposit = ConsoleService.AskTransaction();
                    bankAccount.MakeDeposit(deposit.Amount, deposit.Note, deposit.Date);
                    break;
                case "Withdraw":
                    var withdraw = ConsoleService.AskTransaction();
                    bankAccount.MakeWithdrawal(withdraw.Amount, withdraw.Note, withdraw.Date);
                    break;
                case "Exit":
                    Environment.Exit(0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("That menu options was not found!");
            }
        }

        static void ExportCSV()
        {
            if (BankAccounts.Count <= 0)
            {
                Console.WriteLine("No accounts where found!");
                return;
            }
            BankAccount bankAccount = ConsoleService.ChooseBankaccount(BankAccounts);

            var exeFolder = Path.GetDirectoryName(Environment.ProcessPath);
            if (exeFolder == null)
                return;

            var exportFilePath = Path.Combine(exeFolder, bankAccount.Owner.Replace(' ', '_') + ".csv");

            var transactionHistory = bankAccount.ExportTransactionsAsCsv(DateTime.MinValue, DateTime.MaxValue);
            File.WriteAllText(exportFilePath, transactionHistory);
            Console.WriteLine($"All the transaction of {bankAccount.Owner} have been saved at:\n{exportFilePath}");
        }

        static void ChosenMenu(string option)
        {
            switch (option)
            {
                case "Neues Konto anlegen":
                    CreateAccount();
                    break;
                case "Alle verfügbaren Konten anzeigen":
                    AllAccounts();
                    break;
                case "Transaktionen eines Kontos anzeigen":
                    AccountTransaction();
                    break;
                case "Einzahlung / Auszahlung durchführen":
                    CreateTransaction();
                    break;
                case "Csv Export erstellen und an einen vom Benutzer eingegebenen Pfad speichern":
                    ExportCSV();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("That menu options was not found!");
            }

            if(!ConsoleService.RunAgain())
                Environment.Exit(0);
        }
    }
}