using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Physics;
using System.Linq;

namespace Physics.Test.Core
{
    [TestClass]
    public class WhenCalculatingWithUnits : GivenSiSystem
    {
        [TestMethod]
        public void ThenUnitsCanBeMultiplied()
        {
            var result = this.m * this.s;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Dimension.Count(), Math.Max(this.m.Dimension.Count(), this.s.Dimension.Count()));
        }

        [TestMethod]
        public void ThenUnitsCanBeDivided()
        {
            var result = this.m / this.s;
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Dimension.Count(), Math.Max(this.m.Dimension.Count(), this.s.Dimension.Count()));
        }

        [TestMethod]
        public void ThenMultipicationCanBeRevertedWithDivision()
        {
            var mPerS = this.m / this.s;
            var m = mPerS * this.s;

            Assert.AreEqual(m, this.m);
        }

        [TestMethod]
        public void ThenKnownUnitsAreReturnedWhenPossible()
        {
            var result = this.kg * this.m * (this.s ^ -2);

            Assert.IsTrue(ReferenceEquals(result, this.N));
        }
    }
}
