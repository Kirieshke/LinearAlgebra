using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebra
{

    public class Matrix
    {
        public class MatrixComparer : IEqualityComparer<Matrix>
        {
            public bool Equals(Matrix x, Matrix y)
            {
                if (x.coefficients.GetLength(0) != y.coefficients.GetLength(0) ||
                    x.coefficients.GetLength(1) != y.coefficients.GetLength(1))
                {
                    return false;
                }

                for (int i = 0; i < x.coefficients.GetLength(0); i++)
                {
                    for (int j = 0; j < x.coefficients.GetLength(1); j++)
                    {
                        if (x.coefficients[i, j] != y.coefficients[i, j])
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            public int GetHashCode(Matrix obj)
            {
                return obj.GetHashCode();
            }
        }

        private double[,] coefficients;

        public Matrix(double[,] coefficients)
        {
            this.coefficients = coefficients;
        }
        public static Matrix CreateMatrix(int rows, int cols)
        {
            throw new ArgumentException();
        }
        public static Matrix IdentityMatrix(int rows, int cols)
        {
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = (i == j) ? 1 : 0;
                }
            }

            return new Matrix(result);
        }
        public static Matrix Add(Matrix matrix1, Matrix matrix2)
        {
            int rows1 = matrix1.coefficients.GetLength(0);
            int cols1 = matrix1.coefficients.GetLength(1);
            int rows2 = matrix2.coefficients.GetLength(0);
            int cols2 = matrix2.coefficients.GetLength(1);

            if (rows1 != rows2 || cols1 != cols2)
            {
                throw new ArgumentException("The two matrices must have the same dimensions.");
            }

            double[,] result = new double[rows1, cols1];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < cols1; j++)
                {
                    result[i, j] = matrix1.coefficients[i, j] + matrix2.coefficients[i, j];
                }
            }

            return new Matrix(result);
        }
        public static Matrix Add(Matrix matrix, double number)
        {
            int rows = matrix.coefficients.GetLength(0);
            int cols = matrix.coefficients.GetLength(1);
            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix.coefficients[i, j] += number;
                }
            }
            return new Matrix(result);
        }
        public static Matrix Subtract(Matrix matrix1, Matrix matrix2)
        {
            int rows1 = matrix1.coefficients.GetLength(0);
            int cols1 = matrix1.coefficients.GetLength(1);
            int rows2 = matrix2.coefficients.GetLength(0);
            int cols2 = matrix2.coefficients.GetLength(1);

            if (rows1 != rows2 || cols1 != cols2)
            {
                throw new ArgumentException("The two matrices must have the same dimensions.");
            }

            double[,] result = new double[rows1, cols1];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < cols1; j++)
                {
                    result[i, j] = matrix1.coefficients[i, j] - matrix2.coefficients[i, j];
                }
            }

            return new Matrix(result);
        }
        public static Matrix Subtract(Matrix matrix, double scalar)
        {
            int rows = matrix.coefficients.GetLength(0);
            int cols = matrix.coefficients.GetLength(1);

            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix.coefficients[i, j] - scalar;
                }
            }

            return new Matrix(result);
        }
        public static Matrix Multiply(Matrix matrix, double scalar)
        {
            int rows = matrix.coefficients.GetLength(0);
            int cols = matrix.coefficients.GetLength(1);

            double[,] result = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix.coefficients[i, j] * scalar;
                }
            }

            return new Matrix(result);
        }
        public static Matrix Multiply(Matrix matrix1, Matrix matrix2)
        {
            int rows1 = matrix1.coefficients.GetLength(0);
            int cols1 = matrix1.coefficients.GetLength(1);
            int rows2 = matrix2.coefficients.GetLength(0);
            int cols2 = matrix2.coefficients.GetLength(1);

            if (cols1 != rows2)
            {
                throw new ArgumentException("The number of columns of the first matrix must equal the number of rows of the second matrix.");
            }

            double[,] result = new double[rows1, cols2];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < cols2; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < cols1; k++)
                    {
                        sum += matrix1.coefficients[i, k] * matrix2.coefficients[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return new Matrix(result);
        }
        public static Matrix Pow(Matrix matrix, int power)
        {
            if (power < 0)
            {
                throw new ArgumentException("The power must be a non-negative integer.");
            }

            int rows = matrix.coefficients.GetLength(0);
            int cols = matrix.coefficients.GetLength(1);

            if (rows != cols)
            {
                throw new ArgumentException("The matrix must be square to raise it to a power.");
            }

            Matrix result = IdentityMatrix(rows, cols);

            while (power > 0)
            {
                if (power % 2 == 1)
                {
                    result = Multiply(result, matrix);
                }

                matrix = Multiply(matrix, matrix);
                power /= 2;
            }

            return result;
        }
        public double[,] Inverse()
        {
            int n = coefficients.GetLength(0);
            double[,] augmented = new double[n, 2 * n];

            // Дописываем единичную матрицу справа от матрицы A
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    augmented[i, j] = coefficients[i, j];
                    augmented[i, j + n] = i == j ? 1 : 0;
                }
            }

            // Применяем метод Гаусса-Жордана
            for (int i = 0; i < n; i++)
            {
                if (augmented[i, i] == 0)
                {
                    // Ищем ненулевой элемент на главной диагонали и меняем текущую строку с ним
                    int j;
                    for (j = i + 1; j < n; j++)
                    {
                        if (augmented[j, i] != 0)
                        {
                            break;
                        }
                    }
                    if (j == n)
                    {
                        throw new ArgumentException("Matrix is not invertible.");
                    }
                    for (int k = 0; k < 2 * n; k++)
                    {
                        double tmp = augmented[i, k];
                        augmented[i, k] = augmented[j, k];
                        augmented[j, k] = tmp;
                    }
                }

                double divisor = augmented[i, i];
                for (int j = 0; j < 2 * n; j++)
                {
                    augmented[i, j] /= divisor;
                }

                for (int j = 0; j < n; j++)
                {
                    if (j == i)
                    {
                        continue;
                    }

                    double factor = augmented[j, i];
                    for (int k = 0; k < 2 * n; k++)
                    {
                        augmented[j, k] -= factor * augmented[i, k];
                    }
                }
            }

            // Извлекаем матрицу A^-1 из расширенной матрицы
            double[,] result = new double[n, n];
            double determinant = 1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = augmented[i, j + n];
                    if (i == j)
                    {
                        determinant *= augmented[i, j];
                    }
                }
            }

            if (determinant == 0)
            {
                throw new ArgumentException("Matrix is not invertible.");
            }

            return result;
        }
        public Matrix Transpose()
        {
            int rows = coefficients.GetLength(0);
            int cols = coefficients.GetLength(1);


            double[,] result = new double[cols, rows];

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    result[i, j] = coefficients[j, i];
                }
            }

            return new Matrix(result);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < coefficients.GetLength(0); i++)
            {
                for (int j = 0; j < coefficients.GetLength(1); j++)
                {
                    sb.Append(coefficients[i, j] + "\t");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
