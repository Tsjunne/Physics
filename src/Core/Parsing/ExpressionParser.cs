using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Physics.Parsing
{
    internal class ExpressionParser
    {
        private readonly IUnitSystem _system;
        private readonly IDialect _dialect;
        private readonly NumberFormatInfo _numberFormat;

        public ExpressionParser(IUnitSystem system, IDialect dialect, NumberFormatInfo numberFormat)
        {
            _system = system;
            _dialect = dialect;
            _numberFormat = numberFormat;
        }

        public Expression Parse(TextReader expressionStream)
        {
            do
            {
                var c = (char)expressionStream.Peek();

                if (char.IsDigit(c))
                {
                    var constantString = CollectConstant(expressionStream);
                    return new QuantityExpression(double.Parse(constantString), _system.NoUnit);
                }

            } while (expressionStream.Peek() != -1);

            return null;
        }

        private string CollectConstant(TextReader expression)
        {
            var builder = new StringBuilder();
            var c = (char)expression.Peek();

            while (IsConstantCharacter(c))
            {
                c = (char) expression.Read();
                builder.Append(c);
                c = (char)expression.Peek();
            }

            return builder.ToString();
        }

        private bool IsConstantCharacter(char c)
        {
            return
                char.IsDigit(c) ||
                _numberFormat.NumberDecimalSeparator == c.ToString() ||
                _numberFormat.NumberGroupSeparator == c.ToString();
        }
    }
}
