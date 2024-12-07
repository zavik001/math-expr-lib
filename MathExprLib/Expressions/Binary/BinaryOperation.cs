using Expressions.VariablesAndConstants;
using System;
using System.Collections.Generic;

namespace Expressions.Binary
{
    public abstract class BinaryOperation : ExprBase
    {
        public IExpr Left { get; }
        public IExpr Right { get; }

        public BinaryOperation(IExpr left, IExpr right)
        {
            Left = left;
            Right = right;
        }

        public override IEnumerable<string> Variables
        {
            get
            {
                var variables = new HashSet<string>(Left.Variables);
                variables.UnionWith(Right.Variables);
                return variables;
            }
        }

        public override bool IsConstant => Left.IsConstant && Right.IsConstant;
        public override bool IsPolynomial => Left.IsPolynomial && Right.IsPolynomial;
        public override int PolynomialDegree => Math.Max(Left.PolynomialDegree, Right.PolynomialDegree);
    }
}
