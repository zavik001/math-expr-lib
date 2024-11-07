using System;
using System.Collections.Generic;

namespace Expressions.Functions
{
    public class E : UnaryOperation
    {
        public E(IExpr operand) : base(operand) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            return Math.Exp(Operand.Compute(variableValues));
        }

        public override int PolynomialDegree => 0;
        public override string ToString() => $"exp({Operand})";
    }
}
