using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Physics;

namespace Physics.Test.Core
{
    [TestClass]
    public class WhenComparingDimensions
    {
        [TestMethod]
        public void ThenExponentsDetermineEquality()
        {
            var dim1 = Dimension.Create(1, 2, 3);
            var dim2 = Dimension.Create(1, 2, 3);
            var dim3 = Dimension.Create(3, 2, 1);

            Assert.AreEqual(dim1, dim2);
            Assert.AreEqual(dim1.GetHashCode(), dim1.GetHashCode());
            Assert.AreNotEqual(dim1, dim3);
            Assert.AreNotEqual(dim1.GetHashCode(), dim3.GetHashCode());
            Assert.AreNotEqual(dim2, dim3);
            Assert.AreNotEqual(dim2.GetHashCode(), dim3.GetHashCode());

            var dim4 = Dimension.Create(1);
            var dim5 = Dimension.Create(1);

            Assert.AreEqual(dim4, dim5);
            Assert.AreEqual(dim4.GetHashCode(), dim5.GetHashCode());

            var dim6 = Dimension.Create();
            var dim7 = Dimension.Create();

            Assert.AreEqual(dim6, dim7);
            Assert.AreEqual(dim6.GetHashCode(), dim7.GetHashCode());
        }

        [TestMethod]
        public void ThenTrailingZerosAreIgnored()
        {
            var dim1 = Dimension.Create(1, 2, 3, 0, 0);
            var dim2 = Dimension.Create(1, 2, 3);

            Assert.AreEqual(dim1, dim2);
            Assert.AreEqual(dim1.GetHashCode(), dim2.GetHashCode());
        }

        [TestMethod]
        public void ThenStartingZerosAreNotIgnored()
        {
            var dim1 = Dimension.Create(0, 0, 1);
            var dim2 = Dimension.Create(1);

            Assert.AreNotEqual(dim1, dim2);
        }

        [TestMethod]
        public void ThenAnEmptyListOfExponentsIsDimensionLess()
        {
            Assert.AreEqual(Dimension.Create(new int[] { }), Dimension.DimensionLess);
        }
    }
}
