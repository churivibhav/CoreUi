using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Vhc.CoreUi.Abstractions;

namespace Vhc.CoreUi.AwsLambda
{
    public interface ILambdaHost : IAppHost
    {
        ILambdaContext Context { get; }
    }
}
