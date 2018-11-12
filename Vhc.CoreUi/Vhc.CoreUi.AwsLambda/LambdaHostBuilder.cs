using Amazon.Lambda.Core;
using System;
using Vhc.CoreUi;
using Vhc.CoreUi.Abstractions;

namespace Vhc.CoreUi.AwsLambda
{
    public class LambdaHostBuilder : AppHostBuilder
    {
        private ILambdaContext _context;

        public LambdaHostBuilder UseContext(ILambdaContext context)
        {
            _context = context;
            return this;
        }

        public override IAppHost Build() => Build((services, hostingProvider) => new LambdaHost(services, hostingProvider, _config, _arguments, _context));
    }
}
