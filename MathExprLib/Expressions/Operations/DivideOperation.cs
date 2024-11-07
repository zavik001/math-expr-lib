using System.Collections.Generic;

namespace Expressions.Operations
{
    public class DivideOperation : BinaryOperation
    {
        public DivideOperation(IExpr left, IExpr right) : base(left, right) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            double rightValue = Right.Compute(variableValues);
            if (rightValue == 0)
            {
                throw new DivideByZeroException("Деление на ноль.");
            }
            return Left.Compute(variableValues) / rightValue;
        }

        public override int PolynomialDegree => Left.PolynomialDegree - Right.PolynomialDegree;

        public override string ToString() => $"({Left} / {Right})";
    }
}
