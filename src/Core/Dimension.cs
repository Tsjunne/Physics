using System;
using System.Linq;

namespace Physics
{
    [Serializable]
    public sealed class Dimension : ImmutableCollection<int>
    {
        internal Dimension(params int[] exponents)
            : base(Trim(exponents))
        {
            Check.Argument(exponents, nameof(exponents)).IsNotNull();
        }

        public static Dimension DimensionLess { get; } = new Dimension();

        public static Dimension operator *(Dimension dimension1, Dimension dimension2)
        {
            Check.Argument(dimension1, nameof(dimension1)).IsNotNull();
            Check.Argument(dimension2, nameof(dimension2)).IsNotNull();

            return new Dimension(dimension1.Merge(dimension2, (x, y) => x + y).ToArray());
        }

        public static Dimension operator /(Dimension dimension1, Dimension dimension2)
        {
            Check.Argument(dimension1, nameof(dimension1)).IsNotNull();
            Check.Argument(dimension2, nameof(dimension2)).IsNotNull();

            return new Dimension(dimension1.Merge(dimension2, (x, y) => x - y).ToArray());
        }

        public static Dimension operator ^(Dimension dimension, int exponent)
        {
            Check.Argument(dimension, nameof(dimension)).IsNotNull();

            return new Dimension(dimension.Select(e => e*exponent).ToArray());
        }

        private static int[] Trim(int[] exponents)
        {
            if (exponents.Length == 0) return exponents;

            var index = exponents.Length - 1;

            while (index >= 0 && exponents[index] == 0)
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