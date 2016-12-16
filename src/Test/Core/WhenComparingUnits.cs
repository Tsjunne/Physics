using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Physics.Test.Core
{
    [TestClass]
    public class WhenComparingUnits : GivenWhenThen
    {
        [TestMethod]
        public void ThenUnitsWithSameDimensionAndFactorAreEqual()
        {
            var system = Substitute.For<IUnitSystem>();

            var unit1 = new KnownUnit(system, 1, new Dimension(1), "x", "x");
            var unit2 = new KnownUnit(system, 1, new Dimension(1), "x", "x");
            var unit3 = new KnownUnit(system, 3, new Dimension(0, 0, 1), "x", "x");

            Assert.AreEqual(unit1, unit2);
            Assert.AreNotEqual(unit2, unit3);
        }
    }
}