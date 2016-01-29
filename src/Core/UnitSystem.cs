using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Physics.Presentation;

namespace Physics
{
    internal class UnitSystem : IUnitSystem
    {
        private readonly Dictionary<Dimension, KnownUnit> baseUnits;
        private readonly Dictionary<Dimension, Unit> coherentUnits;
        private readonly IUnitDialect dialect;
        private readonly IUnitFactory unitFactory;
        private readonly Dictionary<Tuple<double, Dimension>, KnownUnit> units;
        private readonly Dictionary<string, KnownUnit> unitsBySymbol;
        private UnitInterpretor unitInterpretor;

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

            this.unitFactory = unitFactory;
            this.dialect = dialect;
            this.Name = name;
            this.baseUnits = new Dictionary<Dimension, KnownUnit>();
            this.coherentUnits = new Dictionary<Dimension, Unit>();
            this.units = new Dictionary<Tuple<double, Dimension>, KnownUnit>();
            this.unitsBySymbol = new Dictionary<string, KnownUnit>();
            this.NoUnit = new DerivedUnit(this, 1, Dimension.DimensionLess);
        }

        private UnitInterpretor Interpretor
            => this.unitInterpretor ?? (this.unitInterpretor = new UnitInterpretor(this, dialect));

        public string Name { get; }
        public int NumberOfDimensions { get; private set; }
        public Unit NoUnit { get; }
        public IEnumerable<KnownUnit> BaseUnits => this.baseUnits.Values;

        public KnownUnit this[string key]
        {
            get
            {
                KnownUnit unit;
                this.unitsBySymbol.TryGetValue(key, out unit);
                return unit;
            }
        }

        public Unit AddBaseUnit(string symbol, string name, bool inherentPrefix = false)
        {
            var newUnit = this.unitFactory.CreateUnit(this, 1, CreateNewBaseDimension(this.BaseUnits.Count()), symbol,
                name,
                inherentPrefix);
            EnsureUnitIsNotRegistered(newUnit);

            baseUnits.Add(newUnit.Dimension, newUnit);
            NumberOfDimensions++;

            RegisterKnownUnit(newUnit);

            return newUnit;
        }

        public Unit AddDerivedUnit(string symbol, string name, Unit unit)
        {
            var newUnit = this.unitFactory.CreateUnit(this, unit.Factor, unit.Dimension, symbol, name, false);
            EnsureUnitIsNotRegistered(newUnit);

            RegisterKnownUnit(newUnit);

            return newUnit;
        }

        public Unit CreateUnit(double factor, Dimension dimension)
        {
            // Prefer returning a known or coherent unit
            KnownUnit known;
            Unit coherent;

            if (this.units.TryGetValue(Tuple.Create(factor, dimension), out known))
            {
                return known;
            }
            else if(factor.Equals(1) && this.coherentUnits.TryGetValue(dimension, out coherent))
            {
                return coherent;
            }

            return this.unitFactory.CreateUnit(this, factor, dimension);
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
            if (!this.coherentUnits.TryGetValue(unit.Dimension, out coherentUnit))
            {
                lock (this.coherentUnits)
                {
                    if (!this.coherentUnits.TryGetValue(unit.Dimension, out coherentUnit))
                    {
                        coherentUnit = unit/unit.Factor;
                        this.coherentUnits.Add(coherentUnit.Dimension, coherentUnit);
                    }
                }
            }

            return new Quantity(unit.Factor*quantity.Amount, coherentUnit);
        }

        private void RegisterKnownUnit(KnownUnit unit)
        {
            units.Add(Tuple.Create(unit.Factor, unit.Dimension), unit);
            unitsBySymbol.Add(unit.Symbol, unit);

            if (unit.Factor.Equals(1))
            {
                this.coherentUnits.Add(unit.Dimension, unit);
            }
        }

        private void EnsureUnitIsNotRegistered(KnownUnit unit)
        {
            if (this.units.Values.Any(u => u.Symbol == unit.Symbol))
            {
                throw new InvalidOperationException(Messages.UnitSymbolAlreadyKnown.FormatWith(unit.Symbol));
            }

            KnownUnit collision;

            if (this.units.TryGetValue(Tuple.Create(unit.Factor, unit.Dimension), out collision))
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
            return this.units.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}