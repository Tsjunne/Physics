using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics
{
    public static class UnitSystemFactory
    {
        public static IUnitSystem CreateSystem(string name)
        {
            return new UnitSystem(name);
        }
    }
}
