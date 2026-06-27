using Microsoft.Extensions.DependencyInjection;
using BullsAndCowsWPF.Services.Interfaces;

namespace BullsAndCowsWPF.Services
{
    public static class ServicesRegistrator
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<INumberGenerator, RandomNumberGenerator>();
            services.AddSingleton<IGameEngine, GameEngine>();
            return services;
        }
    }
}
