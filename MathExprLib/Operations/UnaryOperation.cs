public abstract class UnaryOperation : IExpr
{
    protected IExpr Operand;
    
    protected UnaryOperation(IExpr operand) => Operand = operand;

    public abstract IEnumerable<string> Variables { get; }
    public abstract bool IsConstant { get; }
    public abstract bool IsPolynomial { get; }
    public abstract int PolynomialDegree { get; }
    public abstract double Compute(IReadOnlyDictionary<string, double> variableValues);
    public abstract IExpr Accept(IExpressionVisitor visitor);
}
