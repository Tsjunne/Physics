using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Physics.Test.Presentation
{
    [TestClass]
    public class WhenPrintingUnits : GivenSiSystem
    {
        [TestMethod]
        public void ThenUnitCanBePrintedAndReparsed()
        {
            var expected = this.kg*this.m*(this.s ^ -2);
            var display = this.System.Display(expected);
            var result = this.System.Parse(display);

            Assert.AreEqual(expected, result);
            Assert.AreEqual("m kg / s^2", display);
        }
    }
}