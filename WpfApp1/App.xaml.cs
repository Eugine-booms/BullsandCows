using BullsAndCowsWPF;
using BullsAndCowsWPF.Services;
using BullsAndCowsWPF.ViewModels;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost _host;
        public static IHost Host => _host ?? Programm.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();  

        internal static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services
                .RegisterViewModel()
                .RegisterServices();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;
            base.OnStartup(e);
            await host.StartAsync().ConfigureAwait(false);
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            _host = null;
        }
    }
}
