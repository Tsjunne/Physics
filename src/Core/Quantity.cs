using System;
using System.Globalization;

namespace Physics
{
    public class Quantity : IEquatable<Quantity>, IComparable<Quantity>
    {
        private readonly Quantity _coherent;
        private readonly int _hashCode;

        public Quantity(double amount, Unit unit)
        {
            Amount = amount;
            Unit = unit;
            _coherent = ToCoherent();

            _hashCode = GenerateHashCode();
        }

        private Quantity(double amount, Unit unit, Quantity coherent)
        {
            Amount = amount;
            Unit = unit;
            _coherent = coherent;

            _hashCode = GenerateHashCode();
        }

        public double Amount { get; }
        public Unit Unit { get; }

        public bool Equals(Quantity other)
        {
            if (other is null) return false;

            return _coherent.Unit == other._coherent.Unit
                   && _coherent.Amount == other._coherent.Amount;
        }

        public int CompareTo(Quantity other)
        {
            if (this < other) return -1;
            if (this > other) return 1;

            return 0;
        }

        public Quantity Convert(Unit unit)
        {
            if (Unit == unit) return this;

            Check.UnitsAreSameDimension(Unit, unit);
            return new Quantity(_coherent.Amount/unit.Factor, unit, _coherent);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Quantity);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }
        
        public static bool operator ==(Quantity quantity1, Quantity quantity2)
        {
            if (ReferenceEquals(quantity1, quantity2)) return true;
            if (quantity1 is null) return false;

            return quantity1.Equals(quantity2);
        }

        public static bool operator !=(Quantity quantity1, Quantity quantity2)
        {
            return !(quantity1 == quantity2);
        }

        public static bool operator >(Quantity quantity1, Quantity quantity2)
        {
            if (ReferenceEquals(quantity1, quantity2)) return false;
            if (quantity1 is null) return false;
            if (quantity2 is null) return true;

            return quantity1._coherent.Amount > quantity2._coherent.Amount && quantity1._coherent.Unit == quantity2._coherent.Unit;
        }

        public static bool operator <(Quantity quantity1, Quantity quantity2)
        {
            if (ReferenceEquals(quantity1, quantity2)) return false;
            if (quantity1 is null) return true;
            if (quantity2 is null) return false;

            return quantity1._coherent.Amount < quantity2._coherent.Amount && quantity1._coherent.Unit == quantity2._coherent.Unit;
        }

        public static bool operator >=(Quantity quantity1, Quantity quantity2)
        {
            if (ReferenceEquals(quantity1, quantity2)) return true;
            if (quantity1 is null) return false;
            if (quantity2 is null) return true;

            return quantity1._coherent.Amount >= quantity2._coherent.Amount && quantity1._coherent.Unit == quantity2._coherent.Unit;
        }

        public static bool operator <=(Quantity quantity1, Quantity quantity2)
        {
            if (ReferenceEquals(quantity1, quantity2)) return true;
            if (quantity1 is null) return true;
            if (quantity2 is null) return false;

            return quantity1._coherent.Amount <= quantity2._coherent.Amount && quantity1._coherent.Unit == quantity2._coherent.Unit;
        }

        public static Quantity operator +(Quantity q1, Quantity q2)
        {
            Check.UnitsAreSameDimension(q1.Unit, q2.Unit);
            return new Quantity(q1._coherent.Amount + q2._coherent.Amount, q1._coherent.Unit);
        }

        public static Quantity operator -(Quantity q1, Quantity q2)
        {
            Check.UnitsAreSameDimension(q1.Unit, q2.Unit);
            return new Quantity(q1._coherent.Amount - q2._coherent.Amount, q1._coherent.Unit);
        }

        public static Quantity operator *(Quantity q1, Quantity q2)
        {
            return new Quantity(q1._coherent.Amount*q2._coherent.Amount, q1._coherent.Unit*q2._coherent.Unit);
        }

        public static Quantity operator *(Quantity q, double factor)
        {
            return new Quantity(q._coherent.Amount*factor, q._coherent.Unit);
        }

        public static Quantity operator *(double factor, Quantity q)
        {
            return new Quantity(q._coherent.Amount*factor, q._coherent.Unit);
        }

        public static Quantity operator /(Quantity q1, Quantity q2)
        {
            return new Quantity(q1._coherent.Amount/q2._coherent.Amount, q1._coherent.Unit/q2._coherent.Unit);
        }

        public static Quantity operator /(Quantity q, double factor)
        {
            return new Quantity(q._coherent.Amount/factor, q._coherent.Unit);
        }

        public static Quantity operator /(double factor, Quantity q)
        {
            return new Quantity(q._coherent.Amount/factor, q._coherent.Unit);
        }

        public static Quantity operator ^(Quantity q, int exponent)
        {
            return new Quantity(Math.Pow(q._coherent.Amount, exponent), q._coherent.Unit ^ exponent);
        }

        public override string ToString()
        {
            return ToString(null, NumberFormatInfo.CurrentInfo);
        }

        public string ToString(string format)
        {
            return ToString(format, NumberFormatInfo.CurrentInfo);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return ToString(null, formatProvider);
        }

        public string ToString(Unit unit)
        {
            return Convert(unit).ToString(null, NumberFormatInfo.CurrentInfo);
        }

        public string ToString(string format, Unit unit)
        {
            return Convert(unit).ToString(format, NumberFormatInfo.CurrentInfo);
        }

        public string ToString(IFormatProvider formatProvider, Unit unit)
        {
            return Convert(unit).ToString(null, formatProvider);
        }

        public string ToString(string format, IFormatProvider formatProvider, Unit unit)
        {
            return Convert(unit).ToString(format, formatProvider);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return "{0} {1}".FormatWith(Amount.ToString(format, formatProvider), Unit);
        }

        internal Quantity ToCoherent()
        {
            return Unit.System.MakeCoherent(this);
        }

        private int GenerateHashCode()
        {
            return (_coherent.Unit, _coherent.Amount).GetHashCode();
        }
    }
}