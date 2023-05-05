using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebra
{
    public class Vector
    {
        private double[] components; // компоненты вектора

        // конструкторы
        public Vector(int n)
        {
            components = new double[n];
        }

        public Vector(double[] c)
        {
            components = c;
        }

        // свойства
        public int Dimension
        {
            get { return components.Length; }
        }

        // индексатор
        public double this[int i]
        {
            get { return components[i]; }
            set { components[i] = value; }
        }

        // методы
        public double Length()
        {
            double sum = 0.0;
            for (int i = 0; i < Dimension; i++)
            {
                sum += components[i] * components[i];
            }
            return Math.Sqrt(sum);
        }

        public Vector Normalize()
        {
            double length = Length();
            double[] newComponents = new double[Dimension];
            for (int i = 0; i < Dimension; i++)
            {
                newComponents[i] = components[i] / length;
            }
            return new Vector(newComponents);
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            if (v1.Dimension != v2.Dimension)
            {
                throw new ArgumentException("Vectors must have the same dimension");
            }

            double[] newComponents = new double[v1.Dimension];
            for (int i = 0; i < v1.Dimension; i++)
            {
                newComponents[i] = v1[i] + v2[i];
            }
            return new Vector(newComponents);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            if (v1.Dimension != v2.Dimension)
            {
                throw new ArgumentException("Vectors must have the same dimension");
            }

            double[] newComponents = new double[v1.Dimension];
            for (int i = 0; i < v1.Dimension; i++)
            {
                newComponents[i] = v1[i] - v2[i];
            }
            return new Vector(newComponents);
        }

        public static double operator *(Vector v1, Vector v2)
        {
            if (v1.Dimension != v2.Dimension)
            {
                throw new ArgumentException("Vectors must have the same dimension");
            }

            double dotProduct = 0.0;
            for (int i = 0; i < v1.Dimension; i++)
            {
                dotProduct += v1[i] * v2[i];
            }
            return dotProduct;
        }

        public static Vector operator *(Vector v, double scalar)
        {
            double[] newComponents = new double[v.Dimension];
            for (int i = 0; i < v.Dimension; i++)
            {
                newComponents[i] = v[i] * scalar;
            }
            return new Vector(newComponents);
        }

        public static Vector operator *(double scalar, Vector v)
        {
            return v * scalar;
        }

        public override string ToString()
        {
            string result = "(";
            for (int i = 0; i < Dimension; i++)
            {
                result += $"{components[i]}";
                if (i != Dimension - 1)
                {
                    result += ", ";
                }
            }
            result += ")";
            return result;
        }
    }

}
