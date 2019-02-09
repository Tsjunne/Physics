using System;
using Xunit;

namespace Physics.Test.Presentation
{
    public class WhenParsingUnits : GivenSiSystem
    {
        [Fact]
        public void ThenUnitsWithOutDenominatorCanBeParsed()
        {
            var result = System.Parse("kW h");
            var expected = UnitPrefix.k*W*h;
            Assert.Equal(result, expected);

            result = System.Parse("kW×h");
            Assert.Equal(result, expected);

            result = System.Parse("kW × h");
            Assert.Equal(result, expected);

            result = System.Parse("kW·h");
            Assert.Equal(result, expected);

            result = System.Parse("kW · h");
            Assert.Equal(result, expected);
        }

        [Fact]
        public void ThenUnitsWithDenominatorCanBeParsed()
        {
            var result = System.Parse("m/s");
            var expected = m/s;
            Assert.Equal(result, expected);

            result = System.Parse("m / s");
            Assert.Equal(result, expected);
        }

        [Fact]
        public void ThenUnitsWithExponentCanBeParsed()
        {
            var result = System.Parse("J/m^3");
            var expected = J/(m ^ 3);
            Assert.Equal(result, expected);

            result = System.Parse("J / m^3");
            Assert.Equal(result, expected);
        }

        [Fact]
        public void ThenUnitsWithNegativeExponentCanBeParsed()
        {
            var result = System.Parse("J m^-3");
            var expected = J*(m ^ -3);

            Assert.Equal(result, expected);
        }

        [Fact]
        public void ThenMilligramsAreParsedCorrectly()
        {
            var result = System.Parse("mg");
            var expected = kg/1000000;

            Assert.Equal(result, expected);
        }

        [Fact]
        public void ThenGramsAreParsedCorrectly()
        {
            var result = System.Parse("g");
            var expected = kg/1000;

            Assert.Equal(result, expected);
        }

        [Fact]
        public void ThenKiloKilogramsThrowsAFormatException()
        {
            Assert.Throws<FormatException>(() => System.Parse("kkg"));
        }
    }
}