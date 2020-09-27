using System;
using System.Collections.Generic;
using System.Text;

namespace Com_Methods
{
    class Program
    {
        public static void Main()
        {
            int size = 2;
            Matrix matrixA = new Matrix(size, size);
            Vector f = new Vector(size);
            Vector x = new Vector(size);

            double[][] aElements = new double[][]
            {
                new double[]{ 2, 1},
                new double[]{ 0, 2 }
            };
            matrixA.Elem = aElements;

            double[] fElements = new double[]
            {
                3,
                2,
            };

            f.Elem = fElements;

            SubstitutionMethods.Back_Column_Substitution(matrixA, f).ConsoleWriteVector();
        }
    }
}
