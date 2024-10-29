using NUnit.Framework;
using MathExprLib.Expressions;
using MathExprLib.Visitors;
using System.Collections.Generic;

namespace MathExprLib.Tests
{
    [TestFixture]
    public class MathExprTests
    {
        [Test]
        public void Expr1_ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var x = new Variable("x");
            var y = new Variable("y");
            var expr1 = new Multiplication(
                new Addition(x, new Constant(-4)),
                new Division(
                    new Addition(
                        new Multiplication(new Constant(3), x),
                        new Power(y, new Constant(2))
                    ),
                    new Constant(5)
                )
            );

            // Act
            var result = expr1.ToString();

            // Assert
            Assert.AreEqual("(x - 4) * (3*x + y^2) / 5", result);
        }

        [Test]
        public void Expr1_Variables_ReturnsCorrectVariables()
        {
            // Arrange
            var x = new Variable("x");
            var y = new Variable("y");
            var expr1 = new Multiplication(
                new Addition(x, new Constant(-4)),
                new Division(
                    new Addition(
                        new Multiplication(new Constant(3), x),
                        new Power(y, new Constant(2))
                    ),
                    new Constant(5)
                )
            );

            // Act
            var variables = expr1.Variables;

            // Assert
            CollectionAssert.AreEquivalent(new List<string> { "x", "y" }, variables);
        }

        [Test]
        public void Expr1_IsConstant_ReturnsFalse()
        {
            // Arrange
            var x = new Variable("x");
            var y = new Variable("y");
            var expr1 = new Multiplication(
                new Addition(x, new Constant(-4)),
                new Division(
                    new Addition(
                        new Multiplication(new Constant(3), x),
                        new Power(y, new Constant(2))
                    ),
                    new Constant(5)
                )
            );

            // Act
            var isConstant = expr1.IsConstant;

            // Assert
            Assert.IsFalse(isConstant);
        }

        [Test]
        public void Expr1_Compute_WithValues_ReturnsExpectedResult()
        {
            // Arrange
            var x = new Variable("x");
            var y = new Variable("y");
            var expr1 = new Multiplication(
                new Addition(x, new Constant(-4)),
                new Division(
                    new Addition(
                        new Multiplication(new Constant(3), x),
                        new Power(y, new Constant(2))
                    ),
                    new Constant(5)
                )
            );
            var variableValues = new Dictionary<string, double> { { "x", 1 }, { "y", 2 } };

            // Act
            var result = expr1.Compute(variableValues);

            // Assert
            Assert.AreEqual(-4.2, result, 1e-6);
        }

        [Test]
        public void Expr2_ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var c = new Constant(3);
            var expr2 = new Multiplication(
                new Addition(new Constant(5), new Multiplication(new Constant(-3), c)),
                new Logarithm(
                    new Addition(new Constant(16), new Power(c, new Constant(2))),
                    new Constant(0.5)  // Квадратный корень
                )
            );

            // Act
            var result = expr2.ToString();

            // Assert
            Assert.AreEqual("(5 - 3*3) * Sqrt(16 + 3^2)", result);
        }

        [Test]
        public void Expr2_IsConstant_ReturnsTrue()
        {
            // Arrange
            var c = new Constant(3);
            var expr2 = new Multiplication(
                new Addition(new Constant(5), new Multiplication(new Constant(-3), c)),
                new Logarithm(
                    new Addition(new Constant(16), new Power(c, new Constant(2))),
                    new Constant(0.5)
                )
            );

            // Act
            var isConstant = expr2.IsConstant;

            // Assert
            Assert.IsTrue(isConstant);
        }

        [Test]
        public void Expr2_Compute_ReturnsExpectedResult()
        {
            // Arrange
            var c = new Constant(3);
            var expr2 = new Multiplication(
                new Addition(new Constant(5), new Multiplication(new Constant(-3), c)),
                new Logarithm(
                    new Addition(new Constant(16), new Power(c, new Constant(2))),
                    new Constant(0.5)
                )
            );
            var variableValues = new Dictionary<string, double>();

            // Act
            var result = expr2.Compute(variableValues);

            // Assert
            Assert.AreEqual(-20, result, 1e-6);
        }
    }
}
