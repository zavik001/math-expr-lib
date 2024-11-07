using System;
using System.Collections.Generic;

namespace Expressions.Functions
{
    public class Sin : UnaryOperation
    {
        public Sin(IExpr operand) : base(operand) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            return Math.Sin(Operand.Compute(variableValues));
        }

        public override int PolynomialDegree => 0;

        public override string ToString() => $"sin({Operand})";
    }
}
