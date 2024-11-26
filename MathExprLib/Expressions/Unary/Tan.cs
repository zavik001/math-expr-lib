using System;
using System.Collections.Generic;

namespace Expressions.Unary
{
    public class Tan : UnaryOperation
    {
        public Tan(IExpr operand) : base(operand) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            return Math.Tan(Operand.Compute(variableValues));
        }

        public override string ToString() => $"tan({Operand})";
    }
}
