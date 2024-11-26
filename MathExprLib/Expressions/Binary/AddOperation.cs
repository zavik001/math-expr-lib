using System.Collections.Generic;

namespace Expressions.Binary
{
    public class AddOperation : BinaryOperation
    {
        public AddOperation(IExpr left, IExpr right) : base(left, right) {}

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            return Left.Compute(variableValues) + Right.Compute(variableValues);
        }

        public override string ToString() => $"({Left} + {Right})";
    }
}
