
using Microsoft.Extensions.DependencyInjection;

using WpfApp1;

namespace BullsAndCowsWPF.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowsViewModel MainViewModel => App.Host.Services.GetService<MainWindowsViewModel>();
    }
}
