using System.Linq;
using Xunit;
using NSubstitute;

namespace Physics.Test.Core
{
    public class WhenCreatingBaseUnit
    {
        private Unit _baseUnit;
        private UnitFactory _subject;

        public WhenCreatingBaseUnit()
        {
            _subject = new UnitFactory();
        }
        
        [Fact]
        public void ThenNoTrailingEmptyDimensionsAreCreated()
        {
            _baseUnit = _subject.CreateUnit(Substitute.For<IUnitSystem>(), 1, new Dimension(0, 1), "x", "x", false);
            Assert.Equal(2, _baseUnit.Dimension.Count());
        }
    }
}