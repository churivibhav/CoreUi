using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Vhc.CoreUi.Abstractions;

namespace Vhc.CoreUi
{
    public class AppHost : IAppHost, IDependancyResolver
    {
        private IServiceProvider _appServices;
        private readonly IServiceCollection _applicationServiceCollection;

        private IConfiguration _config;
        private readonly IConfigurationBuilder _configurationBuilder;

        private IServiceProvider _hostingProvider;

        private Action<IAppHost> _entryPoint;

        private bool _isRunning;

        public AppHost(IServiceCollection appServices, IServiceProvider hostingProvider, IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            _configurationBuilder = new ConfigurationBuilder().AddConfiguration(configuration);

            _hostingProvider = hostingProvider ?? throw new ArgumentNullException(nameof(hostingProvider));
            _applicationServiceCollection = appServices ?? throw new ArgumentNullException(nameof(appServices));
            _isRunning = false;
        }

        public IServiceProvider Services => _appServices;

        internal void Initialize()
        {
            _applicationServiceCollection.AddSingleton<IDependancyResolver>(this);

            var startup = _hostingProvider.GetService<IStartup>();
            if (startup != null)
            {
                startup.ConfigureServices(_applicationServiceCollection);
                startup.Configure(_configurationBuilder);
            }
            _config = _configurationBuilder.Build();
            _applicationServiceCollection.AddSingleton<IConfiguration>(_config);
            _appServices = _applicationServiceCollection.BuildServiceProvider();
            _entryPoint = startup.Start;
        }

        public void Run() => Run(_entryPoint);

        public void Run(Action<IAppHost> action)
        {
            if (!_isRunning)
            {
                action?.Invoke(this);
                _isRunning = true;
            }
        }

        public void Dispose()
        {
            if (_isRunning)
            {
                Stop();
            }

            (_appServices as IDisposable)?.Dispose();
            (_hostingProvider as IDisposable)?.Dispose();
        }

        private void Stop()
        {
            if (_isRunning) return;
            _isRunning = false;
            // TODO : Stop
        }
    }
}
