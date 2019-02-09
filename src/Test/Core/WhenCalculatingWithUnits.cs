using System;
using System.Linq;
using Xunit;

namespace Physics.Test.Core
{
    public class WhenCalculatingWithUnits : GivenSiSystem
    {
        [Fact]
        public void ThenUnitsCanBeMultiplied()
        {
            var result = m*s;
            Assert.NotNull(result);
            Assert.Equal(result.Dimension.Count(), Math.Max(m.Dimension.Count(), s.Dimension.Count()));
        }

        [Fact]
        public void ThenUnitsCanBeDivided()
        {
            var result = m/s;
            Assert.NotNull(result);
            Assert.Equal(result.Dimension.Count(), Math.Max(m.Dimension.Count(), s.Dimension.Count()));
        }

        [Fact]
        public void ThenMultipicationCanBeRevertedWithDivision()
        {
            var mPerS = this.m/s;
            var m = mPerS*s;

            Assert.Equal(m, this.m);
        }

        [Fact]
        public void ThenKnownUnitsAreReturnedWhenPossible()
        {
            var result = kg*m*(s ^ -2);

            Assert.True(ReferenceEquals(result, N));
        }
    }
}