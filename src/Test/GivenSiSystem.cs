using System.Diagnostics.CodeAnalysis;

namespace Physics.Test
{
    [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
    public abstract class GivenSiSystem
    {
        protected IUnitSystem System { get; private set; }

        #region Incoherent units

        /// <summary>
        ///     Time expressed in hours
        /// </summary>
        protected Unit h { get; private set; }

        #endregion

        public GivenSiSystem()
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
            N = System.AddDerivedUnit("N", "newton", kg*m*(s ^ -2));
            Pa = System.AddDerivedUnit("Pa", "pascal", N*(m ^ -2));
            J = System.AddDerivedUnit("J", "joule", N*m);
            W = System.AddDerivedUnit("W", "watt", J/s);
            C = System.AddDerivedUnit("C", "coulomb", s*A);
            V = System.AddDerivedUnit("V", "volt", W/A);
            F = System.AddDerivedUnit("F", "farad", C/V);
            Ω = System.AddDerivedUnit("Ω", "joule", V/A);
            S = System.AddDerivedUnit("S", "siemens", A/V);
            Wb = System.AddDerivedUnit("Wb", "weber", V*s);
            T = System.AddDerivedUnit("T", "tesla", Wb*(s ^ -2));
            H = System.AddDerivedUnit("H", "inductance", Wb/A);
            lx = System.AddDerivedUnit("lx", "immulinance", (m ^ -2)*cd);
            Sv = System.AddDerivedUnit("Sv", "sievert", J/kg);
            kat = System.AddDerivedUnit("kat", "katal", (s ^ -1)*mol);

            //Incoherent units
            h = System.AddDerivedUnit("h", "hour", 60*60*s);
        }

        #region Base units

        /// <summary>
        ///     length
        /// </summary>
        protected Unit m { get; private set; }

        /// <summary>
        ///     mass
        /// </summary>
        protected Unit kg { get; private set; }

        /// <summary>
        ///     time
        /// </summary>
        protected Unit s { get; private set; }

        /// <summary>
        ///     electric current
        /// </summary>
        protected Unit A { get; private set; }

        /// <summary>
        ///     thermodynamic temperature
        /// </summary>
        protected Unit K { get; private set; }

        /// <summary>
        ///     amount of substance
        /// </summary>
        protected Unit mol { get; private set; }

        /// <summary>
        ///     luminous intensity
        /// </summary>
        protected Unit cd { get; private set; }

        #endregion

        #region Derived units

        /// <summary>
        ///     frequency
        /// </summary>
        protected Unit Hz { get; private set; }

        /// <summary>
        ///     force, weight
        /// </summary>
        protected Unit N { get; private set; }

        /// <summary>
        ///     pressure, stress
        /// </summary>
        protected Unit Pa { get; private set; }

        /// <summary>
        ///     energy, work, heat
        /// </summary>
        protected Unit J { get; private set; }

        /// <summary>
        ///     power, radiant flux
        /// </summary>
        protected Unit W { get; private set; }

        /// <summary>
        ///     electric charge, quantity of electricity
        /// </summary>
        protected Unit C { get; private set; }

        /// <summary>
        ///     voltage (electrical potential difference), electromotive force
        /// </summary>
        protected Unit V { get; private set; }

        /// <summary>
        ///     electric capacitance
        /// </summary>
        protected Unit F { get; private set; }

        /// <summary>
        ///     electric resistance, impedance, reactance
        /// </summary>
        protected Unit Ω { get; private set; }

        /// <summary>
        ///     electrical conductance
        /// </summary>
        protected Unit S { get; private set; }

        /// <summary>
        ///     magnetic flux
        /// </summary>
        protected Unit Wb { get; private set; }

        /// <summary>
        ///     magnetic field strength
        /// </summary>
        protected Unit T { get; private set; }

        /// <summary>
        ///     inductance
        /// </summary>
        protected Unit H { get; private set; }

        /// <summary>
        ///     illuminance
        /// </summary>
        protected Unit lx { get; private set; }

        /// <summary>
        ///     equivalent dose of ionizing radiation
        /// </summary>
        protected Unit Sv { get; private set; }

        /// <summary>
        ///     catalytic activity
        /// </summary>
        protected Unit kat { get; private set; }

        #endregion
    }
}