using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Physics.Presentation;

namespace Physics
{
    internal class UnitSystem : IUnitSystem
    {
        private readonly Dictionary<Dimension, KnownUnit> _baseUnits;
        private readonly Dictionary<Dimension, Unit> _coherentUnits;
        private readonly IUnitDialect _dialect;
        private readonly IUnitFactory _unitFactory;
        private readonly Dictionary<Tuple<double, Dimension>, KnownUnit> _units;
        private readonly Dictionary<string, KnownUnit> _unitsBySymbol;
        private UnitInterpretor _unitInterpretor;

        public UnitSystem(string name)
            : this(name, new UnitFactory(), new UnitDialect())
        {
        }

        public UnitSystem(string name, IUnitDialect dialect)
            : this(name, new UnitFactory(), dialect)
        {
        }

        internal UnitSystem(string name, IUnitFactory unitFactory, IUnitDialect dialect)
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
            => _unitInterpretor ?? (_unitInterpretor = new UnitInterpretor(this, _dialect));

        public string Name { get; }
        public int NumberOfDimensions { get; private set; }
        public Unit NoUnit { get; }
        public IEnumerable<KnownUnit> BaseUnits => _baseUnits.Values;

        public KnownUnit this[string key]
        {
            get
            {
                _unitsBySymbol.TryGetValue(key, out KnownUnit unit);
                return unit;
            }
        }

        public Unit AddBaseUnit(string symbol, string name, bool inherentPrefix = false)
        {
            var newUnit = _unitFactory.CreateUnit(this, 1, CreateNewBaseDimension(BaseUnits.Count()), symbol,
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
            var newUnit = _unitFactory.CreateUnit(this, unit.Factor, unit.Dimension, symbol, name, false);
            EnsureUnitIsNotRegistered(newUnit);

            RegisterKnownUnit(newUnit);

            return newUnit;
        }

        public Unit CreateUnit(double factor, Dimension dimension)
        {
            // Prefer returning a known or coherent unit

            if (_units.TryGetValue(Tuple.Create(factor, dimension), out KnownUnit known))
            {
                return known;
            }
            else if (factor.Equals(1) && _coherentUnits.TryGetValue(dimension, out Unit coherent))
            {
                return coherent;
            }

            return _unitFactory.CreateUnit(this, factor, dimension);
        }

        public Unit Parse(string unitExpression)
        {
            return Interpretor.Parse(unitExpression);
        }

        public string Display(Unit unit)
        {
            Check.UnitsAreFromSameSystem(NoUnit, unit);

            return Interpretor.ToString(unit);
        }

        public Quantity MakeCoherent(Quantity quantity)
        {
            var unit = quantity.Unit;

            if (unit.IsCoherent) return quantity;

            if (!_coherentUnits.TryGetValue(unit.Dimension, out Unit coherentUnit))
            {
                lock (_coherentUnits)
                {
                    if (!_coherentUnits.TryGetValue(unit.Dimension, out coherentUnit))
                    {
                        coherentUnit = unit / unit.Factor;
                        _coherentUnits.Add(coherentUnit.Dimension, coherentUnit);
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
                _coherentUnits.Add(unit.Dimension, unit);
            }
        }

        private void EnsureUnitIsNotRegistered(KnownUnit unit)
        {
            if (_units.Values.Any(u => u.Symbol == unit.Symbol))
            {
                throw new InvalidOperationException(Messages.UnitSymbolAlreadyKnown.FormatWith(unit.Symbol));
            }

            if (_units.TryGetValue(Tuple.Create(unit.Factor, unit.Dimension), out KnownUnit collision))
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
            return _units.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}