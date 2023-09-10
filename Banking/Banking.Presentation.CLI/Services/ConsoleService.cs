using Banking.Domain.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Presentation.CLI.Services
{
    public static class ConsoleService
    {
        public static string Choose(string[] menu)
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose an option")
                    .PageSize(menu.Length)
                    .AddChoices(menu));
        }

        public static BankAccount ChooseBankaccount(List<BankAccount> accounts)
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<BankAccount>()
                    .Title("Choose an option")
                    .PageSize(10)
                    .AddChoices(accounts));
        }

        public static void Header()
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("Banking")
                    .LeftJustified()
                    .Color(Color.Red));
        }

        public static BankAccount CreateBankAccount()
        {
            string owner = AnsiConsole.Prompt(
            new TextPrompt<string>("What is the namew of the owner?")
                .PromptStyle("red"));

            decimal amount = AnsiConsole.Prompt(
            new TextPrompt<decimal>("How much should the initial balance be?")
                .PromptStyle("red"));

            return new(owner, amount);
        }

        public static void ShowAllBankaccounts(List<BankAccount> accounts)
        {
            AnsiConsole.Write(new Rows(accounts.Select(x => new Text(x.ToString()))));
        }

        public static void ShowAllTransactions(List<Transaction> transactions)
        {
            AnsiConsole.Write(new Rows(transactions.Select(x => new Text(x.ToString()))));
        }

        public static Transaction AskTransaction()
        {
            decimal amount = AnsiConsole.Prompt(
            new TextPrompt<decimal>("How much do you want to transfer?")
                .PromptStyle("red"));

            string note = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter a note for the transaction:")
                .PromptStyle("red"));

            return new(amount, note, DateTime.Now);
        }

        public static bool RunAgain()
        {
            return AnsiConsole.Confirm("Return to the menu?");
        }
    }
}
