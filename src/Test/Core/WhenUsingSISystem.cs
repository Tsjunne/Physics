using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Physics.Test.Core
{
    [TestClass]
    public class WhenUsingSISystem : GivenSiSystem
    {
        [TestMethod]
        public void ThenAll7BaseUnitsAreDefined()
        {
            Assert.AreEqual(this.System.BaseUnits.Count(), 7);
            Assert.IsNotNull(this.m);
            Assert.IsNotNull(this.kg);
            Assert.IsNotNull(this.s);
            Assert.IsNotNull(this.A);
            Assert.IsNotNull(this.K);
            Assert.IsNotNull(this.mol);
            Assert.IsNotNull(this.cd);
        }

        [TestMethod]
        public void ThenSomeDerivedUnitsAreKnown()
        {
            Assert.IsNotNull(this.Hz);
            Assert.IsNotNull(this.N);
            Assert.IsNotNull(this.Pa);
            Assert.IsNotNull(this.J);
            Assert.IsNotNull(this.W);
            Assert.IsNotNull(this.C);
            Assert.IsNotNull(this.V);
            Assert.IsNotNull(this.F);
            Assert.IsNotNull(this.Ω);
            Assert.IsNotNull(this.S);
            Assert.IsNotNull(this.Wb);
            Assert.IsNotNull(this.T);
            Assert.IsNotNull(this.H);
            Assert.IsNotNull(this.lx);
            Assert.IsNotNull(this.Sv);
            Assert.IsNotNull(this.kat);
        }
    }
}