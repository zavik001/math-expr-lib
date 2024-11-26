using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Expressions;
using Expressions.Simplification;
using Expressions.VariablesAndConstants;
using Expressions.Unary;
using Expressions.Binary;
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
            expr2.IsPolynomial.Should().BeFalse();

            // Проверка степени полинома
            expr2.PolynomialDegree.Should().Be(-1);

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
            var expr3 = (Sin(x) + Cos(y)) * Exp(c) + Sqrt(10) + Tan(x) * c;

            // Act & Assert
            // Проверка строки выражения
            expr3.ToString().Should().Contain("((((sin(x) + cos(y)) * exp(3)) + sqrt(10)) + (tan(x) * 3))");

            // Проверка переменных
            expr3.Variables.Should().BeEquivalentTo(new[] { "x", "y" });

            // Проверка на константу
            expr3.IsConstant.Should().BeFalse();

            // Проверка, является ли полиномом
            expr3.IsPolynomial.Should().BeFalse();

            // Проверка степени полинома
            expr3.PolynomialDegree.Should().Be(-1);

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

        [Fact]
        public void Expr8_Simplify_MultiplicationByZero()
        {
            // Arrange
            var x = new Variable("x");
            var expr = x * 0;

            // Act
            var simplifiedExpr = ExpressionSimplifier.Simplify(expr);

            // Assert
            simplifiedExpr.ToString().Should().Be("0"); // x * 0 должно быть упрощено до 0
            simplifiedExpr.IsConstant.Should().BeTrue();
            simplifiedExpr.Compute(new Dictionary<string, double>()).Should().Be(0);
        }

        [Fact]
        public void Expr9_Simplify_AdditionWithZero()
        {
            // Arrange
            var x = new Variable("x");
            var expr = x + 0;

            // Act
            var simplifiedExpr = ExpressionSimplifier.Simplify(expr);

            // Assert
            simplifiedExpr.ToString().Should().Be("x");
            simplifiedExpr.IsConstant.Should().BeFalse();
            simplifiedExpr.Variables.Should().ContainSingle(v => v == "x");
        }

        [Fact]
        public void Expr10_Simplify_ComplexExpression()
        {
            // Arrange
            var x = new Variable("x");
            var y = new Variable("y");
            var expr = (x + 0) * (y + 1) - (y * 0);

            // Act
            var simplifiedExpr = ExpressionSimplifier.Simplify(expr);

            // Assert
            simplifiedExpr.ToString().Should().Be("(x * (y + 1))");
        }

        [Fact]
        public void Expr11_Simplify_DivisionByOne()
        {
            // Arrange
            var x = new Variable("x");
            var expr = x / 1;

            // Act
            var simplifiedExpr = ExpressionSimplifier.Simplify(expr);

            // Assert
            simplifiedExpr.ToString().Should().Be("x");
        }

        [Fact]
        public void Expr12_Simplify_ExpressionWithTrigonometry()
        {
            // Arrange
            var c = new Constant(0);
            var expr = Sin(c) + Cos(c);

            // Act
            var simplifiedExpr = ExpressionSimplifier.Simplify(expr);

            // Assert
            simplifiedExpr.ToString().Should().Be("1");
            simplifiedExpr.IsConstant.Should().BeTrue();
            simplifiedExpr.Compute(new Dictionary<string, double>()).Should().Be(1);
        }

        [Fact]
        public void Expr13_DivideOperation_Compute_ValidInputs_ReturnsCorrectResult()
        {
            // Arrange
            var left = new Constant(10);
            var right = new Constant(2);
            var divide = new DivideOperation(left, right);

            // Act
            var result = divide.Compute(new Dictionary<string, double>());

            // Assert
            result.Should().Be(5);
        }

        [Fact]
        public void Expr14_DivideOperation_Compute_DivideByZero_ThrowsException()
        {
            // Arrange
            var left = new Constant(10);
            var right = new Constant(0);
            var divide = new DivideOperation(left, right);

            // Act
            Action act = () => divide.Compute(new Dictionary<string, double>());

            // Assert
            act.Should().Throw<DivideByZeroException>().WithMessage("Деление на ноль.");
        }

        [Fact]
        public void Expr15_DivideOperation_IsPolynomial_ValidPolynomials_ReturnsTrue()
        {
            // Arrange
            var x = new Variable("x");
            var numerator = x * x * 2; // Полином 2x^2
            var denominator = x;      // Полином x
            var divide = new DivideOperation(numerator, denominator);

            // Act
            var isPolynomial = divide.IsPolynomial;

            // Assert
            isPolynomial.Should().BeTrue();
        }

        [Fact]
        public void Expr16_DivideOperation_IsPolynomial_InvalidPolynomials_ReturnsFalse()
        {
            // Arrange
            var x = new Variable("x");
            var numerator = x * 2 + 3; // Полином 2x + 3
            var denominator = x * x;  // Полином x^2
            var divide = new DivideOperation(numerator, denominator);

            // Act
            var isPolynomial = divide.IsPolynomial;

            // Assert
            isPolynomial.Should().BeFalse();
        }

        [Fact]
        public void Expr17_DivideOperation_PolynomialDegree_ValidPolynomials_ReturnsDegreeDifference()
        {
            // Arrange
            var x = new Variable("x");
            var numerator = x * x * 3; // Полином 3x^2
            var denominator = x;      // Полином x
            var divide = new DivideOperation(numerator, denominator);

            // Act
            var degree = divide.PolynomialDegree;

            // Assert
            degree.Should().Be(1); // Разница степеней 2 - 1 = 1
        }

        [Fact]
        public void Expr18_DivideOperation_PolynomialDegree_InvalidPolynomials_ReturnsNegativeOne()
        {
            // Arrange
            var x = new Variable("x");
            var numerator = x + 1;    // Полином x + 1
            var denominator = x * x; // Полином x^2
            var divide = new DivideOperation(numerator, denominator);

            // Act
            var degree = divide.PolynomialDegree;

            // Assert
            degree.Should().Be(-1);
        }

        [Fact]
        public void Expr19_DivideOperation_IsConstant_ValidConstants_ReturnsTrue()
        {
            // Arrange
            var left = new Constant(10);
            var right = new Constant(2);
            var divide = new DivideOperation(left, right);

            // Act
            var isConstant = divide.IsConstant;

            // Assert
            isConstant.Should().BeTrue();
        }

        [Fact]
        public void Expr20_DivideOperation_IsConstant_WithVariable_ReturnsFalse()
        {
            // Arrange
            var x = new Variable("x");
            var left = new Constant(10);
            var divide = new DivideOperation(left, x);

            // Act
            var isConstant = divide.IsConstant;

            // Assert
            isConstant.Should().BeFalse();
        }

        [Fact]
        public void Expr21_DivideOperation_ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var x = new Variable("x");
            var y = new Variable("y");
            var divide = new DivideOperation(x, y);

            // Act
            var result = divide.ToString();

            // Assert
            result.Should().Be("(x / y)");
        }

        [Fact]
        public void Expr22_ConstantExpressionTests()
        {
            // Arrange
            var c = new Constant(42);

            // Act & Assert
            c.ToString().Should().Be("42");
            c.IsConstant.Should().BeTrue();
            c.Variables.Should().BeEmpty();
            c.Compute(new Dictionary<string, double>()).Should().Be(42);
        }

        [Fact]
        public void Expr23_NegativeVariableTests()
        {
            // Arrange
            var x = new Variable("x");
            var expr = -x;

            // Act & Assert
            expr.ToString().Should().Be("-(x)");
            expr.IsConstant.Should().BeFalse();
            expr.Variables.Should().BeEquivalentTo(new[] { "x" });
            expr.PolynomialDegree.Should().Be(1);

            var variableValues = new Dictionary<string, double> { ["x"] = -3 };
            expr.Compute(variableValues).Should().Be(3);
        }

        [Fact]
        public void Expr24_MixedOperationsTests()
        {
            // Arrange
            var x = new Variable("x");
            var y = new Variable("y");
            var expr = (x + 2) / (y - 3);

            // Act & Assert
            expr.ToString().Should().Be("((x + 2) / (y - 3))");
            expr.IsConstant.Should().BeFalse();
            expr.Variables.Should().BeEquivalentTo(new[] { "x", "y" });
        }

        [Fact]
        public void Expr25_ExponentialTests()
        {
            // Arrange
            var x = new Variable("x");
            var expr = Exp(x);

            // Act & Assert
            expr.ToString().Should().Be("exp(x)");
            expr.IsPolynomial.Should().BeFalse();
            expr.PolynomialDegree.Should().Be(-1);

            var variableValues = new Dictionary<string, double> { ["x"] = 1 };
            expr.Compute(variableValues).Should().BeApproximately(Math.E, 1e-5);
        }

        [Fact]
        public void Expr26_TrigSimplificationTests()
        {
            // Arrange
            var x = new Variable("x");
            var expr = Sin(x) * Cos(x);

            // Act
            var simplified = ExpressionSimplifier.Simplify(expr);

            // Assert
            simplified.ToString().Should().Be("(sin(x) * cos(x))");
        }

        [Fact]
        public void Expr27_SquareRootTests()
        {
            // Arrange
            var x = new Variable("x");
            var expr = Sqrt(x);

            // Act & Assert
            expr.ToString().Should().Be("sqrt(x)");
            expr.IsPolynomial.Should().BeFalse();

            var variableValues = new Dictionary<string, double> { ["x"] = 4 };
            expr.Compute(variableValues).Should().Be(2);
        }

        [Fact]
        public void Expr28_LogarithmicExpressionTests()
        {
            // Arrange
            var x = new Variable("x");
            var expr = Log(10, x);

            // Act & Assert
            expr.ToString().Should().Be("log_10(x)");
            expr.IsPolynomial.Should().BeTrue();

            var variableValues = new Dictionary<string, double> { ["x"] = 100 };
            expr.Compute(variableValues).Should().BeApproximately(2, 1e-5);
        }

        [Fact]
        public void Expr29_ZeroMultiplicationTests()
        {
            // Arrange
            var x = new Variable("x");
            var expr = x * 0;

            // Act
            var simplified = ExpressionSimplifier.Simplify(expr);

            // Assert
            simplified.ToString().Should().Be("0");
            simplified.IsConstant.Should().BeTrue();
        }

        [Fact]
        public void Expr30_ComplexExpressionTests()
        {
            // Arrange
            var x = new Variable("x");
            var y = new Variable("y");
            var expr = (x + y) * (x - y);

            // Act
            var simplified = ExpressionSimplifier.Simplify(expr);

            // Assert
            simplified.ToString().Should().Be("((x + y) * (x - y))");
        }

        [Fact]
        public void Expr31_DivisionByConstantTests()
        {
            // Arrange
            var x = new Variable("x");
            var expr = x / 4;

            // Act & Assert
            expr.ToString().Should().Be("(x / 4)");
            expr.IsConstant.Should().BeFalse();

            var variableValues = new Dictionary<string, double> { ["x"] = 8 };
            expr.Compute(variableValues).Should().Be(2);
        }

        [Fact]
        public void Expr32_ValidLogarithmTests()
        {
            // Arrange
            var expr = MathExpr.Log(2, 8);

            // Act & Assert
            expr.Compute(new Dictionary<string, double>()).Should().Be(3, "log base 2 of 8 equals 3");
            expr.ToString().Should().Be("log_2(8)");
        }

        [Fact]
        public void Expr33_LogarithmWithInvalidBaseTests()
        {
            // Arrange
            var expr = MathExpr.Log(-2, 8);

            // Act
            Action compute = () => expr.Compute(new Dictionary<string, double>());

            // Assert
            compute.Should().Throw<ArgumentException>()
                .WithMessage("Основание логарифма должно быть положительным и не равно 1.");
        }

        [Fact]
        public void Expr34_LogarithmWithInvalidArgumentTests()
        {
            // Arrange
            var expr = MathExpr.Log(2, -8);

            // Act
            Action compute = () => expr.Compute(new Dictionary<string, double>());

            // Assert
            compute.Should().Throw<ArgumentException>()
                .WithMessage("Аргумент логарифма должен быть положительным.");
        }

        [Fact]
        public void Expr35_ValidSqrtTests()
        {
            // Arrange
            var expr = MathExpr.Sqrt(16);

            // Act & Assert
            expr.Compute(new Dictionary<string, double>()).Should().Be(4, "sqrt(16) equals 4");
            expr.ToString().Should().Be("sqrt(16)");
        }

        [Fact]
        public void Expr36_SqrtWithNegativeArgumentTests()
        {
            // Arrange
            var expr = MathExpr.Sqrt(-9);

            // Act
            Action compute = () => expr.Compute(new Dictionary<string, double>());

            // Assert
            compute.Should().Throw<ArgumentException>()
                .WithMessage("Квадратный корень из отрицательного числа не существует.");
        }

        [Fact]
        public void Expr37_CombinedExpressionTests()
        {
            // Arrange
            var expr = MathExpr.Sqrt(MathExpr.Log(2, 8));

            // Act
            var result = expr.Compute(new Dictionary<string, double>());

            // Assert
            result.Should().Be(Math.Sqrt(3), "sqrt(log_2(8)) equals sqrt(3)");
            expr.ToString().Should().Be("sqrt(log_2(8))");
        }

        [Fact]
        public void Expr38_ValidPolynomialPowerTests()
        {
            // Arrange
            var x = new Variable("x");
            var expr = MathExpr.Pow(x, 3);

            // Act & Assert
            expr.IsPolynomial.Should().BeTrue();
            expr.PolynomialDegree.Should().Be(3);
            expr.ToString().Should().Be("(x ^ 3)");
        }

        [Fact]
        public void Expr39_InvalidPolynomialPowerTests()
        {
            // Arrange
            var x = new Variable("x");
            var expr = MathExpr.Pow(x, 1.5);

            // Act & Assert
            expr.IsPolynomial.Should().BeFalse();
            expr.PolynomialDegree.Should().Be(-1);
        }

        [Fact]
        public void Expr40_NegativeExponentTests()
        {
            // Arrange
            var x = new Variable("x");
            var expr = MathExpr.Pow(x, -2);

            // Act & Assert
            expr.IsPolynomial.Should().BeFalse();
            expr.PolynomialDegree.Should().Be(-1);
        }
    }
}