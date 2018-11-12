using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Vhc.CoreUi.Abstractions;
using Vhc.CoreUi.AwsLambda;

namespace LambdaSample
{
    internal class Startup : IStartup
    {
        public void Configure(IConfigurationBuilder config)
        {
            
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            
        }

        public void Start(IAppHost app)
        {
            var fnName = (app as ILambdaHost).Context.FunctionName;
            Console.WriteLine(fnName);
        }
    }
}