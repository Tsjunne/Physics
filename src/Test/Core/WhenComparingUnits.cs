using Xunit;
using NSubstitute;

namespace Physics.Test.Core
{
    public class WhenComparingUnits
    {
        [Fact]
        public void ThenUnitsWithSameDimensionAndFactorAreEqual()
        {
            var system = Substitute.For<IUnitSystem>();
            
            var unit1 = new KnownUnit(system, 1, new Dimension(1), "x", "x");
            var unit2 = new KnownUnit(system, 1, new Dimension(1), "x", "x");
            var unit3 = new KnownUnit(system, 3, new Dimension(0, 0, 1), "x", "x");

            Assert.Equal(unit1, unit2);
            Assert.NotEqual(unit2, unit3);
        }
    }
}