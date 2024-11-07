using Expressions.Operations;
using System.Collections.Generic;

namespace Expressions.VariablesAndConstants
{
    public class Constant : ExprBase
    {
        public double Value { get; }

        public Constant(double value) => Value = value;

        public override IEnumerable<string> Variables => new string[0];
        public override bool IsConstant => true;
        public override bool IsPolynomial => true;
        public override int PolynomialDegree => 0;

        public override double Compute(IReadOnlyDictionary<string, double> variableValues) => Value;

        public override string ToString() => Value.ToString();
    }
}
