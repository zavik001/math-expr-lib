using System;
using System.Collections.Generic;

namespace Expressions.Functions
{
    public class Tan : UnaryOperation
    {
        public Tan(IExpr operand) : base(operand) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            return Math.Tan(Operand.Compute(variableValues));
        }

        public override int PolynomialDegree => 0;

        public override string ToString() => $"tan({Operand})";
    }
}
