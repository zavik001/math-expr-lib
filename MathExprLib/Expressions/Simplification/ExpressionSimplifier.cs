using System;
using System.Collections.Generic;
using Expressions;
using Expressions.Operations;
using Expressions.VariablesAndConstants;

namespace Expressions.Simplification
{
    public static class ExpressionSimplifier
    {
        public static ExprBase Simplify(ExprBase expression)
        {
            if (expression is Constant constant)
            {
                return constant;
            }

            if (expression is BinaryOperation binaryOperation)
            {
                var left = Simplify((ExprBase)binaryOperation.Left);
                var right = Simplify((ExprBase)binaryOperation.Right);

                if (left is Constant leftConst && right is Constant rightConst)
                {

                    return new Constant(binaryOperation.Compute(new Dictionary<string, double>()));
                }

                if (binaryOperation is MultiplyOperation)
                {
                    if (left is Constant l && l.Value == 1) return right;
                    if (right is Constant r && r.Value == 1) return left;
                    if (left is Constant lZero && lZero.Value == 0 || right is Constant rZero && rZero.Value == 0) return new Constant(0);
                }
                else if (binaryOperation is AddOperation)
                {
                    if (left is Constant l && l.Value == 0) return right;
                    if (right is Constant r && r.Value == 0) return left;
                }
                else if (binaryOperation is SubtractOperation)
                {
                    if (right is Constant r && r.Value == 0) return left;
                }
                else if (binaryOperation is DivideOperation)
                {
                    if (right is Constant r && r.Value == 1) return left;
                }

                var result = Activator.CreateInstance(binaryOperation.GetType(), left, right) as ExprBase;
                return result ?? expression;
            }

            if (expression is UnaryOperation unaryOperation)
            {
                var operand = Simplify((ExprBase)unaryOperation.Operand);

                if (operand is Constant constantOperand)
                {
                    return new Constant(unaryOperation.Compute(new Dictionary<string, double>()));
                }

                var simplifiedUnary = Activator.CreateInstance(unaryOperation.GetType(), operand) as ExprBase;
                return simplifiedUnary ?? expression;
            }

            return expression;
        }
    }
}
