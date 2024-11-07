using System;
using System.Collections.Generic;
using Expressions;

namespace Expressions.Operations
{

    public class Sqrt : UnaryOperation
    {
        public Sqrt(IExpr operand) : base(operand) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            double value = Operand.Compute(variableValues);

            if (value < 0)
                throw new ArgumentException("Квадратный корень из отрицательного числа не существует.");
            return Math.Sqrt(value);
        }

        public override int PolynomialDegree => 0;

        public override string ToString() => $"sqrt({Operand})";
    }
}
