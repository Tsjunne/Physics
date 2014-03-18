using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    [Serializable]
    public class Dimension : ImmutableCollection<int>
    {
        private static Dimension dimensionLess = new Dimension(new int[] { });

        private Dimension(int[] exponents)
            : base(Trim(exponents))
        {
            Check.Argument(exponents, "exponents").IsNotNull();
        }

        public static Dimension DimensionLess
        {
            get 
            {
                return dimensionLess;
            }
        }

        public static Dimension Create(params int[] exponents)
        {
            return new Dimension(exponents);
        }

        public static Dimension operator *(Dimension dimension1, Dimension dimension2)
        {
            Check.Argument(dimension1, "dimension1").IsNotNull();
            Check.Argument(dimension2, "dimension2").IsNotNull();

            return new Dimension(dimension1.Merge(dimension2, (x, y) => x + y).ToArray());
        }

        public static Dimension operator /(Dimension dimension1, Dimension dimension2)
        {
            Check.Argument(dimension1, "dimension1").IsNotNull();
            Check.Argument(dimension2, "dimension2").IsNotNull();

            return new Dimension(dimension1.Merge(dimension2, (x, y) => x - y).ToArray());
        }

        public static Dimension operator ^(Dimension dimension, int exponent)
        {
            Check.Argument(dimension, "dimension").IsNotNull();

            return new Dimension(dimension.Select(e => e * exponent).ToArray());
        }

        private static int[] Trim(int[] exponents)
        {
            if (!exponents.Any()) return exponents;

            int index = exponents.Length - 1; ;

            while (exponents[index] == 0)
            {
                index--;
            }

            Array.Resize(ref exponents, index + 1);

            return exponents;
        }

        public override string ToString()
        {
            return this.ToArray().ToString();
        }
    }
}
