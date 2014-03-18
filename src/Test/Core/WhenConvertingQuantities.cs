using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Physics.Test.Core
{
    [TestClass]
    public class WhenConvertingQuantities : GivenSiSystem
    {
        [TestMethod]
        public void ThenConvertedQuantitiesAreEquivalent()
        {
            var kWh = this.System.AddDerivedUnit("kWh", "kilowatt hour", UnitPrefix.k * this.W * this.h);
            var quantity = new Quantity(100, this.J);
            var result = quantity.Convert(kWh);

            Assert.AreEqual(quantity, result);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void ThenConvertingToInequivalentUnitsThrowsException()
        {
            var quantity = new Quantity(100, this.J);
            var result = quantity.Convert(this.W);
        }
    }
}
