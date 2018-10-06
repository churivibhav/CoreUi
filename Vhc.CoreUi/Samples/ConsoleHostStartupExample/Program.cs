using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vhc.CoreUi;

namespace ConsoleHostStartupExample
{
    class Program
    {
        static void Main(string[] args)
        {
            new AppHostBuilder()
                .UseStartup<Startup>()
                .Build()
                .Run();
        }
    }
}
