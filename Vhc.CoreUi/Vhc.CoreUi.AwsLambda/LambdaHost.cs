using System;
using System.Collections.Generic;
using System.Text;
using Amazon.Lambda.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Vhc.CoreUi.AwsLambda
{
    class LambdaHost : AppHost, ILambdaHost
    {
        private ILambdaContext _context;

        public ILambdaContext Context => _context;

        public LambdaHost(IServiceCollection appServices, IServiceProvider hostingProvider, IConfiguration configuration, ICollection<string> arguments, ILambdaContext context)
            : base(appServices, hostingProvider, configuration, arguments)
        {
            _context = context;
        }
    }
}
