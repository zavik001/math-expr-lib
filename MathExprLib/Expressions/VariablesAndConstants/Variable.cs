using Expressions.Operations;
using System;
using System.Collections.Generic;

namespace Expressions.VariablesAndConstants
{
    public class Variable : ExprBase
    {
        public string Name { get; }

        public Variable(string name)
        {
            Name = name;
        }

        public override IEnumerable<string> Variables => new[] { Name };
        public override bool IsConstant => false;
        public override bool IsPolynomial => true;
        public override int PolynomialDegree => 1;

        public override double Compute(IReadOnlyDictionary<string, double> variableValues) =>
            variableValues.TryGetValue(Name, out var value) ? value : throw new ArgumentException($"Variable {Name} этого значения не имеет.");

        public override string ToString() => Name;
    }
}
