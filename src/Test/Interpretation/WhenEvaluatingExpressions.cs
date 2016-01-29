using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Physics.Parsing;

namespace Physics.Test.Interpretation
{
    [TestClass]
    public class WhenEvaluatingExpressions : GivenSiSystem
    {
        private NumberFormatInfo _formatInfo;
        private ExpressionParser _subject;

        [TestInitialize]
        public void Setup()
        {
            _formatInfo = NumberFormatInfo.CurrentInfo;
            _subject = new ExpressionParser(System, new Dialect(), _formatInfo);
        }

        [TestMethod]
        public void ThenConstantValuesCanBeEvaluated()
        {
            var expected = 1234.56d;
            var result = Evaluate(expected.ToString(_formatInfo));

            Assert.IsNotNull(result);
            Assert.IsTrue(result is QuantityExpression);
            Assert.AreEqual(expected, result.Execute().Amount);

        }

        private Expression Evaluate(string expression)
        {
            using (var reader = new StringReader(expression))
            {
                return _subject.Parse(reader);
            }
        }
    }
}
