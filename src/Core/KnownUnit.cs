using System;

namespace Physics
{
    public sealed class KnownUnit : Unit
    {
        internal KnownUnit(IUnitSystem system, double factor, Dimension dimension, string symbol, string name)
            : this(system, factor, dimension, symbol, name, false)
        {
            this.Symbol = symbol;
        }

        internal KnownUnit(IUnitSystem system, double factor, Dimension dimension, string symbol, string name,
            bool inherentPrefix)
            : base(system, factor, dimension)
        {
            Check.Argument(symbol, nameof(symbol)).IsNotNull();
            Check.Argument(name, nameof(name)).IsNotNull();

            this.Symbol = symbol;
            this.Name = name;

            if (inherentPrefix)
            {
                this.DeriveInherentFactorAndBaseSymbol(symbol);
            }
            else
            {
                this.InherentFactor = 1;
                this.BaseSymbol = symbol;
            }
        }

        public string Symbol { get; }
        public string Name { get; private set; }
        public double InherentFactor { get; private set; }
        public string BaseSymbol { get; private set; }

        public override string ToString()
        {
            return this.Symbol;
        }

        private void DeriveInherentFactorAndBaseSymbol(string symbol)
        {
            UnitPrefix inherentPrefix;
            var firstCharacter = symbol.Substring(0, 1);
            var baseSymbol = symbol.Substring(1);

            if (string.IsNullOrEmpty(baseSymbol) || !UnitPrefix.Prefixes.TryGetValue(firstCharacter, out inherentPrefix))
            {
                throw new ArgumentException(Messages.InherentPrefixInvalid.FormatWith(symbol));
            }

            this.InherentFactor = inherentPrefix.Factor;
            this.BaseSymbol = baseSymbol;
        }
    }
}