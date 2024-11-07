using System.Collections.Generic;

namespace Expressions.Operations
{
    public class SubtractOperation : BinaryOperation
    {
        public SubtractOperation(IExpr left, IExpr right) : base(left, right) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            return Left.Compute(variableValues) - Right.Compute(variableValues);
        }

        public override string ToString() => $"({Left} - {Right})";
    }
}
