public class SimplificationVisitor : IExpressionVisitor
{
    public IExpr Visit(Constant constant) => constant;

    public IExpr Visit(Variable variable) => variable;

    public IExpr Visit(Addition addition)
    {
        var left = addition.Left.Accept(this);
        var right = addition.Right.Accept(this);
        
        if (left is Constant lc && lc.Value == 0) return right;
        if (right is Constant rc && rc.Value == 0) return left;
        
        return new Addition(left, right);
    }

    public IExpr Visit(Multiplication multiplication)
    {
        var left = multiplication.Left.Accept(this);
        var right = multiplication.Right.Accept(this);
        
        if (left is Constant lc)
        {
            if (lc.Value == 0) return new Constant(0);
            if (lc.Value == 1) return right;
        }
        if (right is Constant rc)
        {
            if (rc.Value == 0) return new Constant(0);
            if (rc.Value == 1) return left;
        }
        
        return new Multiplication(left, right);
    }

    public IExpr Visit(Power power)
    {
        var baseExpr = power.Left.Accept(this);
        var exponent = power.Right.Accept(this);
        
        if (exponent is Constant ec && ec.Value == 0) return new Constant(1);
        if (exponent is Constant ec && ec.Value == 1) return baseExpr;

        return new Power(baseExpr, exponent);
    }

    public IExpr Visit(Logarithm logarithm) => logarithm;
}
