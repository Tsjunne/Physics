using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Physics.Test.Core
{
    [TestClass]
    public class WhenComparingDimensions
    {
        [TestMethod]
        public void ThenExponentsDetermineEquality()
        {
            var dim1 = new Dimension(1, 2, 3);
            var dim2 = new Dimension(1, 2, 3);
            var dim3 = new Dimension(3, 2, 1);

            Assert.AreEqual(dim1, dim2);
            Assert.AreEqual(dim1.GetHashCode(), dim1.GetHashCode());
            Assert.AreNotEqual(dim1, dim3);
            Assert.AreNotEqual(dim1.GetHashCode(), dim3.GetHashCode());
            Assert.AreNotEqual(dim2, dim3);
            Assert.AreNotEqual(dim2.GetHashCode(), dim3.GetHashCode());

            var dim4 = new Dimension(1);
            var dim5 = new Dimension(1);

            Assert.AreEqual(dim4, dim5);
            Assert.AreEqual(dim4.GetHashCode(), dim5.GetHashCode());

            var dim6 = new Dimension();
            var dim7 = new Dimension();

            Assert.AreEqual(dim6, dim7);
            Assert.AreEqual(dim6.GetHashCode(), dim7.GetHashCode());
        }

        [TestMethod]
        public void ThenTrailingZerosAreIgnored()
        {
            var dim1 = new Dimension(1, 2, 3, 0, 0);
            var dim2 = new Dimension(1, 2, 3);

            Assert.AreEqual(dim1, dim2);
            Assert.AreEqual(dim1.GetHashCode(), dim2.GetHashCode());
        }

        [TestMethod]
        public void ThenStartingZerosAreNotIgnored()
        {
            var dim1 = new Dimension(0, 0, 1);
            var dim2 = new Dimension(1);

            Assert.AreNotEqual(dim1, dim2);
        }

        [TestMethod]
        public void ThenAnEmptyListOfExponentsIsDimensionLess()
        {
            Assert.AreEqual(new Dimension(), Dimension.DimensionLess);
        }
    }
}