using Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics.Test
{
    public abstract class GivenSiSystem : GivenWhenThen
    {
        protected IUnitSystem System {get; private set;}
        
        #region Base units
        /// <summary>
        /// length
        /// </summary>
        protected Unit m { get; private set; }
        /// <summary>
        /// mass
        /// </summary>
        protected Unit kg { get; private set; }
        /// <summary>
        /// time
        /// </summary>
        protected Unit s { get; private set; }
        /// <summary>
        /// electric current
        /// </summary>
        protected Unit A { get; private set; }
        /// <summary>
        /// thermodynamic temperature
        /// </summary>
        protected Unit K { get; private set; }
        /// <summary>
        /// amount of substance
        /// </summary>
        protected Unit mol { get; private set; }
        /// <summary>
        /// luminous intensity
        /// </summary>
        protected Unit cd { get; private set; }
        #endregion

        #region Derived units
        /// <summary>
        /// frequency
        /// </summary>
        protected Unit Hz { get; private set; }
        /// <summary>
        /// force, weight
        /// </summary>
        protected Unit N { get; private set; }
        /// <summary>
        /// pressure, stress
        /// </summary>
        protected Unit Pa { get; private set; }
        /// <summary>
        /// energy, work, heat
        /// </summary>
        protected Unit J { get; private set; }
        /// <summary>
        /// power, radiant flux
        /// </summary>
        protected Unit W { get; private set; }
        /// <summary>
        /// electric charge, quantity of electricity
        /// </summary>
        protected Unit C { get; private set; }
        /// <summary>
        /// voltage (electrical potential difference), electromotive force
        /// </summary>
        protected Unit V { get; private set; }
        /// <summary>
        /// electric capacitance
        /// </summary>
        protected Unit F { get; private set; }
        /// <summary>
        /// electric resistance, impedance, reactance
        /// </summary>
        protected Unit Ω { get; private set; }
        /// <summary>
        /// electrical conductance
        /// </summary>
        protected Unit S { get; private set; }
        /// <summary>
        /// magnetic flux
        /// </summary>
        protected Unit Wb { get; private set; }
        /// <summary>
        /// magnetic field strength
        /// </summary>
        protected Unit T { get; private set; }
        /// <summary>
        /// inductance
        /// </summary>
        protected Unit H { get; private set; }
        /// <summary>
        /// illuminance
        /// </summary>
        protected Unit lx { get; private set; }
        /// <summary>
        /// equivalent dose of ionizing radiation
        /// </summary>
        protected Unit Sv { get; private set; }
        /// <summary>
        /// catalytic activity
        /// </summary>
        protected Unit kat { get; private set; } 
        #endregion

        #region Incoherent units
        /// <summary>
        /// Time expressed in hours
        /// </summary>
        protected Unit h { get; private set; }
        #endregion

        public override void Given()
        {
            this.System = UnitSystemFactory.CreateSystem("SI");

            //Base units
            this.m = this.System.AddBaseUnit("m", "metre");
            this.kg = this.System.AddBaseUnit("kg", "kilogram", true);
            this.s = this.System.AddBaseUnit("s", "second");
            this.A = this.System.AddBaseUnit("A", "ampere");
            this.K = this.System.AddBaseUnit("K", "kelvin");
            this.mol = this.System.AddBaseUnit("mol", "mole");
            this.cd = this.System.AddBaseUnit("cd", "candela");

            //Derived units
            this.Hz = this.System.AddDerivedUnit("Hz", "hertz", s ^ -1);
            this.N = this.System.AddDerivedUnit("N", "newton", kg * m * (s ^ -2));
            this.Pa = this.System.AddDerivedUnit("Pa", "pascal", N * (m ^ -2));
            this.J = this.System.AddDerivedUnit("J", "joule", N * m);
            this.W = this.System.AddDerivedUnit("W", "watt", J / s);
            this.C = this.System.AddDerivedUnit("C", "coulomb", s * A);
            this.V = this.System.AddDerivedUnit("V", "volt", W / A);
            this.F = this.System.AddDerivedUnit("F", "farad", C / V);
            this.Ω = this.System.AddDerivedUnit("Ω", "joule", V / A);
            this.S = this.System.AddDerivedUnit("S", "siemens", A / V);
            this.Wb = this.System.AddDerivedUnit("Wb", "weber", V * s);
            this.T = this.System.AddDerivedUnit("T", "tesla", Wb * (s ^ -2));
            this.H = this.System.AddDerivedUnit("H", "inductance", Wb / A);
            this.lx = this.System.AddDerivedUnit("lx", "immulinance", (m ^ -2) * cd);
            this.Sv = this.System.AddDerivedUnit("Sv", "sievert", J / kg);
            this.kat = this.System.AddDerivedUnit("kat", "katal", (s ^ -1) * mol);

            //Incoherent units
            this.h = this.System.AddDerivedUnit("h", "hour", 60 * 60 * s);
        }
    }
}
