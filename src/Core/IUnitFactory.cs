using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Physics
{
    internal interface IUnitFactory
    {
        KnownUnit CreateUnit(IUnitSystem system, double factor, string symbol, string name, bool inherentPrefix);
        KnownUnit CreateUnit(IUnitSystem system, double factor, Dimension dimension, string symbol, string name, bool inherentPrefix);
        Unit CreateUnit(IUnitSystem system, double factor, Dimension dimension);
    }
}
