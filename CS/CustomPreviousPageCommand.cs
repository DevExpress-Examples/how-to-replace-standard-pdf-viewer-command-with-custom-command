using System;
using System.Windows.Input;

namespace DXSample {
  public  class CustomPreviousPageCommand : ICommand {
        ICommand baseCommand;
        public CustomPreviousPageCommand(ICommand baseCommand) {
            this.baseCommand = baseCommand;
        }

        public event EventHandler CanExecuteChanged {
            add { baseCommand.CanExecuteChanged += value; }
            remove { baseCommand.CanExecuteChanged -= value; }
        }

        public bool CanExecute(object parameter) {
            return baseCommand.CanExecute(parameter);
        }

        public void Execute(object parameter) {
            baseCommand.Execute(parameter);
        }
    }
}