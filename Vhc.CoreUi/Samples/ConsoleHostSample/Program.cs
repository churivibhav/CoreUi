using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vhc.CoreUi;

namespace ConsoleHostSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new AppHostBuilder()
                .WithArguments(args)
                .Build();
            host.Run(app =>
            {
                app.Arguments.ToList().ForEach(a => Console.WriteLine(a));
                Console.ReadKey();
            });
        }
    }
}
