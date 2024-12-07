using System;
using System.Collections.Generic;
using Expressions.VariablesAndConstants;

namespace Expressions.Binary
{
    public class DivideOperation : BinaryOperation
    {
        public DivideOperation(IExpr left, IExpr right) : base(left, right) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            double rightValue = Right.Compute(variableValues);
            if (rightValue == 0)
            {
                throw new DivideByZeroException("Деление на ноль.");
            }
            return Left.Compute(variableValues) / rightValue;
        }

        public override bool IsPolynomial
        {
            get
            {
                return ArePolynomialsDivisible(out _);
            }
        }

        public override int PolynomialDegree
        {
            get
            {
                if (!ArePolynomialsDivisible(out var degreeDifference))
                {
                    return -1;
                }
                return degreeDifference;
            }
        }

        public override bool IsConstant => Left.IsConstant && Right.IsConstant;

        public override string ToString() => $"({Left} / {Right})";

        /// <summary>
        /// Проверяет, можно ли выразить деление как полином, и возвращает разницу степеней, если возможно.
        /// </summary>
        private bool ArePolynomialsDivisible(out int degreeDifference)
        {
            degreeDifference = -1;

            // Если числитель или знаменатель не являются полиномами, результат тоже не полином.
            if (!Left.IsPolynomial || !Right.IsPolynomial)
            {
                return false;
            }

            // Собираем степени переменных
            var numeratorPowers = GetVariablePowers(Left);
            var denominatorPowers = GetVariablePowers(Right);

            // Проверяем делимость переменных
            foreach (var variable in denominatorPowers.Keys)
            {
                if (!numeratorPowers.ContainsKey(variable) ||
                    numeratorPowers[variable] < denominatorPowers[variable])
                {
                    return false;
                }
            }

            // Если деление возможно, вычисляем разницу степеней
            degreeDifference = Left.PolynomialDegree - Right.PolynomialDegree;
            return true;
        }

        private Dictionary<string, int> GetVariablePowers(IExpr expr)
        {
            var variablePowers = new Dictionary<string, int>();

            // Рекурсивный анализ выражения
            if (expr is Variable variable)
            {
                variablePowers[variable.Name] = 1;
            }
            else if (expr is MultiplyOperation multiply)
            {
                var leftPowers = GetVariablePowers(multiply.Left);
                var rightPowers = GetVariablePowers(multiply.Right);

                foreach (var kvp in leftPowers)
                {
                    if (!variablePowers.ContainsKey(kvp.Key))
                        variablePowers[kvp.Key] = kvp.Value;
                }

                foreach (var kvp in rightPowers)
                {
                    if (variablePowers.ContainsKey(kvp.Key))
                        variablePowers[kvp.Key] += kvp.Value;
                    else
                        variablePowers[kvp.Key] = kvp.Value;
                }
            }
            else if (expr is Pow pow)
            {
                var basePowers = GetVariablePowers(pow.Left);
                int exponent = (int)pow.Right.Compute(new Dictionary<string, double>());

                foreach (var kvp in basePowers)
                {
                    variablePowers[kvp.Key] = kvp.Value * exponent;
                }
            }

            return variablePowers;
        }
    }
}
