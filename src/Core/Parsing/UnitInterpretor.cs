using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Physics.Parsing
{
    internal class UnitInterpretor
    {
        private readonly List<KnownUnit> baseUnits;
        private readonly IDialect dialect;
        private readonly Dictionary<string, Unit> parseCache = new Dictionary<string, Unit>();
        private readonly Dictionary<string, KnownUnit> prefixedUnits = new Dictionary<string, KnownUnit>();
        private readonly IUnitSystem system;
        private readonly string unitRegex;

        public UnitInterpretor(IUnitSystem system, IDialect dialect)
        {
            this.system = system;
            this.dialect = dialect;
            this.unitRegex = BuildUnitRegex();
            this.baseUnits = new List<KnownUnit>(this.system.BaseUnits);

            foreach (var unit in system)
            {
                if (unit.Symbol != unit.BaseSymbol)
                {
                    this.prefixedUnits.Add(unit.BaseSymbol, unit);
                }
            }
        }

        public string ToString(Unit unit)
        {
            var nominator = new List<string>();
            var denominator = new List<string>();

            for (var i = 0; i < unit.Dimension.Count(); i++)
            {
                var exponent = unit.Dimension[i];

                if (exponent > 0)
                {
                    nominator.Add(BuildFactor(this.baseUnits[i].Symbol, exponent));
                }

                if (exponent < 0)
                {
                    denominator.Add(BuildFactor(this.baseUnits[i].Symbol, exponent));
                }
            }

            if (!nominator.Any() && !denominator.Any())
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            if (!unit.IsCoherent)
            {
                builder.Append(unit.Factor).Append(" ");
            }

            builder.Append(!nominator.Any() ? "1" : string.Join(this.dialect.Multiplication, nominator));

            if (denominator.Any())
            {
                builder.Append(" {0} ".FormatWith(this.dialect.Division));
                builder.Append(string.Join(this.dialect.Multiplication, denominator));
            }

            return builder.ToString();
        }

        private string BuildFactor(string symbol, int exponent)
        {
            var absolute = Math.Abs(exponent);

            if (absolute == 1)
            {
                return symbol;
            }

            return "{0}{1}{2}".FormatWith(symbol, this.dialect.Exponentiation.First(), absolute);
        }

        public Unit Parse(string unitExpression)
        {
            Unit unit;

            if (!parseCache.TryGetValue(unitExpression, out unit))
            {
                lock (parseCache)
                {
                    if (!parseCache.TryGetValue(unitExpression, out unit))
                    {
                        unit = ParseCore(unitExpression);
                        parseCache.Add(unitExpression, unit);
                    }
                }
            }

            return unit;
        }

        private Unit ParseCore(string unitExpression)
        {
            var fraction = unitExpression.Split(this.dialect.Division, StringSplitOptions.RemoveEmptyEntries);

            if (fraction.Length == 0)
            {
                return this.system.NoUnit;
            }

            if (fraction.Length == 1)
            {
                return ParseUnitProduct(fraction[0]);
            }

            if (fraction.Length > 2)
            {
                throw new FormatException(Messages.UnitExpressionInvalid.FormatWith(unitExpression));
            }

            var nominator = ParseUnitProduct(fraction[0]);
            var denominator = ParseUnitProduct(fraction[1]);

            return nominator/denominator;
        }

        private Unit ParseUnitProduct(string unitExpression)
        {
            var factors = unitExpression.Split(this.dialect.Multiplication, StringSplitOptions.RemoveEmptyEntries);
            var product = this.system.NoUnit;

            return factors.Aggregate(product, (current, factor) => current*ParseUnit(factor));
        }

        private Unit ParseUnit(string unitExpression)
        {
            var matches = Regex.Matches(unitExpression, this.unitRegex);

            if (matches.Count == 0)
            {
                throw new FormatException(Messages.UnitExpressionInvalid.FormatWith(unitExpression));
            }

            if (matches.Count > 1)
            {
                throw new FormatException(Messages.UnitExpressionAmbiguous.FormatWith(unitExpression));
            }

            var prefixSymbol = matches[0].Groups["prefix"].Value;
            var unitSymbol = matches[0].Groups["unit"].Value;
            var exponent = matches[0].Groups["exponent"].Value;

            var knownUnit = this.system[unitSymbol];
            Unit parsedUnit;

            if (knownUnit == null)
            {
                knownUnit = this.prefixedUnits[unitSymbol];
                parsedUnit = knownUnit/knownUnit.InherentFactor;
            }
            else
            {
                parsedUnit = knownUnit;
            }


            if (!string.IsNullOrEmpty(prefixSymbol))
            {
                parsedUnit *= UnitPrefix.Prefixes[prefixSymbol];
            }

            if (!string.IsNullOrEmpty(exponent))
            {
                parsedUnit ^= int.Parse(exponent);
            }

            return parsedUnit;
        }

        private string BuildUnitRegex()
        {
            var prefixes = string.Join("|", UnitPrefix.Prefixes.Keys);
            var units = string.Join("|", this.system.Select(u => Regex.Escape(u.BaseSymbol)));
            var exponentiationOperators = string.Join("|", this.dialect.Exponentiation.Select(c => Regex.Escape(c.ToString())));

            var regex = @"^(?<prefix>({0}))?(?<unit>{1})({2}(?<exponent>-?\d+))?$".FormatWith(prefixes, units,
                exponentiationOperators);

            return regex;
        }
    }
}