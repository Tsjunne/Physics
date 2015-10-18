namespace Physics
{
    internal sealed class DerivedUnit : Unit
    {
        internal DerivedUnit(IUnitSystem system, double factor, Dimension dimension)
            : base(system, factor, dimension)
        {
        }
    }
}