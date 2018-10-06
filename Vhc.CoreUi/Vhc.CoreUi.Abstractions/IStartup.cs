﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vhc.CoreUi.Abstractions
{
    public interface IStartup
    {
        void ConfigureServices(IServiceCollection services);

        void Configure(IConfigurationBuilder config);

        /// <summary>
        /// The starting point of the Windows Application
        /// </summary>
        /// <param name="app"></param>
        void Start(IAppHost app);
    }
}