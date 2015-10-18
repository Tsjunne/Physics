namespace Physics.Presentation
{
    internal class UnitDialect : IUnitDialect
    {
        public Token Division => new Token("/");
        public Token Multiplication => new Token(" ", "·", "×");
        public Token Exponentiation => new Token("^");
    }
}