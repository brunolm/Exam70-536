using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Exam70_536
{
    public static class Performance
    {
        public static TimeSpan Test(string name, decimal times, bool precompile, Action fn)
        {
            if (precompile)
            {
                fn();
            }

            var sw = new Stopwatch();

            sw.Start();

            for (decimal i = 0; i < times; ++i)
            {
                fn();
            }

            sw.Stop();

            return sw.Elapsed;
        }
    }
}
