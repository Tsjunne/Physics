namespace Physics
{
    public static class UnitSystemFactory
    {
        public static IUnitSystem CreateSystem(string name)
        {
            return new UnitSystem(name);
        }
    }
}