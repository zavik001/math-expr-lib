using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Expressions;
using Expressions.Simplification;
using Expressions.VariablesAndConstants;
using Expressions.Operations;
using static Expressions.MathExpr;

namespace Expressions.Tests
{
    public class MathExprTests
    {
        [Fact]
        public void Expr1_EvaluationTests()
        {
            // Arrange
            var x = new Variable("x");
            var y = new Variable("y");
            var expr1 = (x - 4) * (3 * x + y * y) / 5;

            // Act & Assert
            // Проверка строки выражения
            expr1.ToString().Should().Be("(((x - 4) * ((3 * x) + (y * y))) / 5)");

            // Проверка переменных
            expr1.Variables.Should().BeEquivalentTo(new[] { "x", "y" });

            // Проверка на константу
            expr1.IsConstant.Should().BeFalse();

            // Проверка, является ли полиномом
            expr1.IsPolynomial.Should().BeTrue();

            // Проверка степени полинома
            expr1.PolynomialDegree.Should().Be(3);

            // Проверка вычисления значения
            var variableValues = new Dictionary<string, double> { ["x"] = 1, ["y"] = 2 };
            expr1.Compute(variableValues).Should().BeApproximately(-4.2, 1e-5);
        }

        [Fact]
        public void Expr2_EvaluationTests()
        {
            // Arrange
            var c = new Constant(3);
            var expr2 = (5 - 3 * c) * Sqrt(16 + c * c);

            // Act & Assert
            // Проверка строки выражения
            expr2.ToString().Should().Contain("((5 - (3 * 3)) * sqrt((16 + (3 * 3))))");

            // Проверка переменных
            expr2.Variables.Should().BeEmpty();

            // Проверка на константу
            expr2.IsConstant.Should().BeTrue();

            // Проверка, является ли полиномом
            expr2.IsPolynomial.Should().BeTrue();

            // Проверка степени полинома
            expr2.PolynomialDegree.Should().Be(0);

            // Проверка вычисления значения
            var variableValues = new Dictionary<string, double> { ["x"] = 1, ["y"] = 2 };
            expr2.Compute(variableValues).Should().Be(-20);
        }

        [Fact]
        public void Expr3_UnaryOperationsTests()
        {
            // Arrange
            var x = new Variable("x");
            var y = new Variable("y");
            var c = new Constant(3);
            var expr3 = (Sin(x) + Cos(y)) * e(c) + Sqrt(10) + Tan(x) * c;
            
            // Act & Assert
            // Проверка строки выражения
            expr3.ToString().Should().Contain("((((sin(x) + cos(y)) * exp(3)) + sqrt(10)) + (tan(x) * 3))");

            // Проверка переменных
            expr3.Variables.Should().BeEquivalentTo(new[] { "x", "y" });

            // Проверка на константу
            expr3.IsConstant.Should().BeFalse();

            // Проверка, является ли полиномом
            expr3.IsPolynomial.Should().BeTrue();

            // Проверка степени полинома
            expr3.PolynomialDegree.Should().Be(0);

            // Проверка вычисления значения
            var variableValues = new Dictionary<string, double> { ["x"] = 1, ["y"] = 2 };
            expr3.Compute(variableValues).Should().BeApproximately(16.37736, 1e-5);
        }

        [Fact]
        public void Expr4_BinaryOperationsTests()
        {
            // Arrange
            var x = new Variable("x");
            var y = new Variable("y");
            var c = new Constant(3);
            var expr4 = Pow(x, 10) * c + (Log(2, x) - Log(3, x)) * y / 10.4 - Pow(y, 3);

            // Act & Assert
            // Проверка строки выражения
            expr4.ToString().Should().Contain("((((x ^ 10) * 3) + (((log_2(x) - log_3(x)) * y) / 10,4)) - (y ^ 3))");

            // Проверка переменных
            expr4.Variables.Should().BeEquivalentTo(new[] { "x", "y" });

            // Проверка на константу
            expr4.IsConstant.Should().BeFalse();

            // Проверка, является ли полиномом
            expr4.IsPolynomial.Should().BeTrue();

            // Проверка степени полинома
            expr4.PolynomialDegree.Should().Be(10);

            // Проверка вычисления значения
            var variableValues = new Dictionary<string, double> { ["x"] = 1, ["y"] = 2 };
            expr4.Compute(variableValues).Should().BeApproximately(-5, 1e-5);
        }

        [Fact] 
        public void Expr5_ErrorTests() 
        {
            // Arrange
            var x = new Variable("x");
            var y = new Variable("y");
            var c = new Constant(3);
            var d = new Constant(0);

            var expr5 = Pow(x, 10) * c + (Log(2, x) - Log(3, x)) * y / 10.4 - Pow(y, 3) / d;

            // Act & Assert
            // Проверка строки выражения
            expr5.ToString().Should().Contain("((((x ^ 10) * 3) + (((log_2(x) - log_3(x)) * y) / 10,4)) - ((y ^ 3) / 0))");

            // Проверка переменных
            expr5.Variables.Should().BeEquivalentTo(new[] { "x", "y" });

            // Проверка на константу
            expr5.IsConstant.Should().BeFalse();

            // Проверка, является ли полиномом
            expr5.IsPolynomial.Should().BeTrue();

            // Проверка степени полинома
            expr5.PolynomialDegree.Should().Be(10);

            // Проверка на нулевое деление
            var variableValues = new Dictionary<string, double> { ["x"] = 1, ["y"] = 2 };
            Assert.Throws<DivideByZeroException>(() => expr5.Compute(variableValues));
        }

        [Fact]
        public void Expr6_SimplifiersTests()
        {
            // Arrange
            var x = new Variable("x");
            var y = new Variable("y");
            var c = new Constant(3);
            var d = new Constant(10);

            var expr6 = 10 * 3 * 2 * c * 3 * 3 + x / 10 + c / d + (Sin(10) + Cos(c)) * Pow(2, d) * 2 * (-100);
            var expr6Simplified = ExpressionSimplifier.Simplify(expr6);

            // Act & Assert
            // Проверка строки выражения
            expr6Simplified.ToString().Should().Be("(((1620 + (x / 10)) + 0,3) + 314165,98681391415)");
        }
    }
}
