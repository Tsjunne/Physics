using Physics.Presentation;
using Physics.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
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
        private readonly Dictionary<Dimension, KnownUnit> coherentUnits;
        private readonly Dictionary<Unit, KnownUnit> units;
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
            this.unitFactory = unitFactory;
            this.dialect = dialect;
            this.Name = name;
            this.baseUnits = new Dictionary<Dimension, KnownUnit>();
            this.coherentUnits = new Dictionary<Dimension, KnownUnit>();
            this.units = new Dictionary<Unit, KnownUnit>();
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

        public IEnumerable<KnownUnit> BaseUnits 
        {
            get { return this.baseUnits.Values; }
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
            return this.AddBaseUnit(symbol, name, 1, inherentPrefix);
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
            var unit = this.unitFactory.CreateUnit(this, factor, dimension);

            if (this.units.TryGetValue(unit, out known))
            {
                return known;
            }

            return unit;
        }

        public Unit Parse(string unitExpression)
        {
            EnsureInterpretor();

            return this.unitInterpretor.Parse(unitExpression);
        }

        public string Display(Unit unit)
        {
            EnsureInterpretor();
            Check.SystemKnowsDimension(this, unit.Dimension);

            return this.unitInterpretor.ToString(unit);
        }

        public Quantity MakeCoherent(Quantity quantity)
        {
            Unit originalUnit = quantity.Unit;

            if (originalUnit.IsCoherent) return quantity;

            Unit coherentUnit;

            if (this.coherentUnits.ContainsKey(originalUnit.Dimension))
            {
                coherentUnit = this.coherentUnits[originalUnit.Dimension];
            }
            else
            {
                coherentUnit = this.unitFactory.CreateUnit(this, 1, originalUnit.Dimension);
            }

            return new Quantity(originalUnit.Factor * quantity.Amount, coherentUnit);
        }

        private void EnsureInterpretor()
        {
            if (this.unitInterpretor == null)
            {
                this.unitInterpretor = new UnitInterpretor(this, new UnitDialect());
            }
        }

        private Unit AddBaseUnit(string symbol, string name, double factor, bool inherentPrefix)
        {
            var newUnit = this.unitFactory.CreateUnit(this, factor, symbol, name, inherentPrefix);
            EnsureUnitIsNotRegistered(newUnit);

            baseUnits.Add(newUnit.Dimension, newUnit);
            numberOfDimensions++;

            RegisterKnownUnit(newUnit);

            return newUnit;
        }

        private void RegisterKnownUnit(KnownUnit unit)
        {
            units.Add(unit, unit);
            unitsBySymbol.Add(unit.Symbol, unit);

            if (unit.Factor == 1)
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

            if (this.units.TryGetValue(unit, out collision))
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

            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newUnit));
            }
        }
        #endregion
    }
}
