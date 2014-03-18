using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    internal class UnitFactory : IUnitFactory
    {
        public KnownUnit CreateUnit(IUnitSystem system, double factor, string symbol, string name, bool inherentPrefix)
        {
            var unit = CreateUnit(system, factor, CreateDimension(system.BaseUnits.Count()), symbol, name, inherentPrefix);

            return unit;
        }

        public KnownUnit CreateUnit(IUnitSystem system, double factor, Dimension dimension, string symbol, string name, bool inherentPrefix)
        {
            var unit = new KnownUnit(system, factor, dimension, symbol, name, inherentPrefix);

            return unit;
        }

        public Unit CreateUnit(IUnitSystem system, double factor, Dimension dimension)
        {
            Check.SystemKnowsDimension(system, dimension);

            var unit = new DerivedUnit(system, factor, dimension);

            return unit;
        }

        private static Dimension CreateDimension(int index)
        {
            var exponents = new int[index + 1];
            exponents[index] = 1;

            return Dimension.Create(exponents);
        }
    }
}
