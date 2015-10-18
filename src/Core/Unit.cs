using System;

namespace Physics
{
    public abstract class Unit : IEquatable<Unit>
    {
        private readonly int hashCode;

        internal Unit(IUnitSystem system, double factor, Dimension dimension)
        {
            Check.Argument(dimension, nameof(dimension)).IsNotNull();

            this.System = system;
            this.Dimension = dimension;
            this.Factor = factor;

            this.hashCode = GenerateHashCode();
        }

        public IUnitSystem System { get; }
        public double Factor { get; }
        public Dimension Dimension { get; }
        public bool IsCoherent => this.Factor.Equals(1);

        public bool Equals(Unit other)
        {
            if (ReferenceEquals(this, other)) return true;
            if ((object) other == null) return false;

            return this.HasSameDimension(other)
                   && this.Factor.Equals(other.Factor);
        }

        public bool HasSameSystem(Unit other)
        {
            return this.System == other.System;
        }

        public bool HasSameDimension(Unit other)
        {
            return this.HasSameSystem(other)
                   && this.Dimension.Equals(other.Dimension);
        }

        public override int GetHashCode()
        {
            return this.hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Unit);
        }

        public static bool operator ==(Unit unit1, Unit unit2)
        {
            if (ReferenceEquals(unit1, unit2)) return true;
            if (((object) unit1 == null) || ((object) unit2 == null)) return false;

            return unit1.HasSameDimension(unit2)
                   && unit1.Factor.Equals(unit2.Factor);
        }

        public static bool operator !=(Unit unit1, Unit unit2)
        {
            return !(unit1 == unit2);
        }

        public static Unit operator *(Unit unit1, Unit unit2)
        {
            Check.Argument(unit1, nameof(unit1)).IsNotNull();
            Check.Argument(unit2, nameof(unit2)).IsNotNull();
            Check.UnitsAreFromSameSystem(unit1, unit2);

            return unit1.System.CreateUnit(unit1.Factor*unit2.Factor, unit1.Dimension*unit2.Dimension);
        }

        public static Unit operator /(Unit unit1, Unit unit2)
        {
            Check.Argument(unit1, nameof(unit1)).IsNotNull();
            Check.Argument(unit2, nameof(unit2)).IsNotNull();
            Check.UnitsAreFromSameSystem(unit1, unit2);

            return unit1.System.CreateUnit(unit1.Factor/unit2.Factor, unit1.Dimension/unit2.Dimension);
        }

        public static Unit operator ^(Unit unit, int exponent)
        {
            Check.Argument(unit, nameof(unit)).IsNotNull();

            return unit.System.CreateUnit(Math.Pow(unit.Factor, exponent), unit.Dimension ^ exponent);
        }

        public static Unit operator *(Unit unit, double factor)
        {
            return unit.System.CreateUnit(factor*unit.Factor, unit.Dimension);
        }

        public static Unit operator *(double factor, Unit unit)
        {
            return unit.System.CreateUnit(factor*unit.Factor, unit.Dimension);
        }

        public static Unit operator /(Unit unit, double factor)
        {
            return unit.System.CreateUnit(unit.Factor/factor, unit.Dimension);
        }

        public override string ToString()
        {
            return this.System.Display(this);
        }

        private int GenerateHashCode()
        {
            var hash = 17;
            hash = hash*23 + this.Dimension.GetHashCode();
            hash = hash*23 + this.Factor.GetHashCode();
            return hash;
        }
    }
}