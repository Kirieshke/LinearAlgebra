using LinearAlgebra;
using System;

class Program
{
    static void Main(string[] args)
    {
        int numRows = 17;
        int numCols = 15;
        double[,] matrix = new double[numRows, numCols];
        int value = 1;
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                matrix[i, j] = value % 5 + 1;
                value++;
            }
        }
        

        Console.ReadKey();
    }
}
