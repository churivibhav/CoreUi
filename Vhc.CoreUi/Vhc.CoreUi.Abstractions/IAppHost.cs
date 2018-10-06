using System;

namespace Vhc.CoreUi.Abstractions
{
    public interface IAppHost
    {
        IServiceProvider Services { get; }
        void Run();
        void Run(Action<IAppHost> action);
    }
}