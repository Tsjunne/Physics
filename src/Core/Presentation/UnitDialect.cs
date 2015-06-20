using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics.Presentation
{
    internal class UnitDialect : IUnitDialect
    {
        public Token Division => new Token("/");
        public Token Multiplication => new Token(" ", "·", "×" );
        public Token Exponentiation => new Token("^");
    }
}
