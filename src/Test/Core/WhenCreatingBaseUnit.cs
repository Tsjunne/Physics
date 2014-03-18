using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Physics;
using FakeItEasy;

namespace Physics.Test.Core
{
    [TestClass]
    public class WhenCreatingBaseUnit : GivenWhenThen
    {
        private UnitFactory subject;
        private Unit baseUnit;

        public override void Given()
        {
            subject = new UnitFactory();
        }

        public override void When()
        {
            baseUnit = subject.CreateUnit(A.Fake<IUnitSystem>(), 1, Dimension.Create(0, 1), "x", "x", false);
        }

        [TestMethod]
        public void ThenNoTrailingEmptyDimensionsAreCreated()
        {
            Assert.AreEqual(baseUnit.Dimension.Count(), 2);
        }
    }
}
