using System;
using System.Windows.Input;

namespace Abr.FlacMetadataEditor.Ui.Helpers
{
    public class RelayCommand : ICommand    
    {
        #region Fields
        
        private readonly Action<object> _execute;    
        private readonly Func<object, bool> _canExecute;    
     
        #endregion
        
        #region Events
        
        public event EventHandler CanExecuteChanged    
        {    
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }    
        
        #endregion
        
        #region Constructors
     
        /// <summary>
        /// Initializes new Instance of <see cref="RelayCommand"/>
        /// </summary>
        /// <param name="execute">Action to execute</param>
        /// <param name="canExecute">Determines whether action can be executed</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)    
        {    
            _execute = execute;    
            _canExecute = canExecute;    
        }
        
        #endregion
        
        #region Methods
     
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);
        
        #endregion
    }  
}