using BullsAndCowsWPF.Infrastructure.Command.Base;

using System;
using System.Windows.Markup;

namespace BullsAndCowsWPF.Infrastructure.Command
{
    [MarkupExtensionReturnType(typeof(LambdaCommand))]
    public class LambdaCommand : BaseCommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;
        /// <summary>
        ///  Команда с делегатами
        /// </summary>
        /// <param name="execute">Выполнение </param>
        /// <param name="canExecute"> Возможность выполнения</param>
        public LambdaCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }
        /// <summary>
        /// Команда с делегатами без параметров
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecuted"></param>
        //public LambdaCommand(Action execute, Func<bool> canExecute = null) :
        //    this(
        //        execute: p => execute(),
        //        canExecute: canExecute == null ? null : p => canExecute())
        //{ }

        public override void Execute(object parameter)
        {
            execute?.Invoke(parameter);
        }
        public override bool CanExecute(object parameter)
        {
            return canExecute?.Invoke(parameter) ?? true;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}
