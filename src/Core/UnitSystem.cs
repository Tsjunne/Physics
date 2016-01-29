using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Physics.Parsing;

namespace Physics
{
    internal class UnitSystem : IUnitSystem
    {
        private readonly Dictionary<Dimension, KnownUnit> _baseUnits;
        private readonly Dictionary<Dimension, Unit> _coherentUnits;
        private readonly IDialect _dialect;
        private readonly IUnitFactory _unitFactory;
        private readonly Dictionary<Tuple<double, Dimension>, KnownUnit> _units;
        private readonly Dictionary<string, KnownUnit> _unitsBySymbol;
        private UnitInterpretor _unitInterpretor;

        public UnitSystem(string name)
            : this(name, new UnitFactory(), new Dialect())
        {
        }

        public UnitSystem(string name, IDialect dialect)
            : this(name, new UnitFactory(), dialect)
        {
        }

        internal UnitSystem(string name, IUnitFactory unitFactory, IDialect dialect)
        {
            Check.Argument(name, nameof(name)).IsNotNull();
            Check.Argument(unitFactory, nameof(unitFactory)).IsNotNull();
            Check.Argument(dialect, nameof(dialect)).IsNotNull();

            _unitFactory = unitFactory;
            _dialect = dialect;
            Name = name;
            _baseUnits = new Dictionary<Dimension, KnownUnit>();
            _coherentUnits = new Dictionary<Dimension, Unit>();
            _units = new Dictionary<Tuple<double, Dimension>, KnownUnit>();
            _unitsBySymbol = new Dictionary<string, KnownUnit>();
            NoUnit = new DerivedUnit(this, 1, Dimension.DimensionLess);
        }

        private UnitInterpretor Interpretor
            => this._unitInterpretor ?? (this._unitInterpretor = new UnitInterpretor(this, _dialect));

        public string Name { get; }
        public int NumberOfDimensions { get; private set; }
        public Unit NoUnit { get; }
        public IEnumerable<KnownUnit> BaseUnits => this._baseUnits.Values;

        public KnownUnit this[string key]
        {
            get
            {
                KnownUnit unit;
                this._unitsBySymbol.TryGetValue(key, out unit);
                return unit;
            }
        }

        public Unit AddBaseUnit(string symbol, string name, bool inherentPrefix = false)
        {
            var newUnit = this._unitFactory.CreateUnit(this, 1, CreateNewBaseDimension(this.BaseUnits.Count()), symbol,
                name,
                inherentPrefix);
            EnsureUnitIsNotRegistered(newUnit);

            _baseUnits.Add(newUnit.Dimension, newUnit);
            NumberOfDimensions++;

            RegisterKnownUnit(newUnit);

            return newUnit;
        }

        public Unit AddDerivedUnit(string symbol, string name, Unit unit)
        {
            var newUnit = this._unitFactory.CreateUnit(this, unit.Factor, unit.Dimension, symbol, name, false);
            EnsureUnitIsNotRegistered(newUnit);

            RegisterKnownUnit(newUnit);

            return newUnit;
        }

        public Unit CreateUnit(double factor, Dimension dimension)
        {
            // Prefer returning a known or coherent unit
            KnownUnit known;
            Unit coherent;

            if (this._units.TryGetValue(Tuple.Create(factor, dimension), out known))
            {
                return known;
            }
            else if(factor.Equals(1) && this._coherentUnits.TryGetValue(dimension, out coherent))
            {
                return coherent;
            }

            return this._unitFactory.CreateUnit(this, factor, dimension);
        }

        public Unit Parse(string unitExpression)
        {
            return this.Interpretor.Parse(unitExpression);
        }

        public string Display(Unit unit)
        {
            Check.UnitsAreFromSameSystem(this.NoUnit, unit);

            return this.Interpretor.ToString(unit);
        }

        public Quantity MakeCoherent(Quantity quantity)
        {
            var unit = quantity.Unit;

            if (unit.IsCoherent) return quantity;

            Unit coherentUnit;
            if (!this._coherentUnits.TryGetValue(unit.Dimension, out coherentUnit))
            {
                lock (this._coherentUnits)
                {
                    if (!this._coherentUnits.TryGetValue(unit.Dimension, out coherentUnit))
                    {
                        coherentUnit = unit/unit.Factor;
                        this._coherentUnits.Add(coherentUnit.Dimension, coherentUnit);
                    }
                }
            }

            return new Quantity(unit.Factor*quantity.Amount, coherentUnit);
        }

        private void RegisterKnownUnit(KnownUnit unit)
        {
            _units.Add(Tuple.Create(unit.Factor, unit.Dimension), unit);
            _unitsBySymbol.Add(unit.Symbol, unit);

            if (unit.Factor.Equals(1))
            {
                this._coherentUnits.Add(unit.Dimension, unit);
            }
        }

        private void EnsureUnitIsNotRegistered(KnownUnit unit)
        {
            if (this._units.Values.Any(u => u.Symbol == unit.Symbol))
            {
                throw new InvalidOperationException(Messages.UnitSymbolAlreadyKnown.FormatWith(unit.Symbol));
            }

            KnownUnit collision;

            if (this._units.TryGetValue(Tuple.Create(unit.Factor, unit.Dimension), out collision))
            {
                throw new InvalidOperationException(Messages.UnitAlreadyKnown.FormatWith(unit, collision));
            }
        }

        private static Dimension CreateNewBaseDimension(int index)
        {
            var exponents = new int[index + 1];
            exponents[index] = 1;

            return new Dimension(exponents);
        }

        #region IEnumerable<KnownUnit>

        public IEnumerator<KnownUnit> GetEnumerator()
        {
            return this._units.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}