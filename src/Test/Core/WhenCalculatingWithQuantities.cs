using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Physics;

namespace Physics.Test.Core
{
    [TestClass]
    public class WhenCalculatingWithQuantities : GivenSiSystem
    {
        [TestMethod]
        public void ThenCanDivideAndMultipy()
        {
            var kWh = UnitPrefix.k * W * h;
            var m3 = m ^ 3;

            // 100 kWh
            var energy = new Quantity(100, kWh);

            // 5 m³
            var volume = new Quantity(5, m3);

            // 20 kWh/m³
            var result = energy / volume;
            var expected = new Quantity(20, kWh / m3);

            Assert.AreEqual(result, expected);
        }
        
        [TestMethod]
        public void ThenKilogramBehavesCorrectly()
        {
            var mass100 = new Quantity(100, kg);
            var mass25 = new Quantity(25, kg);

            Assert.AreEqual(mass100 / 4, mass25);
        }
    }
}
