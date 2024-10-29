public class Logarithm : UnaryOperation
{
    private readonly double _base;

    public Logarithm(IExpr operand, double baseValue = Math.E) : base(operand)
    {
        _base = baseValue;
    }

    public override IEnumerable<string> Variables => Operand.Variables;
    public override bool IsConstant => Operand.IsConstant;
    public override bool IsPolynomial => false;
    public override int PolynomialDegree => 0;

    public override double Compute(IReadOnlyDictionary<string, double> variableValues) =>
        Math.Log(Operand.Compute(variableValues), _base);

    public override IExpr Accept(IExpressionVisitor visitor) => visitor.Visit(this);

    public override string ToString() => _base == Math.E ? $"ln({Operand})" : $"log_{_base}({Operand})";
}
