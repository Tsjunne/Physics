using Physics.Presentation;
using Physics.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    internal class UnitSystem : IUnitSystem, INotifyCollectionChanged
    {
        private readonly IUnitFactory unitFactory;
        private readonly IUnitDialect dialect;
        private readonly Dictionary<Dimension, KnownUnit> baseUnits;
        private readonly Dictionary<Dimension, Unit> coherentUnits;
        private readonly Dictionary<Tuple<double, Dimension>, KnownUnit> units;
        private readonly Dictionary<string, KnownUnit> unitsBySymbol;
        private readonly Unit noUnit;

        private int numberOfDimensions;
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
            this.noUnit = new DerivedUnit(this, 1, Dimension.DimensionLess);
        }

        public string Name { get; private set; }

        public int NumberOfDimensions
        {
            get { return numberOfDimensions; }
        }

        public Unit NoUnit
        {
            get { return this.noUnit; }
        }

        public ReadOnlyCollection<KnownUnit> BaseUnits 
        {
            get 
            { 
                return new List<KnownUnit>(this.baseUnits.Values).AsReadOnly();
            }
        }

        public IEnumerable<KnownUnit> Units
        {
            get { return this.units.Values; }
        }

        public KnownUnit this[string key]
        {
            get 
            {
                KnownUnit unit;
                this.unitsBySymbol.TryGetValue(key, out unit);
                return unit; ;
            }
        }

        public Unit AddBaseUnit(string symbol, string name, bool inherentPrefix = false)
        {
            var newUnit = this.unitFactory.CreateUnit(this, 1, symbol, name, inherentPrefix);
            EnsureUnitIsNotRegistered(newUnit);

            baseUnits.Add(newUnit.Dimension, newUnit);
            numberOfDimensions++;

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
            // Prefer returning a known unit
            KnownUnit known;

            if (this.units.TryGetValue(Tuple.Create(factor, dimension), out known))
            {
                return known;
            }

            return this.unitFactory.CreateUnit(this, factor, dimension); ;
        }

        public Unit Parse(string unitExpression)
        {
            return this.Interpretor.Parse(unitExpression);
        }

        public string Display(Unit unit)
        {
            Check.UnitsAreFromSameSystem(this.noUnit, unit);

            return this.Interpretor.ToString(unit);
        }

        public Quantity MakeCoherent(Quantity quantity)
        {
            Unit unit = quantity.Unit;

            if (unit.IsCoherent) return quantity;

            return new Quantity(unit.Factor * quantity.Amount, this.coherentUnits[unit.Dimension]);
        }

        private UnitInterpretor Interpretor
        {
            get
            {
                if (this.unitInterpretor == null)
                {
                    this.unitInterpretor = new UnitInterpretor(this, new UnitDialect());
                }

                return this.unitInterpretor;
            }
        }

        private void RegisterKnownUnit(KnownUnit unit)
        {
            units.Add(Tuple.Create(unit.Factor, unit.Dimension), unit);
            unitsBySymbol.Add(unit.Symbol, unit);

            if (unit.Factor.Equals(1))
            {
                this.coherentUnits.Add(unit.Dimension, unit);
            }

            OnUnitAdded(unit);
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

        #region INotifyCollectionChanged
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected void OnUnitAdded(Unit newUnit)
        {
            this.unitInterpretor = null;

            this.CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newUnit));
        }
        #endregion
    }
}
