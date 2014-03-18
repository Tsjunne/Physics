using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    public sealed class UnitPrefix
    {
        static UnitPrefix()
        {
            Prefixes = new Dictionary<string, UnitPrefix>();

            Prefixes.Add(Z.Symbol, Z);
            Prefixes.Add(E.Symbol, E);
            Prefixes.Add(P.Symbol, P);
            Prefixes.Add(T.Symbol, T);
            Prefixes.Add(G.Symbol, G);
            Prefixes.Add(M.Symbol, M);
            Prefixes.Add(k.Symbol, k);
            Prefixes.Add(d.Symbol, d);
            Prefixes.Add(c.Symbol, c);
            Prefixes.Add(m.Symbol, m);
            Prefixes.Add(µ.Symbol, µ);
            Prefixes.Add(n.Symbol, n);
            Prefixes.Add(p.Symbol, p);
            Prefixes.Add(f.Symbol, f);
            Prefixes.Add(a.Symbol, a);
            Prefixes.Add(z.Symbol, z);
            Prefixes.Add(y.Symbol, y);
        }

        public UnitPrefix(string symbol, string name, int exponent)
        {
            this.Symbol = symbol;
            this.Name = name;
            this.Exponent = exponent;
            this.Factor = Math.Pow(10, this.Exponent);
        }

        public string Symbol { get; private set; }
        public string Name { get; private set; }
        public int Exponent { get; private set; }
        public double Factor { get; private set; }

        public override string ToString()
        {
            return this.Symbol;
        }

        public static implicit operator double (UnitPrefix prefix)
        {
            return prefix.Factor;
        }

        public static IDictionary<string, UnitPrefix> Prefixes;

        public static UnitPrefix Z = new UnitPrefix("Z","zetta",  21);
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

    }
}
