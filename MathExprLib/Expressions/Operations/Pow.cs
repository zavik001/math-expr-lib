using System;
using System.Collections.Generic;

namespace Expressions.Operations
{
    public class Pow : BinaryOperation
    {
        public Pow(IExpr left, IExpr right) : base(left, right) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            return Math.Pow(Left.Compute(variableValues), Right.Compute(variableValues));
        }

        public override int PolynomialDegree => Right.IsConstant ? Left.PolynomialDegree * (int)Right.Compute(new Dictionary<string, double>()) : -1;

        public override string ToString() => $"({Left} ^ {Right})";
    }
}
