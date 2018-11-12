using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

        private CancellationTokenSource cancellationTokenSource;

        private readonly ICollection<string> _arguments;

        public AppHost(IServiceCollection appServices, IServiceProvider hostingProvider, IConfiguration configuration, ICollection<string> arguments)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            _configurationBuilder = new ConfigurationBuilder().AddConfiguration(configuration);
            _arguments = arguments;
            _hostingProvider = hostingProvider ?? throw new ArgumentNullException(nameof(hostingProvider));
            _applicationServiceCollection = appServices ?? throw new ArgumentNullException(nameof(appServices));
            _isRunning = false;
        }

        public IServiceProvider Services => _appServices;

        public ICollection<string> Arguments => _arguments;

        internal void Initialize()
        {
            _applicationServiceCollection.AddSingleton<IDependancyResolver>(this);

            var startup = _hostingProvider.GetService<IStartup>();
            // Build configuration
            if (startup != null)
            {
                startup.Configure(_configurationBuilder);
            }
            _config = _configurationBuilder.Build();
            
            // Build services
            if (startup != null)
            {
                _applicationServiceCollection.AddSingleton<IConfiguration>(_config);
                startup.ConfigureServices(_applicationServiceCollection, _config);
                _entryPoint = startup.Start;
            }
            _appServices = _applicationServiceCollection.BuildServiceProvider();
        }

        public void Run() => Run(_entryPoint);

        public void Run(Action<IAppHost> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(Resources.AppHost_EntryActionNotDefined);
            }
            if (!_isRunning)
            {
                action?.Invoke(this);
                _isRunning = true;
            }
        }


        public async Task RunAsync(Func<IAppHost, CancellationToken, Task> asyncFunction)
        {
            if (asyncFunction is null)
            {
                throw new ArgumentNullException(Resources.AppHost_EntryActionNotDefined);
            }
            if (!_isRunning)
            {
                cancellationTokenSource = new CancellationTokenSource();
                await asyncFunction(this, cancellationTokenSource.Token);
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
            cancellationTokenSource.Cancel();
        }

    }
}
