using System;

namespace Physics
{
    public abstract class Unit : IEquatable<Unit>
    {
        private readonly int _hashCode;

        internal Unit(IUnitSystem system, double factor, Dimension dimension)
        {
            Check.Argument(dimension, nameof(dimension)).IsNotNull();

            System = system;
            Dimension = dimension;
            Factor = factor;

            _hashCode = GenerateHashCode();
        }

        internal IUnitSystem System { get; }
        internal double Factor { get; }
        internal Dimension Dimension { get; }
        internal bool IsCoherent => Factor.Equals(1);

        public bool Equals(Unit other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (other is null) return false;

            return HasSameDimension(other)
                   && Factor.Equals(other.Factor);
        }

        public bool HasSameSystem(Unit other)
        {
            return System == other.System;
        }

        public bool HasSameDimension(Unit other)
        {
            return HasSameSystem(other)
                   && Dimension.Equals(other.Dimension);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Unit);
        }

        public static bool operator ==(Unit unit1, Unit unit2)
        {
            if (ReferenceEquals(unit1, unit2)) return true;
            if ((unit1 is null) || (unit2 is null)) return false;

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
            return System.Display(this);
        }

        private int GenerateHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + Dimension.GetHashCode();
                hash = hash * 23 + Factor.GetHashCode();
                return hash;
            }
        }
    }
}