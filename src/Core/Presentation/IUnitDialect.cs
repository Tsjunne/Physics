using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Physics.Presentation
{
    /// <summary>
    /// Represents a convention for unit representation
    /// </summary>
    /// <remarks>
    /// The first representation of a token will take precedence for unit display
    /// </remarks>
    public interface IUnitDialect
    {
        Token Division { get; }
        Token Multiplication { get; }
        Token Exponentiation { get; }
    }
}
