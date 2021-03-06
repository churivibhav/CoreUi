﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vhc.CoreUi.Abstractions;

namespace ConsoleHostStartupExample
{
    class Startup : IStartup
    {
        public void Configure(IConfigurationBuilder config)
        {
            config.AddJsonFile("appsettings.json", optional: true);
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            var connStr = config.GetConnectionString("DefaultConnection");
            services.AddTransient<Foo>();
        }

        public void Start(IAppHost app)
        {
            var foo = app.Services.GetService<Foo>();
            Console.WriteLine(foo.ToString());
        }
    }
}
