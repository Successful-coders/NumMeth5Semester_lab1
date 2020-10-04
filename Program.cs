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
                new double[]{ 1, 2 }
            };
            matrixA.Elem = aElements;

            double[] fElements = new double[]
            {
                3,
                3,
            };

            f.Elem = fElements;

            //LUDecompositions LU = new LUDecompositions();
            //LU.Solve(matrixA, f).ConsoleWriteVector();

            //GaussMethod solver = new GaussMethod();
            //solver.SolveMatrix(matrixA, f).ConsoleWriteVector();

            //QRDecomposition QR = new QRDecomposition();
            //QR.solve(matrixA, f).ConsoleWriteVector();
        }
    }
}
