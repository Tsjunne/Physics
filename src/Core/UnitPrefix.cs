using System;
using System.Collections.Generic;

namespace Physics
{
    public sealed class UnitPrefix
    {
        public static IDictionary<string, UnitPrefix> Prefixes;
        public static UnitPrefix Z = new UnitPrefix("Z", "zetta", 21);
        public static UnitPrefix E = new UnitPrefix("E", "exa", 18);
        public static UnitPrefix P = new UnitPrefix("P", "peta", 15);
        public static UnitPrefix T = new UnitPrefix("T", "tera", 12);
        public static UnitPrefix G = new UnitPrefix("G", "giga", 9);
        public static UnitPrefix M = new UnitPrefix("M", "mega", 6);
        public static UnitPrefix k = new UnitPrefix("k", "kilo", 3);
        public static UnitPrefix h = new UnitPrefix("h", "hecto", 2);
        public static UnitPrefix d = new UnitPrefix("d", "deca", 1);
        public static UnitPrefix c = new UnitPrefix("c", "centi", -2);
        public static UnitPrefix m = new UnitPrefix("m", "milli", -3);
        public static UnitPrefix µ = new UnitPrefix("µ", "micro", -6);
        public static UnitPrefix n = new UnitPrefix("n", "nano", -9);
        public static UnitPrefix p = new UnitPrefix("p", "pico", -12);
        public static UnitPrefix f = new UnitPrefix("f", "femto", -15);
        public static UnitPrefix a = new UnitPrefix("a", "atto", -18);
        public static UnitPrefix z = new UnitPrefix("z", "zepto", -21);
        public static UnitPrefix y = new UnitPrefix("y", "yocto", -24);

        static UnitPrefix()
        {
            Prefixes = new Dictionary<string, UnitPrefix>
            {
                {Z.Symbol, Z},
                {E.Symbol, E},
                {P.Symbol, P},
                {T.Symbol, T},
                {G.Symbol, G},
                {M.Symbol, M},
                {k.Symbol, k},
                {d.Symbol, d},
                {c.Symbol, c},
                {m.Symbol, m},
                {µ.Symbol, µ},
                {n.Symbol, n},
                {p.Symbol, p},
                {f.Symbol, f},
                {a.Symbol, a},
                {z.Symbol, z},
                {y.Symbol, y}
            };
        }

        public UnitPrefix(string symbol, string name, int exponent)
        {
            Symbol = symbol;
            Name = name;
            Factor = Math.Pow(10, exponent);
        }

        public string Symbol { get; }
        public string Name { get; }
        public double Factor { get; }

        public override string ToString()
        {
            return Symbol;
        }

        public static implicit operator double(UnitPrefix prefix)
        {
            return prefix.Factor;
        }
    }
}