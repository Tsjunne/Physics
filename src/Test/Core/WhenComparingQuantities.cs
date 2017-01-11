using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Physics.Test.Core
{
    [TestClass]
    public class WhenComparingQuantities : GivenSiSystem
    {
        [TestMethod]
        public void ThenCanCheckEqualityForEquivalentQuantities()
        {
            var hour1 = new Quantity(1, this.h);
            var hour2 = new Quantity(3600, this.s);

            Assert.IsTrue(hour1 == hour2);
            Assert.AreEqual(hour1, hour2);
        }


        [TestMethod]
        public void ThenEqualityOperatorsWorksAsExpected()
        {
            var amount1 = new Quantity(0.1, kg);
            var amount2 = new Quantity((double)1 / 10, kg);

            Assert.IsTrue(amount1 == amount2);
            Assert.IsFalse(amount1 != amount2);

            amount1 = null;

            Assert.IsFalse(amount1 == amount2);
            Assert.IsTrue(amount1 != amount2);

            amount2 = null;

            Assert.IsTrue(amount1 == amount2);
            Assert.IsFalse(amount1 != amount2);

            amount1 = new Quantity(0.1, kg);
            amount2 = amount1;

            Assert.IsTrue(amount1 == amount2);
            Assert.IsFalse(amount1 != amount2);
        }

        [TestMethod]
        public void ThanCanCompareEquivalentQuantities()
        {
            var kWh = this.System.AddDerivedUnit("kWh", "kilowatt hour", UnitPrefix.k * this.W * this.h);
            var energy1 = new Quantity(100, this.J).Convert(kWh);
            var energy2 = new Quantity(101, this.J);

            Assert.IsTrue(energy1 < energy2);
            Assert.IsTrue(energy1 <= energy2);
            Assert.IsFalse(energy1 > energy2);
            Assert.IsFalse(energy1 >= energy2);
            Assert.IsFalse(energy1 == energy2);
        }

        [TestMethod]
        public void ThenComparisonOperatorsWorkAsExpected()
        {
            var amount1 = new Quantity(1, kg);
            var amount2 = new Quantity(2, kg);

            Assert.IsTrue(amount1 < amount2);
            Assert.IsFalse(amount1 > amount2);
            Assert.IsTrue(amount1 <= amount2);
            Assert.IsFalse(amount1 >= amount2);

            amount1 = new Quantity(1, kg);
            amount2 = new Quantity(1, kg);

            Assert.IsFalse(amount1 < amount2);
            Assert.IsFalse(amount1 > amount2);
            Assert.IsTrue(amount1 <= amount2);
            Assert.IsTrue(amount1 >= amount2);

            amount2 = null;

            Assert.IsFalse(amount1 < amount2);
            Assert.IsTrue(amount1 > amount2);
            Assert.IsFalse(amount1 <= amount2);
            Assert.IsTrue(amount1 >= amount2);

            amount1 = null;

            Assert.IsFalse(amount1 < amount2);
            Assert.IsFalse(amount1 > amount2);
            Assert.IsTrue(amount1 <= amount2);
            Assert.IsTrue(amount1 >= amount2);
        }

        [TestMethod]
        public void ThenCompareToWorksAsExpected()
        {
            var amount1 = new Quantity(1, kg);
            var amount2 = new Quantity(2, kg);

            Assert.AreEqual(amount1.CompareTo(amount2), -1);
            Assert.AreEqual(amount2.CompareTo(amount1), 1);

            amount2 = amount1;

            Assert.AreEqual(amount1.CompareTo(amount2), 0);

            amount2 = null;

            Assert.AreEqual(amount1.CompareTo(amount2), 1);
        }
    }
}