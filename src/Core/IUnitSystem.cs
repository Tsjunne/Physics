using System.Collections.Generic;

namespace Physics
{
    /// <summary>
    ///     Represents a system of units, based on a given set of base units.
    /// </summary>
    public interface IUnitSystem : IEnumerable<KnownUnit>
    {
        /// <summary>
        ///     System name
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Number of base dimensions or number of base units in the system.
        /// </summary>
        int NumberOfDimensions { get; }

        /// <summary>
        ///     The base units
        /// </summary>
        IEnumerable<KnownUnit> BaseUnits { get; }

        /// <summary>
        ///     Null object
        /// </summary>
        Unit NoUnit { get; }

        /// <summary>
        ///     Gets a unit by symbol
        /// </summary>
        /// <param name="symbol">the symbol</param>
        /// <returns>The unit</returns>
        KnownUnit this[string symbol] { get; }

        /// <summary>
        ///     Adds a base unit to the system
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="name">The name</param>
        /// <param name="inherentPrefix"></param>
        /// <returns>The base units</returns>
        Unit AddBaseUnit(string symbol, string name, bool inherentPrefix = false);

        /// <summary>
        ///     Adds named derived unit to the system
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="name">The name</param>
        /// <param name="unit">The unit to base the new unit on</param>
        /// <returns>The known unit</returns>
        Unit AddDerivedUnit(string symbol, string name, Unit unit);

        /// <summary>
        ///     Creates a unit with the specified factor and dimension
        /// </summary>
        /// <param name="factor">The factor</param>
        /// <param name="dimension">The dimension</param>
        /// <returns>The unit</returns>
        Unit CreateUnit(double factor, Dimension dimension);

        /// <summary>
        ///     Parses a unit expression
        /// </summary>
        /// <param name="unitExpression">The expression</param>
        /// <returns>The parsed unit</returns>
        Unit Parse(string unitExpression);

        /// <summary>
        ///     Displays a unit as a string
        /// </summary>
        /// <param name="unit">The unit</param>
        /// <returns>The string representation</returns>
        string Display(Unit unit);

        /// <summary>
        ///     Makes a quantity coherent. Expressed in a unit that contains no factor to a base unit.
        /// </summary>
        /// <param name="quantity">The quantity</param>
        /// <returns>The coherent quantity</returns>
        Quantity MakeCoherent(Quantity quantity);
    }
}