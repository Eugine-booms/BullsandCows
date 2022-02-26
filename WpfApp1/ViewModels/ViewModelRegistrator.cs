using Microsoft.Extensions.DependencyInjection;

namespace BullsAndCowsWPF.ViewModels
{
    public static class ViewModelRegistrator
    {
      public static IServiceCollection RegisterViewModel(this IServiceCollection services)
        {
            return services.AddSingleton<MainWindowsViewModel>();
        }
    }
}
