using System;

namespace Physics.Parsing
{
    public class BinaryOperationExpression : Expression
    {
        public BinaryOperationExpression(Expression leftOperand, Expression rightOperand, Operator @operator) 
        {
            LeftOperand = leftOperand;
            RightOperand = rightOperand;
            Operator = @operator;
        }

        public Expression LeftOperand { get; }
        public Expression RightOperand { get; }
        public Operator Operator { get; }
        public override Quantity Execute()
        {
            var quantity1 = LeftOperand.Execute();
            var quantity2 = RightOperand.Execute();

            switch (Operator)
            {
                case Operator.Plus:
                    return quantity1 + quantity2;
                case Operator.Minus:
                    return quantity1 - quantity2;
                case Operator.Multiplication:
                    return quantity1 * quantity2;
                case Operator.Division:
                    return quantity1 / quantity2;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}