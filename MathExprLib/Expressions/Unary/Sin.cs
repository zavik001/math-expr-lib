using System;
using System.Collections.Generic;

namespace Expressions.Unary
{
    public class Sin : UnaryOperation
    {
        public Sin(IExpr operand) : base(operand) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            return Math.Sin(Operand.Compute(variableValues));
        }

        public override string ToString() => $"sin({Operand})";
    }
}
