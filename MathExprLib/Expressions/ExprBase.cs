using System.Collections.Generic;
using Expressions.Binary;
using Expressions.Unary;
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

        public static implicit operator ExprBase(int value) => new Constant(value);
        public static implicit operator ExprBase(double value) => new Constant(value);

        public static ExprBase operator -(ExprBase operand) => new NegateOperation(operand);
    }
}
