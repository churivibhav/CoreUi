using System;
using System.Collections.Generic;

namespace Vhc.CoreUi.Abstractions
{
    public interface IAppHost
    {
        ICollection<string> Arguments { get; }
        IServiceProvider Services { get; }
        void Run();
        void Run(Action<IAppHost> action);
    }
}