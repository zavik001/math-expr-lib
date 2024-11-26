using System;
using System.Collections.Generic;
using Expressions;
using Expressions.Binary;
using Expressions.Unary;
using Expressions.VariablesAndConstants;

namespace Expressions.Simplification
{
    public static class ExpressionSimplifier
    {
        public static ExprBase Simplify(ExprBase expression)
        {
            return expression switch
            {
                Constant constant => constant,

                BinaryOperation binaryOperation => SimplifyBinary(binaryOperation),

                UnaryOperation unaryOperation => SimplifyUnary(unaryOperation),

                _ => expression
            };
        }

        private static ExprBase SimplifyBinary(BinaryOperation binaryOperation)
        {
            var left = Simplify((ExprBase)binaryOperation.Left);
            var right = Simplify((ExprBase)binaryOperation.Right);

            // Если оба операнда константы, возвращаем вычисленный результат
            if (left is Constant leftConst && right is Constant rightConst)
            {
                return new Constant(binaryOperation.Compute(new Dictionary<string, double>()));
            }

            return binaryOperation switch
            {
                MultiplyOperation => SimplifyMultiply(left, right),
                AddOperation => SimplifyAdd(left, right),
                SubtractOperation => SimplifySubtract(left, right),
                DivideOperation => SimplifyDivide(left, right),
                _ => CreateBinary(binaryOperation, left, right)
            };
        }

        private static ExprBase SimplifyMultiply(ExprBase left, ExprBase right) =>
            (left, right) switch
            {
                (Constant { Value: 1 }, var nonConst) => nonConst,        // 1 * x = x
                (var nonConst, Constant { Value: 1 }) => nonConst,        // x * 1 = x
                (Constant { Value: 0 }, _) => new Constant(0),            // 0 * x = 0
                (_, Constant { Value: 0 }) => new Constant(0),            // x * 0 = 0
                _ => CreateBinaryOperation<MultiplyOperation>(left, right)
            };

        private static ExprBase SimplifyAdd(ExprBase left, ExprBase right) =>
            (left, right) switch
            {
                (Constant { Value: 0 }, var nonConst) => nonConst,        // 0 + x = x
                (var nonConst, Constant { Value: 0 }) => nonConst,        // x + 0 = x
                _ => CreateBinaryOperation<AddOperation>(left, right)
            };

        private static ExprBase SimplifySubtract(ExprBase left, ExprBase right) =>
            right switch
            {
                Constant { Value: 0 } => left,                            // x - 0 = x
                _ => CreateBinaryOperation<SubtractOperation>(left, right)
            };

        private static ExprBase SimplifyDivide(ExprBase left, ExprBase right) =>
            right switch
            {
                Constant { Value: 1 } => left,                            // x / 1 = x
                _ => CreateBinaryOperation<DivideOperation>(left, right)
            };

        private static ExprBase SimplifyUnary(UnaryOperation unaryOperation)
        {
            var operand = Simplify((ExprBase)unaryOperation.Operand);

            // Если операнд — константа, вычисляем результат
            if (operand is Constant constantOperand)
            {
                return new Constant(unaryOperation.Compute(new Dictionary<string, double>()));
            }

            return CreateUnaryOperation(unaryOperation.GetType(), operand);
        }

        private static ExprBase CreateBinary(BinaryOperation binaryOperation, ExprBase left, ExprBase right) =>
            Activator.CreateInstance(binaryOperation.GetType(), left, right) as ExprBase ?? binaryOperation;

        private static ExprBase CreateBinaryOperation<T>(ExprBase left, ExprBase right) where T : BinaryOperation =>
            Activator.CreateInstance(typeof(T), left, right) as ExprBase ?? throw new InvalidOperationException();

        private static ExprBase CreateUnaryOperation(Type operationType, ExprBase operand) =>
            Activator.CreateInstance(operationType, operand) as ExprBase ?? throw new InvalidOperationException();
    }
}
