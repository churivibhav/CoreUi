using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleHostSample
{
    interface IDemo
    {
        string Value { get; }
    }

    class Demo : IDemo
    {
        public string Value => "Demo Value";
    }
}
