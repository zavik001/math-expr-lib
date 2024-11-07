using Expressions.Operations;
using Expressions.Functions;
using Expressions.VariablesAndConstants;

namespace Expressions
{
    public static class MathExpr
    {
        public static ExprBase Sqrt(ExprBase operand) => new Sqrt(operand);
        public static ExprBase Sqrt(double operand) => new Sqrt(new Constant(operand));
        public static ExprBase Sqrt(int operand) => new Sqrt(new Constant(operand));


        public static ExprBase Pow(ExprBase baseValue, ExprBase exponent) => new Pow(baseValue, exponent);
        public static ExprBase Pow(ExprBase baseValue, int exponent) => new Pow(baseValue, new Constant(exponent));
        public static ExprBase Pow(ExprBase baseValue, double exponent) => new Pow(baseValue, new Constant(exponent));
        public static ExprBase Pow(double baseValue, ExprBase exponent) => new Pow(new Constant(baseValue), exponent);
        public static ExprBase Pow(double baseValue, double exponent) => new Pow(new Constant(baseValue), new Constant(exponent));
        public static ExprBase Pow(int baseValue, ExprBase exponent) => new Pow(new Constant(baseValue), exponent);
        public static ExprBase Pow(int baseValue, int exponent) => new Pow(new Constant(baseValue), new Constant(exponent));


        public static ExprBase e(ExprBase operand) => new E(operand);
        public static ExprBase e(double operand) => new E(new Constant(operand));
        public static ExprBase e(int operand) => new E(new Constant(operand));


        public static ExprBase Log(ExprBase baseValue, ExprBase argument) => new Log(baseValue, argument);
        public static ExprBase Log(ExprBase baseValue, double argument) => new Log(baseValue, new Constant(argument));
        public static ExprBase Log(double baseValue, ExprBase argument) => new Log(new Constant(baseValue), argument);
        public static ExprBase Log(double baseValue, double argument) => new Log(new Constant(baseValue), new Constant(argument));
        public static ExprBase Log(ExprBase baseValue, int exponent) => new Log(baseValue, new Constant(exponent));
        public static ExprBase Log(int baseValue, ExprBase argument) => new Log(new Constant(baseValue), argument);
        public static ExprBase Log(int baseValue, int argument) => new Log(new Constant(baseValue), new Constant(argument));


        public static ExprBase Sin(ExprBase operand) => new Sin(operand);
        public static ExprBase Sin(double operand) => new Sin(new Constant(operand));
        public static ExprBase Sin(int operand) => new Sin(new Constant(operand));

        public static ExprBase Cos(ExprBase operand) => new Cos(operand);
        public static ExprBase Cos(double operand) => new Cos(new Constant(operand));
        public static ExprBase Cos(int operand) => new Cos(new Constant(operand));


        public static ExprBase Tan(ExprBase operand) => new Tan(operand);
        public static ExprBase Tan(double operand) => new Tan(new Constant(operand));
        public static ExprBase Tan(int operand) => new Tan(new Constant(operand));
    }
}
