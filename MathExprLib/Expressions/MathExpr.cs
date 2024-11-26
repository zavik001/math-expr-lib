using Expressions.Binary;
using Expressions.Unary;
using Expressions.VariablesAndConstants;

namespace Expressions
{
    public static class MathExpr
    {
        public static ExprBase Sqrt(ExprBase operand) => new Sqrt(operand);

        public static ExprBase Pow(ExprBase baseValue, ExprBase exponent) => new Pow(baseValue, exponent);

        public static ExprBase Exp(ExprBase operand) => new Exp(operand);

        public static ExprBase Log(ExprBase baseValue, ExprBase argument) => new Log(baseValue, argument);

        public static ExprBase Sin(ExprBase operand) => new Sin(operand);

        public static ExprBase Cos(ExprBase operand) => new Cos(operand);

        public static ExprBase Tan(ExprBase operand) => new Tan(operand);
    }
}
