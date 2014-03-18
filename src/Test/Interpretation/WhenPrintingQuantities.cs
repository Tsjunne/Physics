using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Physics.Test.Presentation
{
    [TestClass]
    public class WhenPrintingQuantities : GivenSiSystem
    {
        [TestMethod]
        public void ThenQuantityCanBePrintedInDesiredUnitAndFormat()
        {
            var quantity = new Quantity(10 * 1000 * 1000, this.J);

            var kWh = this.System.AddDerivedUnit("kWh", "kilowatt hour", UnitPrefix.k * this.W * this.h);
            string display;

            display = quantity.ToString();
            Assert.AreEqual("10000000 J", display);

            display = quantity.ToString(kWh);
            Assert.AreEqual("2.77777777777778 kWh", display);

            display = quantity.ToString("N3", kWh);
            Assert.AreEqual("2.778 kWh", display);
        }

        [TestMethod]
        public void ThenKilogramBehavesCorrectly()
        {
            var mg = this.System.AddDerivedUnit("mg", "milligram", this.kg / 1000000000);
            var t = this.System.AddDerivedUnit("t", "tonne", 1000 * this.kg);

            var mass = new Quantity(10000, this.kg);

            Assert.AreEqual(mass, mass.Convert(mg));
            Assert.AreEqual(mass, mass.Convert(t));

            Assert.AreEqual("10000000000000 mg", mass.ToString(mg));
            Assert.AreEqual("10 t", mass.ToString(t));
        }
    }
}
