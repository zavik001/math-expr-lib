public class Power : BinaryOperation
{
    public Power(IExpr left, IExpr right) : base(left, right) { }

    public override IEnumerable<string> Variables => Left.Variables.Concat(Right.Variables).Distinct();
    public override bool IsConstant => Left.IsConstant && Right.IsConstant;
    public override bool IsPolynomial => Right.IsConstant && Right.Compute(null) % 1 == 0; // Целая степень
    public override int PolynomialDegree => IsPolynomial ? (int)(Right.Compute(null)) * Left.PolynomialDegree : 0;

    public override double Compute(IReadOnlyDictionary<string, double> variableValues) =>
        Math.Pow(Left.Compute(variableValues), Right.Compute(variableValues));

    public override IExpr Accept(IExpressionVisitor visitor) => visitor.Visit(this);

    public override string ToString() => $"({Left}^{Right})";
}
