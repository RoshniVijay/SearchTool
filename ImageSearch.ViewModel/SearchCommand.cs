
using System;
using System.Windows.Input;

namespace ImageSearch.ViewModel
{
    public class SearchCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action m_searchAction;


        public SearchCommand(Action searchAction)
        {
            m_searchAction = searchAction;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            m_searchAction();
        }
    }
}
