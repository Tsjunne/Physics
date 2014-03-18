using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics.Presentation
{
    internal class UnitDialect : IUnitDialect
    {
        public Token Division 
        {
            get { return new Token("/"); }
        }

        public Token Multiplication
        {
            get { return new Token(" ", "·", "×" ); }
        }

        public Token Exponentiation
        {
            get { return new Token("^"); }
        }
    }
}
