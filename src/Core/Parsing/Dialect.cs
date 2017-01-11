namespace Physics.Parsing
{
    internal class Dialect : IDialect
    {
        public Token Division => new Token("/");
        public Token Multiplication => new Token(" ", "·", "×");
        public Token Exponentiation => new Token("^");
    }
}