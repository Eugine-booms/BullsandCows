using Microsoft.Extensions.DependencyInjection;

namespace BullsAndCowsWPF.ViewModels
{
    public static class ViewModelRegistrator
    {
        public static IServiceCollection RegisterViewModel(this IServiceCollection services)
        {
            services.AddSingleton<MainWindowsViewModel>();
            services.AddSingleton<GameViewModel>();
            services.AddSingleton<SettingsViewModel>();
            return services;
        }
    }
}
