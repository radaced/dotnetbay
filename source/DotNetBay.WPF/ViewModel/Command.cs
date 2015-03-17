using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DotNetBay.WPF.ViewModel
{
    public class RelayCommand<T> : ICommand
    {
        public Action<T> ExecuteDelegate { get; set; }
        public Predicate<T> CanExecuteDelegate { get; set; }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if(execute == null)
                throw new ArgumentNullException("execute");

            ExecuteDelegate = execute;
            CanExecuteDelegate = canExecute;
        }

        public RelayCommand(Action<T> execute) : this(execute, null)
        {
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteDelegate == null ? true : CanExecuteDelegate((T) parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteDelegate((T) parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
