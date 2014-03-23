using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Physics.Systems
{
    public static class SI
    {
        static SI()
        {
            System = UnitSystemFactory.CreateSystem("SI");

            //Base units
            m = System.AddBaseUnit("m", "metre");
            kg = System.AddBaseUnit("kg", "kilogram", true);
            s = System.AddBaseUnit("s", "second");
            A = System.AddBaseUnit("A", "ampere");
            K = System.AddBaseUnit("K", "kelvin");
            mol = System.AddBaseUnit("mol", "mole");
            cd = System.AddBaseUnit("cd", "candela");

            //Derived units
            Hz = System.AddDerivedUnit("Hz", "hertz", s ^ -1);
            N = System.AddDerivedUnit("N", "newton", kg * m * (s ^ -2));
            Pa = System.AddDerivedUnit("Pa", "pascal", N * (m ^ -2));
            J = System.AddDerivedUnit("J", "joule", N * m);
            W = System.AddDerivedUnit("W", "watt", J / s);
            C = System.AddDerivedUnit("C", "coulomb", s * A);
            V = System.AddDerivedUnit("V", "volt", W / A);
            F = System.AddDerivedUnit("F", "farad", C / V);
            Ω = System.AddDerivedUnit("Ω", "joule", V / A);
            S = System.AddDerivedUnit("S", "siemens", A / V);
            Wb = System.AddDerivedUnit("Wb", "weber", V * s);
            T = System.AddDerivedUnit("T", "tesla", Wb * (s ^ -2));
            H = System.AddDerivedUnit("H", "inductance", Wb / A);
            lx = System.AddDerivedUnit("lx", "immulinance", (m ^ -2) * cd);
            Sv = System.AddDerivedUnit("Sv", "sievert", J / kg);
            kat = System.AddDerivedUnit("kat", "katal", (s ^ -1) * mol);

            //Incoherent units
            h = System.AddDerivedUnit("h", "hour", 60 * 60 * s);
        }

        /// <summary>
        /// International System of Units
        /// </summary>
        public static IUnitSystem System { get; private set; }

        #region Base units
        /// <summary>
        /// length
        /// </summary>
        public static Unit m { get; private set; }
        /// <summary>
        /// mass
        /// </summary>
        public static Unit kg { get; private set; }
        /// <summary>
        /// time
        /// </summary>
        public static Unit s { get; private set; }
        /// <summary>
        /// electric current
        /// </summary>
        public static Unit A { get; private set; }
        /// <summary>
        /// thermodynamic temperature
        /// </summary>
        public static Unit K { get; private set; }
        /// <summary>
        /// amount of substance
        /// </summary>
        public static Unit mol { get; private set; }
        /// <summary>
        /// luminous intensity
        /// </summary>
        public static Unit cd { get; private set; }
        #endregion

        #region Derived units
        /// <summary>
        /// frequency
        /// </summary>
        public static Unit Hz { get; private set; }
        /// <summary>
        /// force, weight
        /// </summary>
        public static Unit N { get; private set; }
        /// <summary>
        /// pressure, stress
        /// </summary>
        public static Unit Pa { get; private set; }
        /// <summary>
        /// energy, work, heat
        /// </summary>
        public static Unit J { get; private set; }
        /// <summary>
        /// power, radiant flux
        /// </summary>
        public static Unit W { get; private set; }
        /// <summary>
        /// electric charge, quantity of electricity
        /// </summary>
        public static Unit C { get; private set; }
        /// <summary>
        /// voltage (electrical potential difference), electromotive force
        /// </summary>
        public static Unit V { get; private set; }
        /// <summary>
        /// electric capacitance
        /// </summary>
        public static Unit F { get; private set; }
        /// <summary>
        /// electric resistance, impedance, reactance
        /// </summary>
        public static Unit Ω { get; private set; }
        /// <summary>
        /// electrical conductance
        /// </summary>
        public static Unit S { get; private set; }
        /// <summary>
        /// magnetic flux
        /// </summary>
        public static Unit Wb { get; private set; }
        /// <summary>
        /// magnetic field strength
        /// </summary>
        public static Unit T { get; private set; }
        /// <summary>
        /// inductance
        /// </summary>
        public static Unit H { get; private set; }
        /// <summary>
        /// illuminance
        /// </summary>
        public static Unit lx { get; private set; }
        /// <summary>
        /// equivalent dose of ionizing radiation
        /// </summary>
        public static Unit Sv { get; private set; }
        /// <summary>
        /// catalytic activity
        /// </summary>
        public static Unit kat { get; private set; }
        #endregion

        #region Incoherent units
        /// <summary>
        /// Time expressed in hours
        /// </summary>
        public static Unit h { get; private set; }
        #endregion
    }
}
