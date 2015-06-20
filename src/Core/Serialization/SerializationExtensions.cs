using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics.Serialization
{
    public static class SerializationExtensions
    {
        public static Quantity FromInfo(this IUnitSystem system, QuantityInfo info)
        {
            var unit = system.NoUnit;
            var symbols = info.Unit.Keys.ToArray();
            var exponents = info.Unit.Values.ToArray();

            for (int i = 0; i < info.Unit.Count; i++)
            {
                var baseUnit = system[symbols[i]];

                if (ReferenceEquals(baseUnit, null) || !baseUnit.IsCoherent)
                {
                    throw new InvalidOperationException(Messages.UnitsNotSameSystem);
                }

                unit = unit * (baseUnit ^ exponents[i]);
            }

            return new Quantity(info.Amount, unit);
        }

        public static QuantityInfo ToInfo(this Quantity quantity)
        {
            var coherent = quantity.ToCoherent();
            var baseUnits = quantity.Unit.System.BaseUnits;

            return new QuantityInfo
            {
                Amount = coherent.Amount,
                Unit = baseUnits.Merge(coherent.Unit.Dimension, (u, exp) => new { u.Symbol, exp }, true).ToDictionary(u => u.Symbol, u => u.exp)
            };
        }
    }
}
