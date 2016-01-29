using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Physics.Test.Performance
{
    [TestClass]
    public class PerformanceTest : GivenSiSystem
    {
        [TestMethod]
        public void CheckPerformance()
        {
            var timeTaken = new Stopwatch();
            timeTaken.Start();

            for (var i = 0; i < 100000; i++)
            {
                var kWh = 1000*W*h;
                var m3 = m ^ 3;

                // 100 kWh
                var energy = new Quantity(100, kWh);

                // 5 m³
                var volume = new Quantity(5, m3);

                // 20 kWh/m³
                var result = energy/volume;
            }

            Console.WriteLine(timeTaken.Elapsed);
        }
    }
}