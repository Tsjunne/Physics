using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Physics.Systems;

namespace Physics.Test.Performance
{
    /// <summary>
    /// I can conclude from these tests that it is best to define every unit you are ever gonna use, but not more.
    /// If performance is key, don't use the fully predefined SI system. Gain is marginal, but it's something :)
    /// </summary>
    [TestClass]
    public class PerformanceTest 
    {
        [TestMethod]
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

        [TestMethod]
        public void CheckDefineWhatYouNeedSIPerformance()
        {
            var system = UnitSystemFactory.CreateSystem("SI");

            var m = system.AddBaseUnit("m", "metre");
            var kg = system.AddBaseUnit("kg", "kilogram", true);
            var s = system.AddBaseUnit("s", "second");
            var N = system.AddDerivedUnit("N", "newton", kg * m * (s ^ -2));
            var J = system.AddDerivedUnit("J", "joule", N * m);
            var W = system.AddDerivedUnit("W", "watt", J / s);
            var h = system.AddDerivedUnit("h", "hour", 60 * 60 * s);
            
            var kWh = 1000 * W * h;

            RunTest("SimpleSI", () =>
            {
                var m3 = m ^ 3;
                var energy = new Quantity(100, kWh);
                var volume = new Quantity(5, m3);
                var result = energy / volume;
            });
        }

        [TestMethod]
        public void CheckOnlyBaseUnitSIPerformance()
        {
            var system = UnitSystemFactory.CreateSystem("SI");

            var m = system.AddBaseUnit("m", "metre");
            var kg = system.AddBaseUnit("kg", "kilogram", true);
            var s = system.AddBaseUnit("s", "second");

            var kWh = 1000 * kg * (m^2) * (s ^ -2) * 60 * 60;

            RunTest("BaseSI", () =>
            {
                var m3 = m ^ 3;
                var energy = new Quantity(100, kWh);
                var volume = new Quantity(5, m3);
                var result = energy / volume;
            });
        }

        [TestMethod]
        public void CheckOnlyBaseUnitAndIncoherentUnitsSIPerformance()
        {
            var system = UnitSystemFactory.CreateSystem("SI");

            var m = system.AddBaseUnit("m", "metre");
            var kg = system.AddBaseUnit("kg", "kilogram", true);
            var s = system.AddBaseUnit("s", "second");
            var h = system.AddDerivedUnit("h", "hour", 60 * 60 * s);

            var kWh = 1000 * kg * (m ^ 2) * (s ^ -3) * h;

            RunTest("BaseSI", () =>
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
            new Stopwatch();
            timeTaken.Start();

            for (var i = 0; i < 100000; i++)
            {
                testCase();
            }

            Console.WriteLine($"{name} took: {timeTaken.ElapsedMilliseconds}ms");
        }
    }
}