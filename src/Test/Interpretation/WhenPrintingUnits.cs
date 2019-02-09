using Xunit;

namespace Physics.Test.Presentation
{
    public class WhenPrintingUnits : GivenSiSystem
    {
        [Fact]
        public void ThenUnitCanBePrintedAndReparsed()
        {
            var expected = kg*m*(s ^ -2);
            var display = System.Display(expected);
            var result = System.Parse(display);

            Assert.Equal(expected, result);
            Assert.Equal("m kg / s^2", display);
        }
    }
}