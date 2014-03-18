using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics.Serialization
{
    public static class Extensions
    {
        public static Quantity FromInfo(this IUnitSystem system, QuantityInfo info)
        {
            return new Quantity(info.Amount, system.CreateUnit(1, Dimension.Create(info.Dimension)));
        }

        public static QuantityInfo ToInfo(this Quantity quantity)
        {
            var coherent = quantity.ToCoherent();
            return new QuantityInfo
            {
                Amount = coherent.Amount,
                Dimension = coherent.Unit.Dimension.ToArray()
            };
        }
    }
}
