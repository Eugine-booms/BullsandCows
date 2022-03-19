using System;
using System.Windows.Input;
using System.Windows.Markup;

namespace BullsAndCowsWPF.Infrastructure.Command.Base
{
    [MarkupExtensionReturnType(typeof(BaseCommand))]
    [ContentProperty("Executable")]
    public abstract class BaseCommand : MarkupExtension, ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public virtual bool CanExecute(object parameter) => true;


        public abstract void Execute(object parameter);

    }
}
