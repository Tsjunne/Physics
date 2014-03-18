using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    internal class DerivedUnit : Unit
    {
        internal DerivedUnit(IUnitSystem system, double factor, Dimension dimension)
            : base(system, factor, dimension)
        {
        }
    }
}
