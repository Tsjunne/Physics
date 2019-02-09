using Xunit;
using System.Globalization;

namespace Physics.Test.Presentation
{
    public class WhenPrintingQuantities : GivenSiSystem
    {
        public WhenPrintingQuantities()
            :base()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        }

        [Fact]
        public void ThenQuantityCanBePrintedInDesiredUnitAndFormat()
        {
            var quantity = new Quantity(10*1000*1000, J);

            var kWh = System.AddDerivedUnit("kWh", "kilowatt hour", UnitPrefix.k*W*h);
            string display;

            display = quantity.ToString();
            Assert.Equal("10000000 J", display);

            display = quantity.ToString(kWh);
            Assert.Equal("2.77777777777778 kWh", display);

            display = quantity.ToString("N3", kWh);
            Assert.Equal("2.778 kWh", display);
        }

        [Fact]
        public void ThenKilogramBehavesCorrectly()
        {
            var mg = System.AddDerivedUnit("mg", "milligram", kg/(1000*1000));
            var t = System.AddDerivedUnit("t", "tonne", 1000*kg);

            var mass = new Quantity(10*1000, kg);

            Assert.Equal(mass, mass.Convert(mg));
            Assert.Equal(mass, mass.Convert(t));

            Assert.Equal("10000000000 mg", mass.ToString(mg));
            Assert.Equal("10 t", mass.ToString(t));
        }
    }
}