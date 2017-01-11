using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Physics.Test.Core
{
    [TestClass]
    public class WhenCreatingBaseUnit : GivenWhenThen
    {
        private Unit baseUnit;
        private UnitFactory subject;

        public override void Given()
        {
            subject = new UnitFactory();
        }

        public override void When()
        {
            baseUnit = subject.CreateUnit(Substitute.For<IUnitSystem>(), 1, new Dimension(0, 1), "x", "x", false);
        }

        [TestMethod]
        public void ThenNoTrailingEmptyDimensionsAreCreated()
        {
            Assert.AreEqual(baseUnit.Dimension.Count(), 2);
        }
    }
}