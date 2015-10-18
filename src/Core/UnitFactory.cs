namespace Physics
{
    internal class UnitFactory : IUnitFactory
    {
        public KnownUnit CreateUnit(IUnitSystem system, double factor, Dimension dimension, string symbol, string name,
            bool inherentPrefix)
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
    }
}