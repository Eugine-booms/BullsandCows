
using BullsAndCowsWPF.Infrastructure.Command.Base;

using System;
using System.Windows;

namespace BullsAndCowsWPF.Infrastructure.Command
{
    internal class CloseAppCommand : BaseCommand
    {
        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => this;

    }
}
