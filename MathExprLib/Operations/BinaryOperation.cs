public abstract class BinaryOperation : IExpr
{
    protected IExpr Left;
    protected IExpr Right;
    
    protected BinaryOperation(IExpr left, IExpr right)
    {
        Left = left;
        Right = right;
    }

    public abstract IEnumerable<string> Variables { get; }
    public abstract bool IsConstant { get; }
    public abstract bool IsPolynomial { get; }
    public abstract int PolynomialDegree { get; }
    public abstract double Compute(IReadOnlyDictionary<string, double> variableValues);
    public abstract IExpr Accept(IExpressionVisitor visitor);
}
