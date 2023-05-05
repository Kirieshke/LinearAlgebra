using LinearAlgebra;
using System;

class Program
{
    static void Main(string[] args)
    {
        Matrix m = new Matrix();
        m = Matrix.CreateMatrix(4, 4);
        Console.WriteLine(m.ToString());

        Console.ReadKey();
    }
}
