using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinearAlgebra;

[TestClass]
public class SystemOfLinearEquationsTests
{
    [TestMethod]
    public void SolveGaussianElimination_Test1()
    {
        // Arrange
        int variablesCount = 3;
        double[,] coefficients = { { 1, 2, -1 }, { 2, 1, -2 }, { -3, 1, 1 } };
        double[] constants = { 8, 7, -1 };
        double[] expected = { 3, -2, 2 };
        SystemOfLinearEquations system = new SystemOfLinearEquations(variablesCount, coefficients, constants);
        double[] actual = system.SolveGaussianElimination(coefficients, constants);

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void SolveMatrixInversion_Test1()
    {
        // Arrange
        int variablesCount = 3;
        double[,] coefficients = { { 1, 2, -1 }, { 2, 1, -2 }, { -3, 1, 1 } };
        double[] constants = { 8, 7, -1 };
        double[] expected = { 3, -2, 2 };
        SystemOfLinearEquations system = new SystemOfLinearEquations(variablesCount, coefficients, constants);

        // Act
        double[] actual = system.SolveMatrixInversion();

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }
}