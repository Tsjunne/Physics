﻿namespace Physics.Parsing
{
    /// <summary>
    ///     Represents a convention for unit representation
    /// </summary>
    /// <remarks>
    ///     The first representation of a token will take precedence for unit display
    /// </remarks>
    public interface IDialect
    {
        Token Division { get; }
        Token Multiplication { get; }
        Token Exponentiation { get; }
    }
}