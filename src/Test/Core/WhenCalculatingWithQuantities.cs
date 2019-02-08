using Xunit;

namespace Physics.Test.Core
{
    public class WhenCalculatingWithQuantities : GivenSiSystem
    {
        [Fact]
        public void ThenCanDivideAndMultipy()
        {
            var kWh = UnitPrefix.k*W*h;
            var m3 = m ^ 3;

            // 100 kWh
            var energy = new Quantity(100, kWh);

            // 5 m³
            var volume = new Quantity(5, m3);

            // 20 kWh/m³
            var result = energy/volume;
            var expected = new Quantity(20, kWh/m3);

            Assert.Equal(result, expected);
        }

        [Fact]
        public void ThenKilogramBehavesCorrectly()
        {
            var mass100 = new Quantity(100, kg);
            var mass25 = new Quantity(25, kg);

            Assert.Equal(mass100/4, mass25);
        }

        [Fact]
        public void ThenCanAddQuantities()
        {
            var kWh = UnitPrefix.k * W * h;
            
            var energy1 = new Quantity(100, kWh);
            var energy2 = new Quantity(50, kWh).Convert(J);

            var expected = new Quantity(150, kWh);
            var actual = energy1 + energy2;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ThenCanSubstractQuantities()
        {
            var kWh = UnitPrefix.k * W * h;

            var energy1 = new Quantity(100, kWh);
            var energy2 = new Quantity(20, kWh).Convert(J);

            var expected = new Quantity(80, kWh);
            var actual = energy1 - energy2;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ThenCanExponentizeQuantities()
        {
            var length = new Quantity(4, m);
            var square = m*m;

            var expected = new Quantity(16, square);
            var actual = length ^ 2;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ThenDividingQuantityOfSameUnitYieldsARatio()
        {
            var a = new Quantity(10, m);
            var b = new Quantity(2, m);

            var expected = new Quantity(5, System.NoUnit);
            var actual = a/b;

            Assert.Equal(expected, actual);
        }
    }
}