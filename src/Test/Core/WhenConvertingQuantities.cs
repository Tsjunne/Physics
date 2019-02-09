using System;
using Xunit;

namespace Physics.Test.Core
{
    public class WhenConvertingQuantities : GivenSiSystem
    {
        [Fact]
        public void ThenConvertedQuantitiesAreEquivalent()
        {
            var kWh = System.AddDerivedUnit("kWh", "kilowatt hour", UnitPrefix.k*W*h);
            var quantity = new Quantity(100, J);
            var result = quantity.Convert(kWh);

            Assert.Equal(quantity, result);
        }

        [Fact]
        public void ThenConvertingToInequivalentUnitsThrowsException()
        {
            var quantity = new Quantity(100, J);
            Assert.Throws<InvalidOperationException>(() => quantity.Convert(W));
        }
    }
}