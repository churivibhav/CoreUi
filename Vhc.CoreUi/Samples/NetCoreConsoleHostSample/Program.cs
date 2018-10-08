using System;
using System.Threading.Tasks;
using Vhc.CoreUi;

namespace NetCoreConsoleHostSample
{
    class Program
    {
        static void Main(string[] args)
        {
            new AppHostBuilder()
                .Build()
                .RunAsync(async (app, token) =>
                {
                    if(!token.IsCancellationRequested)
                        await Task.Run(() => Console.WriteLine("aaa"));
                })
                .GetAwaiter()
                .GetResult();
            Console.ReadKey();
        }
    }
}
