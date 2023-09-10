using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Banking.Presentation.GUI.Common
{
    public sealed class RelayCommand : ICommand
    {
        private readonly Action _Execute;
        private readonly Func<bool>? _CanExecute;

        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action execute)
        {
            ArgumentNullException.ThrowIfNull(execute);
            _Execute = execute;
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            ArgumentNullException.ThrowIfNull(execute);
            ArgumentNullException.ThrowIfNull(canExecute);

            _Execute = execute;
            _CanExecute = canExecute;
        }

        public void NotifyCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object? parameter)
        {
            if (_CanExecute == null) return true;

            return _CanExecute.Invoke();
        }

        public void Execute(object? parameter)
        {
            _Execute();
        }
    }
}
