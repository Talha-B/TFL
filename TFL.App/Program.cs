using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using System;
using System.IO;
using TFL.Common.Settings.TflApi;

namespace TFL.App
{
    public class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        
        public static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var serviceProvider = ConfigureIoC(new ServiceCollection());
            serviceProvider.GetService<App>().Run();
        }

        public static IServiceProvider ConfigureIoC(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<TflApiSettings>(options => Configuration.GetSection("TflApiSettings").Bind(options));
            services.AddTransient<App>();

            var container = new Container(config =>
            {
                config.Scan(x =>
                {
                    x.AssembliesAndExecutablesFromApplicationBaseDirectory(f => f.FullName.StartsWith("TFL.", StringComparison.OrdinalIgnoreCase));
                    x.LookForRegistries();
                    x.WithDefaultConventions();
                });

                config.Populate(services);
            });

            return container.GetInstance<IServiceProvider>();
        }
    }
}
