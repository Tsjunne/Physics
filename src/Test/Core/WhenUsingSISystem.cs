using System.Linq;
using Xunit;

namespace Physics.Test.Core
{
    public class WhenUsingSISystem : GivenSiSystem
    {
        [Fact]
        public void ThenAll7BaseUnitsAreDefined()
        {
            Assert.Equal(7, System.BaseUnits.Count());
            Assert.NotNull(m);
            Assert.NotNull(kg);
            Assert.NotNull(s);
            Assert.NotNull(A);
            Assert.NotNull(K);
            Assert.NotNull(mol);
            Assert.NotNull(cd);
        }

        [Fact]
        public void ThenSomeDerivedUnitsAreKnown()
        {
            Assert.NotNull(Hz);
            Assert.NotNull(N);
            Assert.NotNull(Pa);
            Assert.NotNull(J);
            Assert.NotNull(W);
            Assert.NotNull(C);
            Assert.NotNull(V);
            Assert.NotNull(F);
            Assert.NotNull(Ω);
            Assert.NotNull(S);
            Assert.NotNull(Wb);
            Assert.NotNull(T);
            Assert.NotNull(H);
            Assert.NotNull(lx);
            Assert.NotNull(Sv);
            Assert.NotNull(kat);
        }
    }
}