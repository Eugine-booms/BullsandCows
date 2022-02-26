using Microsoft.Extensions.DependencyInjection;

namespace BullsAndCowsWPF.Services
{
    public static  class ServicesRegistrator
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            return services;
        }

    }
}
