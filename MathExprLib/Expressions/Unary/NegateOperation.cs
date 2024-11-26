using System.Collections.Generic;

namespace Expressions.Unary
{
    public class NegateOperation : UnaryOperation
    {
        public NegateOperation(IExpr operand) : base(operand) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            return -Operand.Compute(variableValues);
        }
        public override bool IsPolynomial => Operand.IsPolynomial;
        public override int PolynomialDegree => Operand.PolynomialDegree;

        public override string ToString() => $"-({Operand})";
    }
}
