using System.Collections.Generic;

namespace Expressions
{
    public abstract class UnaryOperation : ExprBase
    {
        public IExpr Operand { get; }

        public UnaryOperation(IExpr operand)
        {
            Operand = operand;
        }

        public override IEnumerable<string> Variables => Operand.Variables;
        public override bool IsConstant => Operand.IsConstant;
        public override bool IsPolynomial => Operand.IsPolynomial;
        public override int PolynomialDegree => Operand.PolynomialDegree;

        public abstract override double Compute(IReadOnlyDictionary<string, double> variableValues);
        public abstract override string ToString();
    }
}
