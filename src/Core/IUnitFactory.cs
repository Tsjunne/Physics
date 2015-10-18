namespace Physics
{
    internal interface IUnitFactory
    {
        KnownUnit CreateUnit(IUnitSystem system, double factor, Dimension dimension, string symbol, string name,
            bool inherentPrefix);

        Unit CreateUnit(IUnitSystem system, double factor, Dimension dimension);
    }
}