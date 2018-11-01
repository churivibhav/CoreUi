using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vhc.CoreUi.Abstractions
{
    public interface IStartup
    {
        void ConfigureServices(IServiceCollection services, IConfiguration config);

        void Configure(IConfigurationBuilder config);

        /// <summary>
        /// The starting point of the application
        /// </summary>
        /// <param name="app">Application Host</param>
        void Start(IAppHost app);
    }
}
