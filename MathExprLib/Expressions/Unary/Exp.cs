using System;
using System.Collections.Generic;

namespace Expressions.Unary
{
    public class Exp : UnaryOperation
    {
        public Exp(IExpr operand) : base(operand) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            return Math.Exp(Operand.Compute(variableValues));
        }

        public override string ToString() => $"exp({Operand})";
    }
}
