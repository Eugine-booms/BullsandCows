using Microsoft.Extensions.DependencyInjection;

namespace BullsAndCowsWPF.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowsViewModel MainViewModel => App.Host.Services.GetRequiredService<MainWindowsViewModel>();
    }
}
