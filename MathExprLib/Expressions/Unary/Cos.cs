using System;
using System.Collections.Generic;

namespace Expressions.Unary
{
    public class Cos : UnaryOperation
    {
        public Cos(IExpr operand) : base(operand) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            return Math.Cos(Operand.Compute(variableValues));
        }

        public override string ToString() => $"cos({Operand})";
    }
}
