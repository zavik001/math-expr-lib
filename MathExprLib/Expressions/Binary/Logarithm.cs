using System;
using System.Collections.Generic;

namespace Expressions.Binary
{
    public class Log : BinaryOperation
    {
        public Log(IExpr baseValue, IExpr argument) : base(baseValue, argument) { }

        public override double Compute(IReadOnlyDictionary<string, double> variableValues)
        {
            double baseVal = Left.Compute(variableValues);
            double argVal = Right.Compute(variableValues);

            if (baseVal <= 0 || baseVal == 1)
                throw new ArgumentException("Основание логарифма должно быть положительным и не равно 1.");
            if (argVal <= 0)
                throw new ArgumentException("Аргумент логарифма должен быть положительным.");

            return Math.Log(argVal, baseVal);
        }

        public override string ToString() => $"log_{Left}({Right})";
    }
}
