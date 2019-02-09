using Xunit;

namespace Physics.Test.Core
{
    public class WhenComparingDimensions
    {
        [Fact]
        public void ThenExponentsDetermineEquality()
        {
            var dim1 = new Dimension(1, 2, 3);
            var dim2 = new Dimension(1, 2, 3);
            var dim3 = new Dimension(3, 2, 1);

            Assert.Equal(dim1, dim2);
            Assert.Equal(dim1.GetHashCode(), dim1.GetHashCode());
            Assert.NotEqual(dim1, dim3);
            Assert.NotEqual(dim1.GetHashCode(), dim3.GetHashCode());
            Assert.NotEqual(dim2, dim3);
            Assert.NotEqual(dim2.GetHashCode(), dim3.GetHashCode());

            var dim4 = new Dimension(1);
            var dim5 = new Dimension(1);

            Assert.Equal(dim4, dim5);
            Assert.Equal(dim4.GetHashCode(), dim5.GetHashCode());

            var dim6 = new Dimension();
            var dim7 = new Dimension();

            Assert.Equal(dim6, dim7);
            Assert.Equal(dim6.GetHashCode(), dim7.GetHashCode());
        }

        [Fact]
        public void ThenTrailingZerosAreIgnored()
        {
            var dim1 = new Dimension(1, 2, 3, 0, 0);
            var dim2 = new Dimension(1, 2, 3);

            Assert.Equal(dim1, dim2);
            Assert.Equal(dim1.GetHashCode(), dim2.GetHashCode());
        }

        [Fact]
        public void ThenStartingZerosAreNotIgnored()
        {
            var dim1 = new Dimension(0, 0, 1);
            var dim2 = new Dimension(1);

            Assert.NotEqual(dim1, dim2);
        }

        [Fact]
        public void ThenAnEmptyListOfExponentsIsDimensionLess()
        {
            Assert.Equal(new Dimension(), Dimension.DimensionLess);
        }

        [Fact]
        public void ThenOnlyZeroExponentsIsDimensionLess()
        {
            Assert.Equal(new Dimension(0, 0), Dimension.DimensionLess);
        }
    }
}