using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vhc.CoreUi;
using Microsoft.Extensions.DependencyInjection;
using Vhc.CoreUi.Abstractions;

namespace ConsoleHostSample
{
    class Program
    {
        static void SimpleMain(string[] args) =>
            new AppHostBuilder().Build().Run(app => Console.WriteLine("Hello World!"));

        static void Main(string[] args)
        {
            IAppHost host = new AppHostBuilder()
                .UseArguments(args)
                .ConfigureServices(s => s.AddSingleton<IDemo, Demo>() )
                .Build();

            host.Run(app =>
            {
                var demo = app.Services.GetRequiredService<IDemo>();
                app.Arguments.ToList().ForEach(arg => Console.WriteLine(arg));
                Console.ReadKey();
            });
        }
    }
}
