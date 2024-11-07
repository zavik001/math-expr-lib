using System;
using System.Collections.Generic;

namespace Expressions.Functions
{
    public class Cos : UnaryOperation
    {
        public Cos(IExpr operand) : base(operand) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            return Math.Cos(Operand.Compute(variableValues));
        }

        public override int PolynomialDegree => 0;

        public override string ToString() => $"cos({Operand})";
    }
}
