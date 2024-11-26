using System.Collections.Generic;
using Expressions.VariablesAndConstants;

namespace Expressions.Binary
{
    public class MultiplyOperation : BinaryOperation
    {
        public MultiplyOperation(IExpr left, IExpr right) : base(left, right) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            return Left.Compute(variableValues) * Right.Compute(variableValues);
        }

        public override bool IsPolynomial
        {
            get
            {
                if (Left.IsPolynomial && Right.IsPolynomial)
                    return true;

                return false;
            }
        }

        public override int PolynomialDegree
        {
            get
            {
                if (!IsPolynomial)
                    return -1;

                return Left.PolynomialDegree + Right.PolynomialDegree;
            }
        }

        public override string ToString() => $"({Left} * {Right})";
    }
}
