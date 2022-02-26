using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using System;

using WpfApp1;

namespace BullsAndCowsWPF
{
    public static class Programm
    {

        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] Args)
        {
            var host_builder = Host.CreateDefaultBuilder(Args)
                .UseContentRoot(Environment.CurrentDirectory)
                .ConfigureAppConfiguration((host, cfg) =>
                {
                    cfg.SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("appsettings.json",true, true);
                }).ConfigureServices(App.ConfigureServices);


            return host_builder;
        }
    }
}
