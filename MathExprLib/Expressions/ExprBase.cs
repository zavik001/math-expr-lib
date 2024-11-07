using System.Collections.Generic;
using Expressions.Operations;
using Expressions.VariablesAndConstants;

namespace Expressions
{
    public abstract class ExprBase : IExpr
    {
        public abstract IEnumerable<string> Variables { get; }
        public abstract bool IsConstant { get; }
        public abstract bool IsPolynomial { get; }
        public abstract int PolynomialDegree { get; }
        public abstract double Compute(IReadOnlyDictionary<string, double> variableValues);

        public static ExprBase operator +(ExprBase left, ExprBase right) => new AddOperation(left, right);
        public static ExprBase operator -(ExprBase left, ExprBase right) => new SubtractOperation(left, right);
        public static ExprBase operator *(ExprBase left, ExprBase right) => new MultiplyOperation(left, right);
        public static ExprBase operator /(ExprBase left, ExprBase right) => new DivideOperation(left, right);

        public static ExprBase operator +(ExprBase left, int right) => new AddOperation(left, new Constant(right));
        public static ExprBase operator -(ExprBase left, int right) => new SubtractOperation(left, new Constant(right));
        public static ExprBase operator *(ExprBase left, int right) => new MultiplyOperation(left, new Constant(right));
        public static ExprBase operator /(ExprBase left, int right) => new DivideOperation(left, new Constant(right));

        public static ExprBase operator +(int left, ExprBase right) => new AddOperation(new Constant(left), right);
        public static ExprBase operator -(int left, ExprBase right) => new SubtractOperation(new Constant(left), right);
        public static ExprBase operator *(int left, ExprBase right) => new MultiplyOperation(new Constant(left), right);
        public static ExprBase operator /(int left, ExprBase right) => new DivideOperation(new Constant(left), right);

        public static ExprBase operator +(ExprBase left, double right) => new AddOperation(left, new Constant(right));
        public static ExprBase operator -(ExprBase left, double right) => new SubtractOperation(left, new Constant(right));
        public static ExprBase operator *(ExprBase left, double right) => new MultiplyOperation(left, new Constant(right));
        public static ExprBase operator /(ExprBase left, double right) => new DivideOperation(left, new Constant(right));

        public static ExprBase operator +(double left, ExprBase right) => new AddOperation(new Constant(left), right);
        public static ExprBase operator -(double left, ExprBase right) => new SubtractOperation(new Constant(left), right);
        public static ExprBase operator *(double left, ExprBase right) => new MultiplyOperation(new Constant(left), right);
        public static ExprBase operator /(double left, ExprBase right) => new DivideOperation(new Constant(left), right);
    }
}
