using System;

namespace Physics
{
    internal static class Check
    {
        public static void UnitsAreFromSameSystem(Unit unit1, Unit unit2)
        {
            if (!unit1.HasSameSystem(unit2))
            {
                throw new InvalidOperationException(Messages.UnitsNotSameSystem);
            }
        }

        public static void UnitsAreSameDimension(Unit unit1, Unit unit2)
        {
            if (!unit1.HasSameDimension(unit2))
            {
                throw new InvalidOperationException(Messages.UnitsNotSameDimension);
            }
        }

        public static void SystemKnowsDimension(IUnitSystem system, Dimension dimension)
        {
            if (system.NumberOfDimensions < dimension.Count)
            {
                throw new InvalidOperationException(Messages.DimensionUnknown.FormatWith(system.Name,
                    system.NumberOfDimensions));
            }
        }

        public static ArgumentCheck<T> Argument<T>(T argument, string paramName)
        {
            return new ArgumentCheck<T>(argument, paramName);
        }

        internal class ArgumentCheck<T>
        {
            private readonly T _argument;
            private readonly string _paramName;

            public ArgumentCheck(T argument, string paramName)
            {
                _argument = argument;
                _paramName = paramName;
            }

            public void IsNotNull()
            {
                if (_argument == null || (_argument is string && string.IsNullOrEmpty(_argument as string)))
                {
                    throw new ArgumentNullException(_paramName);
                }
            }
        }
    }
}