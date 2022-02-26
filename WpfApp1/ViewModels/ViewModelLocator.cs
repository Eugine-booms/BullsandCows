using BullsAndCowsWPF.ViewModels.Base;

using Microsoft.Extensions.DependencyInjection;

using WpfApp1;

namespace BullsAndCowsWPF.ViewModels
{
    internal class ViewModelLocator
    {
        internal MainWindowsViewModel MainViewModel => App.Host.Services.GetService<MainWindowsViewModel>();
    }
}
