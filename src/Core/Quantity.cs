using System;
using System.Globalization;

namespace Physics
{
    public class Quantity : IEquatable<Quantity>, IComparable<Quantity>
    {
        private readonly Quantity coherent;
        private readonly int hashCode;

        public Quantity(double amount, Unit unit)
        {
            this.Amount = amount;
            this.Unit = unit;
            this.coherent = this.ToCoherent();

            this.hashCode = this.GenerateHashCode();
        }

        private Quantity(double amount, Unit unit, Quantity coherent)
        {
            this.Amount = amount;
            this.Unit = unit;
            this.coherent = coherent;

            this.hashCode = this.GenerateHashCode();
        }

        public double Amount { get; }
        public Unit Unit { get; }

        public bool Equals(Quantity other)
        {
            if (ReferenceEquals(other, null)) return false;

            return this.coherent.Unit == other.coherent.Unit
                   && this.coherent.Amount == other.coherent.Amount;
        }

        public int CompareTo(Quantity other)
        {
            if (this < other) return -1;
            if (this > other) return 1;

            return 0;
        }

        public Quantity ToCoherent()
        {
            return this.Unit.System.MakeCoherent(this);
        }

        public Quantity Convert(Unit unit)
        {
            if (this.Unit == unit) return this;

            Check.UnitsAreSameDimension(this.Unit, unit);
            return new Quantity(this.coherent.Amount/unit.Factor, unit, this.coherent);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Quantity);
        }

        public override int GetHashCode()
        {
            return this.hashCode;
        }
        
        public static bool operator ==(Quantity quantity1, Quantity quantity2)
        {
            if (ReferenceEquals(quantity1, quantity2)) return true;
            if (ReferenceEquals(quantity1, null)) return false;

            return quantity1.Equals(quantity2);
        }

        public static bool operator !=(Quantity quantity1, Quantity quantity2)
        {
            return !(quantity1 == quantity2);
        }

        public static bool operator >(Quantity quantity1, Quantity quantity2)
        {
            if (ReferenceEquals(quantity1, quantity2)) return false;
            if (ReferenceEquals(quantity1, null)) return false;
            if (ReferenceEquals(quantity2, null)) return true;

            return quantity1.coherent.Amount > quantity2.coherent.Amount && quantity1.Unit == quantity2.Unit;
        }

        public static bool operator <(Quantity quantity1, Quantity quantity2)
        {
            if (ReferenceEquals(quantity1, quantity2)) return false;
            if (ReferenceEquals(quantity1, null)) return true;
            if (ReferenceEquals(quantity2, null)) return false;

            return quantity1.coherent.Amount < quantity2.coherent.Amount && quantity1.Unit == quantity2.Unit;
        }

        public static bool operator >=(Quantity quantity1, Quantity quantity2)
        {
            if (ReferenceEquals(quantity1, quantity2)) return true;
            if (ReferenceEquals(quantity1, null)) return false;
            if (ReferenceEquals(quantity2, null)) return true;

            return quantity1.coherent.Amount >= quantity2.coherent.Amount && quantity1.Unit == quantity2.Unit;
        }

        public static bool operator <=(Quantity quantity1, Quantity quantity2)
        {
            if (ReferenceEquals(quantity1, quantity2)) return true;
            if (ReferenceEquals(quantity1, null)) return true;
            if (ReferenceEquals(quantity2, null)) return false;

            return quantity1.coherent.Amount <= quantity2.coherent.Amount && quantity1.Unit == quantity2.Unit;
        }

        public static Quantity operator +(Quantity q1, Quantity q2)
        {
            Check.UnitsAreSameDimension(q1.Unit, q2.Unit);
            return new Quantity(q1.coherent.Amount + q2.coherent.Amount, q1.Unit);
        }

        public static Quantity operator -(Quantity q1, Quantity q2)
        {
            Check.UnitsAreSameDimension(q1.Unit, q2.Unit);
            return new Quantity(q1.coherent.Amount - q2.coherent.Amount, q1.Unit);
        }

        public static Quantity operator *(Quantity q1, Quantity q2)
        {
            return new Quantity(q1.coherent.Amount*q2.coherent.Amount, q1.coherent.Unit*q2.coherent.Unit);
        }

        public static Quantity operator *(Quantity q, double factor)
        {
            return new Quantity(q.coherent.Amount*factor, q.coherent.Unit);
        }

        public static Quantity operator *(double factor, Quantity q)
        {
            return new Quantity(q.coherent.Amount*factor, q.coherent.Unit);
        }

        public static Quantity operator /(Quantity q1, Quantity q2)
        {
            return new Quantity(q1.coherent.Amount/q2.coherent.Amount, q1.coherent.Unit/q2.coherent.Unit);
        }

        public static Quantity operator /(Quantity q, double factor)
        {
            return new Quantity(q.coherent.Amount/factor, q.coherent.Unit);
        }

        public static Quantity operator /(double factor, Quantity q)
        {
            return new Quantity(q.coherent.Amount/factor, q.coherent.Unit);
        }

        public static Quantity operator ^(Quantity q, int exponent)
        {
            return new Quantity(Math.Pow(q.coherent.Amount, exponent), q.coherent.Unit ^ exponent);
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
            return this.Convert(unit).ToString(null, NumberFormatInfo.CurrentInfo);
        }

        public string ToString(string format, Unit unit)
        {
            return this.Convert(unit).ToString(format, NumberFormatInfo.CurrentInfo);
        }

        public string ToString(IFormatProvider formatProvider, Unit unit)
        {
            return this.Convert(unit).ToString(null, formatProvider);
        }

        public string ToString(string format, IFormatProvider formatProvider, Unit unit)
        {
            return this.Convert(unit).ToString(format, formatProvider);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return "{0} {1}".FormatWith(this.Amount.ToString(format, formatProvider), this.Unit);
        }

        private int GenerateHashCode()
        {
            var hash = 17;
            hash = hash*23 + this.coherent.Unit.GetHashCode();
            hash = hash*23 + this.coherent.Amount.GetHashCode();
            return hash;
        }
    }
}