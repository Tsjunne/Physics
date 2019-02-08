using System;
using Xunit;

namespace Physics.Test.Core
{
    public class WhenComparingQuantities : GivenSiSystem
    {
        [Fact]
        public void ThenCanCheckEqualityForEquivalentQuantities()
        {
            var hour1 = new Quantity(1, h);
            var hour2 = new Quantity(3600, s);

            Assert.True(hour1 == hour2);
            Assert.Equal(hour1, hour2);
        }


        [Fact]
        public void ThenEqualityOperatorsWorksAsExpected()
        {
            var amount1 = new Quantity(0.1, kg);
            var amount2 = new Quantity((double)1 / 10, kg);

            Assert.True(amount1 == amount2);
            Assert.False(amount1 != amount2);

            amount1 = null;

            Assert.False(amount1 == amount2);
            Assert.True(amount1 != amount2);

            amount2 = null;

            Assert.True(amount1 == amount2);
            Assert.False(amount1 != amount2);

            amount1 = new Quantity(0.1, kg);
            amount2 = amount1;

            Assert.True(amount1 == amount2);
            Assert.False(amount1 != amount2);
        }

        [Fact]
        public void ThenCanCompareEquivalentQuantities()
        {
            var kWh = System.AddDerivedUnit("kWh", "kilowatt hour", UnitPrefix.k * W * h);
            var energy1 = new Quantity(100, J).Convert(kWh);
            var energy2 = new Quantity(101, J);

            Assert.True(energy1 < energy2);
            Assert.True(energy1 <= energy2);
            Assert.False(energy1 > energy2);
            Assert.False(energy1 >= energy2);
            Assert.False(energy1 == energy2);
        }

        [Fact]
        public void ThenComparisonOperatorsWorkAsExpected()
        {
            var amount1 = new Quantity(1, kg);
            var amount2 = new Quantity(2, kg);

            Assert.True(amount1 < amount2);
            Assert.False(amount1 > amount2);
            Assert.True(amount1 <= amount2);
            Assert.False(amount1 >= amount2);

            amount1 = new Quantity(1, kg);
            amount2 = new Quantity(1, kg);

            Assert.False(amount1 < amount2);
            Assert.False(amount1 > amount2);
            Assert.True(amount1 <= amount2);
            Assert.True(amount1 >= amount2);

            amount2 = null;

            Assert.False(amount1 < amount2);
            Assert.True(amount1 > amount2);
            Assert.False(amount1 <= amount2);
            Assert.True(amount1 >= amount2);

            amount1 = null;

            Assert.False(amount1 < amount2);
            Assert.False(amount1 > amount2);
            Assert.True(amount1 <= amount2);
            Assert.True(amount1 >= amount2);
        }

        [Fact]
        public void ThenCompareToWorksAsExpected()
        {
            var amount1 = new Quantity(1, kg);
            var amount2 = new Quantity(2, kg);

            Assert.Equal(-1 ,amount1.CompareTo(amount2));
            Assert.Equal(1, amount2.CompareTo(amount1));

            amount2 = amount1;

            Assert.Equal(0, amount1.CompareTo(amount2));

            amount2 = null;

            Assert.Equal(1, amount1.CompareTo(amount2));
        }
    }
}