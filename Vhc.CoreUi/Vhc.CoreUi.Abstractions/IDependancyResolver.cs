using System;
using System.Collections.Generic;
using System.Text;

namespace Vhc.CoreUi.Abstractions
{
    public interface IDependancyResolver
    {
        IServiceProvider Services { get; }
    }
}
