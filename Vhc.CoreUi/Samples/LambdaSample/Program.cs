using Amazon.Lambda.TestUtilities;
using System;
using Vhc.CoreUi.AwsLambda;

namespace LambdaSample
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new TestLambdaContext();
            new LambdaHostBuilder()
                .UseContext(context)
                .UseStartup<Startup>()
                .Build()
                .Run();
        }
    }
}
