using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Physics;

namespace Physics.Test.Presentation
{
    [TestClass]
    public class WhenParsingUnits : GivenSiSystem
    {
        [TestMethod]
        public void ThenUnitsWithOutDenominatorCanBeParsed()
        {
            var result = this.System.Parse("kW h");
            var expected = UnitPrefix.k * this.W * this.h;
            Assert.AreEqual(result, expected);

            result = this.System.Parse("kW×h");
            Assert.AreEqual(result, expected);

            result = this.System.Parse("kW × h");
            Assert.AreEqual(result, expected);

            result = this.System.Parse("kW·h");
            Assert.AreEqual(result, expected);

            result = this.System.Parse("kW · h");
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void ThenUnitsWithDenominatorCanBeParsed()
        {
            var result = this.System.Parse("m/s");
            var expected = this.m / this.s;
            Assert.AreEqual(result, expected);

            result = this.System.Parse("m / s");
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void ThenUnitsWithExponentCanBeParsed()
        {
            var result = this.System.Parse("J/m^3");
            var expected = this.J / (this.m ^ 3);
            Assert.AreEqual(result, expected);

            result = this.System.Parse("J / m^3");
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void ThenUnitsWithNegativeExponentCanBeParsed()
        {
            var result = this.System.Parse("J m^-3");
            var expected = this.J * (this.m ^ -3);

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void ThenMilligramsAreParsedCorrectly()
        {
            var result = this.System.Parse("mg");
            var expected = this.kg / 1000000;

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void ThenGramsAreParsedCorrectly()
        {
            var result = this.System.Parse("g");
            var expected = this.kg / 1000;

            Assert.AreEqual(result, expected);
        }

        [TestMethod, ExpectedException(typeof(FormatException))]
        public void ThenKiloKilogramsThrowsAFormatException()
        {
            var result = this.System.Parse("kkg");
        }
    }
}
