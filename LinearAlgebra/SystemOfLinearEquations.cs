namespace LinearAlgebra
{
    public class SystemOfLinearEquations
    {
        public int variablesCount;
        public double[,] coefficients;
        private double[] constants;

        public SystemOfLinearEquations(int variablesCount, double[,] coefficients, double[] constants)
        {
            this.variablesCount = variablesCount;
            this.coefficients = coefficients;
            this.constants = constants;
        }

        public double[] SolveGaussianElimination(double[,] A, double[] b)
        {
            // Создаем копии массивов, чтобы избежать изменения исходных данных
            int variablesCount = b.Length;
            double[,] coefficients = (double[,])A.Clone();
            double[] constants = (double[])b.Clone();
            double[] x = new double[variablesCount];

            for (int k = 0; k < variablesCount - 1; k++)
            {
                for (int i = k + 1; i < variablesCount; i++)
                {
                    double factor = coefficients[i, k] / coefficients[k, k];
                    for (int j = k + 1; j < variablesCount; j++)
                    {
                        coefficients[i, j] -= factor * coefficients[k, j];
                    }
                    constants[i] -= factor * constants[k];
                }
            }

            // Обратный ход метода Гаусса
            for (int k = variablesCount - 1; k >= 0; k--)
            {
                x[k] = constants[k];
                for (int i = k + 1; i < variablesCount; i++)
                {
                    x[k] -= coefficients[k, i] * x[i];
                }
                x[k] /= coefficients[k, k];
            }

            return x;
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
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = augmented[i, j + n];
                }
            }

            return result;

        }
    }
}