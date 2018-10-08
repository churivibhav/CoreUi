using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vhc.CoreUi.Abstractions
{
    public interface IAppHost
    {
        ICollection<string> Arguments { get; }
        IServiceProvider Services { get; }
        void Run();
        void Run(Action<IAppHost> action);
        Task RunAsync(Func<IAppHost, CancellationToken, Task> asyncFunction);
    }
}