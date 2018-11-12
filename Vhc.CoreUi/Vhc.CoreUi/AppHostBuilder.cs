using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Vhc.CoreUi.Abstractions;

namespace Vhc.CoreUi
{
    public class AppHostBuilder : IAppHostBuilder
    {
        protected bool _isAppHostBuilt;
        protected IConfiguration _config;
        private readonly List<Action<IServiceCollection>> _configureServiceDelegates;
        private readonly List<Action<IServiceCollection>> _configureHostDelegates;
        protected ICollection<string> _arguments;

        public AppHostBuilder()
        {
            _isAppHostBuilt = false;
            _config = new ConfigurationBuilder().AddInMemoryCollection().Build();
            _configureServiceDelegates = new List<Action<IServiceCollection>>();
            _configureHostDelegates = new List<Action<IServiceCollection>>();
        }

        public virtual IAppHost Build() => Build((services, hostingProvider) => new AppHost(services, hostingProvider, _config, _arguments));

        public IAppHost Build(Func<IServiceCollection, IServiceProvider, AppHost> hostCreatorFunction)
        {
            if (_isAppHostBuilt)
            {
                throw new InvalidOperationException(Resources.AppHostBuilder_SingleInstance);
            }
            _isAppHostBuilt = true;
            IServiceCollection services = BuildServices(_configureServiceDelegates);
            IServiceCollection hostingServices = BuildServices(_configureHostDelegates);

            IServiceProvider hostingProvider = GetProviderFromFactory(hostingServices);
            var host = hostCreatorFunction(services, hostingProvider); 
            try
            {
                host.Initialize();

                return host;
            }
            catch
            {
                // Dispose the host if there's a failure to initialize, this should clean up
                // will dispose services that were constructed until the exception was thrown
                host.Dispose();
                throw;
            }

            IServiceProvider GetProviderFromFactory(IServiceCollection collection)
            {
                var provider = collection.BuildServiceProvider();
                var factory = provider.GetService<IServiceProviderFactory<IServiceCollection>>();

                if (factory != null)
                {
                    using (provider)
                    {
                        return factory.CreateServiceProvider(factory.CreateBuilder(collection));
                    }
                }

                return provider;
            }
        }

        private IServiceCollection BuildServices(List<Action<IServiceCollection>> configureServiceDelegates)
        {
            var services = new ServiceCollection();
            foreach (var configureService in configureServiceDelegates)
            {
                configureService(services);
            }
            return services;
        }

        public IAppHostBuilder ConfigureServices(Action<IServiceCollection> configureDelegate)
        {
            if (configureDelegate == null)
            {
                throw new ArgumentNullException(nameof(configureDelegate));
            }

            _configureServiceDelegates.Add(configureDelegate);
            return this;
        }

        public IAppHostBuilder ConfigureHostServices(Action<IServiceCollection> configureDelegate)
        {
            if (configureDelegate == null)
            {
                throw new ArgumentNullException(nameof(configureDelegate));
            }

            _configureHostDelegates.Add(configureDelegate);
            return this;
        }

        public IAppHostBuilder UseArguments(IEnumerable<string> args)
        {
            if (_arguments is null)
            {
                _arguments = new List<string>();
            }
            foreach (var argument in args)
            {
                _arguments.Add(argument);
            }
            return this;
        }

        public string GetSetting(string key) => _config[key];

        public IAppHostBuilder UseSetting(string key, string value)
        {
            _config[key] = value;
            return this;
        }

        IAppHostBuilder UseStartup(Type startupType)
        {
            var startupAssemblyName = startupType.GetTypeInfo().Assembly.GetName().Name;
            return this
                .UseSetting(AppHostDefaults.StartupAssemblyKey, startupAssemblyName)
                .ConfigureHostServices(services =>
                {
                    if (typeof(IStartup).GetTypeInfo().IsAssignableFrom(startupType.GetTypeInfo()))
                    {
                        services.AddSingleton(typeof(IStartup), startupType);
                    }
                    else
                    {
                        throw new InvalidCastException(Resources.AppHostBuilder_StartupNotAssignable);
                    }
                });
        }

        public IAppHostBuilder UseStartup<T>() where T : class, IStartup => UseStartup(typeof(T));
    }
}
