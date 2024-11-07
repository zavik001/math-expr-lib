using System.Collections.Generic;

namespace Expressions.Operations
{
    public class MultiplyOperation : BinaryOperation
    {
        public MultiplyOperation(IExpr left, IExpr right) : base(left, right) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            return Left.Compute(variableValues) * Right.Compute(variableValues);
        }

        public override int PolynomialDegree => Left.IsPolynomial && Right.IsPolynomial ? Left.PolynomialDegree + Right.PolynomialDegree : -1;

        public override string ToString() => $"({Left} * {Right})";
    }
}
