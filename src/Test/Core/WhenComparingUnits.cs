using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics.Test.Core
{
    [TestClass]
    public class WhenComparingUnits : GivenWhenThen
    {
        [TestMethod]
        public void ThenUnitsWithSameDimensionAndFactorAreEqual()
        {
            var system = A.Fake<IUnitSystem>();

            var unit1 = new KnownUnit(system, 1, Dimension.Create(1), "x", "x");
            var unit2 = new KnownUnit(system, 1, Dimension.Create(1), "x", "x");
            var unit3 = new KnownUnit(system, 3, Dimension.Create(0, 0, 1), "x", "x");

            Assert.AreEqual(unit1, unit2);
            Assert.AreNotEqual(unit2, unit3);
        }
    }
}
