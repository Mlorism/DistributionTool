using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DistributionTool.ViewModels
{
	class RelayCommand : ICommand
	{
		private readonly Action<object> _execute;
		private readonly Predicate<object> _canExecute;

		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null) throw new NullReferenceException("execute");

			_execute = execute;
			_canExecute = canExecute;
		}

		public RelayCommand(Action<object> execute) : this(execute, null)
		{

		}

		public event EventHandler CanExecuteChanged
		{
			add => CommandManager.RequerySuggested += value; // add { CommandManager.RequerySuggested += value;}
			remove => CommandManager.RequerySuggested -= value; // remove { CommandManager.RequerySuggested -= value;}
		}

		/// <summary>
		/// If canExecute predicate was not send to RelayCommand then allways allow command to execute
		/// </summary>

		public bool CanExecute(object parameter)
		{
			return _canExecute == null ? true : _canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			_execute.Invoke(parameter);
		}


	}
}
