using System;
using System.Collections.Generic;

namespace Expressions.Binary
{
    public class Pow : BinaryOperation
    {
        public Pow(IExpr left, IExpr right) : base(left, right) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            return Math.Pow(Left.Compute(variableValues), Right.Compute(variableValues));
        }

        public override bool IsPolynomial
        {
            get
            {
                if (!Left.IsPolynomial || !Right.IsConstant)
                    return false;

                double exponent = Right.Compute(new Dictionary<string, double>());
                return exponent >= 0 && exponent == (int)exponent;
            }
        }

        public override int PolynomialDegree
        {
            get
            {
                if (!IsPolynomial)
                    return -1;

                return Left.PolynomialDegree * (int)Right.Compute(new Dictionary<string, double>());
            }
        }

        public override string ToString() => $"({Left} ^ {Right})";
    }
}
