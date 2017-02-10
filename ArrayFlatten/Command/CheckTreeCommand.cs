using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ArrayFlatten.ViewModel;

namespace ArrayFlatten.Command
{


    internal class CheckTreeCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the UpdateCustomerCommand class.
        /// </summary>
        public CheckTreeCommand(TreeViewModel viewModel)
        {
            _ViewModel = viewModel;
        }

        private TreeViewModel _ViewModel;

        public event System.EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return _ViewModel.CanFlattenInput;
        }

        public void Execute(object parameter)
        {
            _ViewModel.WrapArrayify(_ViewModel.InputArrayString);
        }
    }
}