public interface IExpressionVisitor
{
    IExpr Visit(Constant constant);
    IExpr Visit(Variable variable);
    IExpr Visit(Addition addition);
    IExpr Visit(Multiplication multiplication);
    IExpr Visit(Power power);
    IExpr Visit(Logarithm logarithm);
}
