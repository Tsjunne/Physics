using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Physics.Test.Core
{
    [TestClass]
    public class WhenComparingQuantities : GivenSiSystem
    {
        [TestMethod]
        public void ThenCanCompareEquivalentQuantities()
        {
            var hour1 = new Quantity(1, this.h);
            var hour2 = new Quantity(3600, this.s);

            Assert.AreEqual(hour1, hour2);
        }
    }
}