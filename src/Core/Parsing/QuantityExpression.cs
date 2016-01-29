namespace Physics.Parsing
{
    public class QuantityExpression : Expression
    {
        private readonly double _amount;
        private readonly Unit _unit;

        public QuantityExpression(double amount, Unit unit)
        {
            _amount = amount;
            _unit = unit;
        }

        public override Quantity Execute()
        {
            return new Quantity(_amount, _unit);
        }
    }
}