public class Constant : IExpr
{
    public double Value { get; }

    public Constant(double value) => Value = value;

    public IEnumerable<string> Variables => Enumerable.Empty<string>();
    public bool IsConstant => true;
    public bool IsPolynomial => true;
    public int PolynomialDegree => 0;
    
    public double Compute(IReadOnlyDictionary<string, double> variableValues) => Value;

    public IExpr Accept(IExpressionVisitor visitor) => visitor.Visit(this);

    public override string ToString() => Value.ToString();
}
