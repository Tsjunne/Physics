using Physics;
using Physics.Systems;
using System;
using System.Diagnostics;
using Xunit;

namespace PackageTest
{
    public class WhenUsingPackage
    {
        [Fact]
        public void CheckFullyDefinedSIPerformance()
        {
            var sytem = SI.System;

            var W = sytem["W"];
            var h = sytem["h"];
            var m = sytem["m"];

            var kWh = 1000 * W * h;

            RunTest("FullSI", () =>
            {
                var m3 = m ^ 3;
                var energy = new Quantity(100, kWh);
                var volume = new Quantity(5, m3);
                var result = energy / volume;
            });
        }

        private void RunTest(string name, Action testCase)
        {
            var timeTaken = new Stopwatch();

            timeTaken.Start();

            for (var i = 0; i < 100000; i++)
            {
                testCase();
            }

            Console.WriteLine($"{name} took: {timeTaken.ElapsedMilliseconds}ms");
        }
    }
}
