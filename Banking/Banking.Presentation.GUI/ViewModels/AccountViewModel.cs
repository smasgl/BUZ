using Banking.Domain.Interfaces;
using Banking.Domain.Models;
using Banking.Presentation.GUI.Common;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Presentation.GUI.ViewModels
{
    class AccountViewModel : BaseViewModel
    {
        private readonly IBankAccount _BankAccount;

        public AccountViewModel(IBankAccount bankAccount)
        {
            _BankAccount = bankAccount;

            ReloadTransactions();

            MakeDeposite = new RelayCommand(
                execute: () =>
                {
                    _BankAccount.MakeDeposit(Amount, Note);
                    ReloadTransactions();

                    Note = "";
                    Amount = 0;

                },
                canExecute: CanAddTransaction);

            MakeWithdrawal = new RelayCommand(
                execute: () =>
                {
                    _BankAccount.MakeWithdrawal(Amount, Note);
                    ReloadTransactions();

                    Note = "";
                    Amount = 0;
                },
                canExecute: CanAddTransaction);

            Export = new RelayCommand(
                execute: () =>
                {
                    SaveFileDialog saveFileDialogCsv = new SaveFileDialog();
                    saveFileDialogCsv.Filter = "CSV file (*.csv)|*.csv";
                    StreamWriter writer = null;

                    if (saveFileDialogCsv.ShowDialog() ?? false)
                    {
                        var transactionHistory = bankAccount.ExportTransactionsAsCsv(DateTime.MinValue, DateTime.MaxValue);
                        File.WriteAllText(saveFileDialogCsv.FileName, transactionHistory);
                    }

                    SaveFileDialog saveFileDialogJson = new SaveFileDialog();
                    saveFileDialogJson.Filter = "JSON file (*.json)|*.json";
                    StreamWriter writerJson = null;

                    if (saveFileDialogJson.ShowDialog() ?? false)
                    {
                        var transactionHistory = bankAccount.ExportTransactionsAsJson(DateTime.MinValue, DateTime.MaxValue);
                        File.WriteAllText(saveFileDialogJson.FileName, transactionHistory);
                    }
                });
        }

        #region properties

        public RelayCommand MakeDeposite { get; }
        public RelayCommand MakeWithdrawal { get; }
        public RelayCommand Export { get; }

        private List<Transaction> _Transactions;
        public List<Transaction> Transactions
        {
            get { return _Transactions; }
            set
            {
                _Transactions = value;
                NotifyPropertyChanged(nameof(Transactions));
                NotifyPropertyChanged(nameof(Balance));
            }
        }

        public string Owner => _BankAccount.Owner;
        public string AccountNumber => _BankAccount.Number;
        public decimal Balance => _BankAccount.Balance;


        private string _Note = "";
        public string Note
        {
            get { return _Note; }
            set
            {
                _Note = value;
                NotifyPropertyChanged(nameof(Note));
                MakeDeposite.NotifyCanExecuteChanged();
                MakeWithdrawal.NotifyCanExecuteChanged();
            }
        }

        private decimal _Amount;
        public decimal Amount
        {
            get { return _Amount; }
            set
            {
                _Amount = value;
                NotifyPropertyChanged(nameof(Amount));
                MakeDeposite.NotifyCanExecuteChanged();
                MakeWithdrawal.NotifyCanExecuteChanged();
            }
        }

        #endregion

        #region private functions

        private bool CanAddTransaction()
        {
            if (string.IsNullOrEmpty(Note)) return false;
            if (Amount <= 0) return false;

            return true;
        }

        [MemberNotNull(nameof(_Transactions))]
        private void ReloadTransactions()
        {
            Transactions = _BankAccount.GetTransactions().ToList();
        }

        #endregion

        public override string ToString()
        {
            return $"{Owner} - {AccountNumber}";
        }
    }
}
