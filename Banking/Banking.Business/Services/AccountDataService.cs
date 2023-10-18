using Banking.Domain.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Banking.Business.Services
{
    public static class AccountDataService
    {
        public static List<BankAccount> LoadAccounts()
        {
            if (!File.Exists("Banking.json"))
                return new();

            List<BankAccount> accounts = new List<BankAccount>();

            JsonArray jsonAccounts = JsonArray.Parse(File.ReadAllText("Banking.json")) as JsonArray;
            foreach (dynamic account in jsonAccounts)
            {
                List<Transaction> transations = new();
                foreach (dynamic transaction in account.Transactions as JsonArray)
                {
                    transations.Add(new Transaction(transaction.Amount, transaction.Note, transaction.Date));
                }

                accounts.Add(new BankAccount(account.Owner, account.Number, account.Balance, transations));
            }

            return accounts;
        }

        public static void SaveAccounts(List<BankAccount> accounts)
        {
            JsonArray accountsArray = new JsonArray();
            foreach (var account in accounts)
            {
                JsonArray transations = new JsonArray();
                account.GetTransactions().ToList().ForEach(x =>
                {
                    transations.Add(new JsonObject() { { "Note", x.Note }, { "Date", x.Date }, { "Amount", x.Amount } });
                });

                accountsArray.Add(new JsonObject()
                {
                    { "Owner", account.Owner },
                    { "Number", account.Number },
                    { "Balance", account.Balance },
                    { "Transactions", transations}
                });
            }

            File.WriteAllText("Banking.json", accountsArray.ToJsonString(new System.Text.Json.JsonSerializerOptions() { WriteIndented = true }));
        }
    }
}
