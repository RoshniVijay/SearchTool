using System;
using System.Windows.Input;

namespace ImageSearch.ViewModel
{
    /// <summary>
    /// Search Command triggered from UI
    /// </summary>
    public class SearchCommand : ICommand
    {
        #region ICommand implementaton

        public event EventHandler CanExecuteChanged;
        private readonly Action m_searchAction;

        #endregion ICOmmand implementaton


        #region ICOmmand implementaton

        /// <summary>
        /// Search Command. Uses the injected Action to callback on UI trigger
        /// </summary>
        /// <param name="searchAction">callback to execute on trigger</param>
        public SearchCommand(Action searchAction)
        {
            m_searchAction = searchAction;
        }

        /// <summary>
        /// Can execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Actual action.triggers the action injected in constructor
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            m_searchAction();
        }

        #endregion
    }
}
