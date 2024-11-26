using System.Collections.Generic;

namespace Expressions.Unary
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
        public override bool IsPolynomial => false;
        public override int PolynomialDegree => -1;

        public abstract override double Compute(IReadOnlyDictionary<string, double> variableValues);
        public abstract override string ToString();
    }
}
