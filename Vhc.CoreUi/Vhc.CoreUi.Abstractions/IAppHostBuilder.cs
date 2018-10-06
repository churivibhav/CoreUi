using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Vhc.CoreUi.Abstractions
{
    public interface IAppHostBuilder
    {
        string GetSetting(string key);
        IAppHostBuilder UseSetting(string key, string value);
        IAppHostBuilder UseArguments(IEnumerable<string> args);
        IAppHostBuilder ConfigureServices(Action<IServiceCollection> configureDelegate);
        IAppHostBuilder ConfigureHostServices(Action<IServiceCollection> configureDelegate);
        IAppHostBuilder UseStartup<T>() where T : class, IStartup;
        IAppHost Build();
    }
}
