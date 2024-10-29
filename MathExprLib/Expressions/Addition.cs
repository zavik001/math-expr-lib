public class Addition : BinaryOperation
{
    public Addition(IExpr left, IExpr right) : base(left, right) { }

    public override IEnumerable<string> Variables => Left.Variables.Concat(Right.Variables).Distinct();
    public override bool IsConstant => Left.IsConstant && Right.IsConstant;
    public override bool IsPolynomial => Left.IsPolynomial && Right.IsPolynomial;
    public override int PolynomialDegree => Math.Max(Left.PolynomialDegree, Right.PolynomialDegree);

    public override double Compute(IReadOnlyDictionary<string, double> variableValues) =>
        Left.Compute(variableValues) + Right.Compute(variableValues);

    public override IExpr Accept(IExpressionVisitor visitor) => visitor.Visit(this);

    public override string ToString() => $"({Left} + {Right})";
}
