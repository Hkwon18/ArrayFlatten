using System.Windows.Input;
using ArrayFlatten.ViewModels;

namespace ArrayFlatten.Commands
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
            return _ViewModel.IsFlattenButtonEnabled;
        }

        public void Execute(object parameter)
        {
            _ViewModel.DoFlattenButton(_ViewModel.InputArrayString);
        }
    }
}