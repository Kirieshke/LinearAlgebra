using Xunit;
using LinearAlgebra;
using static LinearAlgebra.Matrix;

namespace LinearAlgebra.Tests
{

    public class SystemOfLinearEquationsTests
    {
        
        [Fact]
        public void SolveGaussianElimination_SolvesEquationCorrectly()
        {
            // Arrange
            int variablesCount = 3;
            double[,] coefficients = { { 2, 1, -1 }, { -3, -1, 2 }, { -2, 1, 2 } };
            double[] constants = { 8, -11, -3 };
            SystemOfLinearEquations system = new SystemOfLinearEquations(variablesCount, coefficients, constants);

            // Act
            double[] actualResult = system.SolveGaussianElimination(coefficients, constants);
            double[] expectedResult = { 2, 3, -1 };

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
        [Fact]
        public void TestAdd()
        {
            // Arrange
            double[,] inputArray = { { 1, 2 }, { 3, 4 } };
            Matrix matrix = new Matrix(inputArray);
            double number = 5;
            Matrix expectedOutput = new Matrix(new double[,] { { 6, 7 }, { 8, 9 } });

            // Act
            Matrix actualOutput = Matrix.Add(matrix,number);

            // Assert
            Assert.Equal(expectedOutput, actualOutput, new MatrixComparer());
        }
        [Fact]
        public void Subtract_TwoMatricesWithSameDimensions_ReturnsMatrixWithCorrectValues()
        {
            // Arrange
            double[,] matrix1Values = new double[,]
            {
        { 1, 2, 3 },
        { 4, 5, 6 }
            };
            Matrix matrix1 = new Matrix(matrix1Values);

            double[,] matrix2Values = new double[,]
            {
        { 1, 1, 1 },
        { 1, 1, 1 }
            };
            Matrix matrix2 = new Matrix(matrix2Values);

            double[,] expectedValues = new double[,]
            {
        { 0, 1, 2 },
        { 3, 4, 5 }
            };
            Matrix expected = new Matrix(expectedValues);

            // Act
            Matrix actual = Matrix.Subtract(matrix1, matrix2);

            // Assert
            Assert.Equal(expected, actual, new MatrixComparer());
        }

        [Fact]
        public void Inverse_2x2Matrix_InvertsCorrectly()
        {
            // Arrange
            int variablesCount = 2;
            double[,] coefficients = { { 2, -1 }, { -3, 2 } };
            double[] constants = { 1, 7 };
            SystemOfLinearEquations system = new SystemOfLinearEquations(variablesCount, coefficients, constants);

            // Act
            double[,] actualInverse = system.Inverse();
            double[,] expectedInverse = { { 2, 1 }, { 3, 2 } };

            // Assert
            Assert.Equal(expectedInverse, actualInverse);
        }

        [Fact]
        public void TestInverse_3x3Matrix()
        {
            // Arrange
            double[,] input = {
            { 1, 2, 3 },
            { 0, 1, 4 },
            { 5, 6, 0 }
        };
            double[,] expectedOutput = {
            { -24, 18, 5 },
            { 20, -15, -4 },
            { -5, 4, 1 }
        };
            Matrix matrix = new Matrix(input);

            // Act
            double[,] actualOutput = matrix.Inverse();

            // Assert
            Assert.Equal(expectedOutput, actualOutput);
        }

        [Fact]
        public void Add_TwoMatricesWithSameDimensions_ReturnsMatrixWithCorrectValues()
        {
            // Arrange
            double[,] matrix1Values = new double[,]
            {
            { 1, 2, 3 },
            { 4, 5, 6 }
            };
            Matrix matrix1 = new Matrix(matrix1Values);

            double[,] matrix2Values = new double[,]
            {
            { 7, 8, 9 },
            { 10, 11, 12 }
            };
            Matrix matrix2 = new Matrix(matrix2Values);

            double[,] expectedValues = new double[,]
            {
            { 8, 10, 12 },
            { 14, 16, 18 }
            };
            Matrix expected = new Matrix(expectedValues);

            // Act
            Matrix actual = Matrix.Add(matrix1, matrix2);

            // Assert
            Assert.Equal(expected, actual, new MatrixComparer());
        }

        [Fact]
        public void Add_TwoMatricesWithDifferentDimensions_ThrowsArgumentException()
        {
            // Arrange
            double[,] matrix1Values = new double[,]
            {
            { 1, 2, 3 },
            { 4, 5, 6 }
            };
            Matrix matrix1 = new Matrix(matrix1Values);

            double[,] matrix2Values = new double[,]
            {
            { 7, 8, 9 },
            { 10, 11, 12 },
            { 13, 14, 15 }
            };
            Matrix matrix2 = new Matrix(matrix2Values);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Matrix.Add(matrix1, matrix2));
        }
        [Fact]
        public void TestMatrixMultiplication()
        {
            // Создаем две матрицы
            double[,] coefficients1 = { { 1, 2 }, { 3, 4 } };
            Matrix matrix1 = new Matrix(coefficients1);

            double[,] coefficients2 = { { 5, 6 }, { 7, 8 } };
            Matrix matrix2 = new Matrix(coefficients2);

            // Ожидаемый результат умножения матриц
            double[,] expectedCoefficients = { { 19, 22 }, { 43, 50 } };
            Matrix expectedMatrix = new Matrix(expectedCoefficients);

            // Умножаем матрицы
            Matrix resultMatrix = Matrix.Multiply(matrix1, matrix2);

            // Сравниваем результат с ожидаемым значением
            Assert.Equal(expectedMatrix.Inverse(), resultMatrix.Inverse());
        }
        [Fact]
        public void TestMatrixMultiplication3x3()
        {
            // Create two matrices
            double[,] coefficients1 = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            Matrix matrix1 = new Matrix(coefficients1);

            double[,] coefficients2 = { { 10, 11, 12 }, { 13, 14, 15 }, { 16, 17, 18 } };
            Matrix matrix2 = new Matrix(coefficients2);

            // Expected result of matrix multiplication
            double[,] expectedCoefficients = { { 84, 90, 96 }, { 201, 216, 231 }, { 318, 342, 366 } };
            Matrix expectedMatrix = new Matrix(expectedCoefficients);

            // Multiply the matrices
            Matrix resultMatrix = Matrix.Multiply(matrix1, matrix2);

            // Compare the result with the expected value
            Assert.Equal(expectedMatrix.Inverse(), resultMatrix.Inverse());
        }
        [Fact]
        public void Multiply_MatrixByScalar_ReturnsMatrixWithCorrectValues()
        {
            // Arrange
            double[,] matrixValues = new double[,]
            {
        { 1, 2 },
        { 3, 4 }
            };
            Matrix matrix = new Matrix(matrixValues);

            double scalar = 2;

            double[,] expectedValues = new double[,]
            {
        { 2, 4 },
        { 6, 8 }
            };
            Matrix expected = new Matrix(expectedValues);

            // Act
            Matrix actual = Matrix.Multiply(matrix, scalar);

            // Assert
            Assert.Equal(expected, actual, new MatrixComparer());
        }
            
    }

}

