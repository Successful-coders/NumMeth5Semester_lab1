using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Com_Methods
{
    enum Decomposition
    {
        Gauss=1,
        LU,
        ClassicGrammShmidth,
        ModifyGrammaShmidth,
        GivensRotatin,
        Hausholder
    }
    class Program
    {
        public static void CreateNonsingularMatrix (Matrix A, Vector F)
        {
            
            var rand = new Random();
            for (int i = 0; i < A.M; i++)
            {
                A.Elem[i][i] = 1.0;
                F.Elem[i] = 1.0;
            }
            for (int i = 0; i < A.N; i++)

                for (int I = 0; I < A.N; I++)
                {
                    double rt = rand.NextDouble() * 100;
                    A.Elem[I][i] += A.Elem[i][i] * rt;
                    F.Elem[I] += F.Elem[i] * rt;
                }
        }

        public static double Time(Decomposition mode, Matrix A, Vector F)
        {
            Stopwatch sw = new Stopwatch();
            Vector X;

            sw.Start();
            switch (mode)
            {
                case Decomposition.Gauss:
                    {
                        GaussMethod solver = new GaussMethod();
                        solver.SolveMatrix(A, F).ConsoleWriteVector();
                        break;
                    }
                case Decomposition.LU:
                    {
                        LUDecompositions LU = new LUDecompositions();
                        LU.Solve(A, F).ConsoleWriteVector();
                        break;
                    }
                case Decomposition.ClassicGrammShmidth:
                    {
                        GramShmidt.SolverGrammShmidth(A, F, 1).ConsoleWriteVector();
                        break;
                    }
                case Decomposition.ModifyGrammaShmidth:
                    {
                        GramShmidt.SolverGrammShmidth(A, F, 2).ConsoleWriteVector();
                        break;
                    }
                case Decomposition.GivensRotatin:
                    {

                        GivensRotation.GivensSolver(A, F).ConsoleWriteVector();
                        break;
                    }
                case Decomposition.Hausholder:
                    {
                        Hausholder.Householder_Reflection_Solver(A, F).ConsoleWriteVector();
                        break;
                    }
            }

            sw.Stop();

            return sw.Elapsed.TotalSeconds;
        }

        public static double Error(Vector X, Vector resX)
        {
            double error=0;
            var help = new Vector(X.N);
            for (int i = 0; i < X.N; i++)
            {
                help.Elem[i] = X.Elem[i] - resX.Elem[i];
            }
            error = help.Norma() / resX.Norma();
            return error;
        }
        public static void Main()
        {
            int size = 3;
            Matrix matrixA = new Matrix(size, size);
            Vector vectorF = new Vector(size);
            //CreateNonsingularMatrix(matrixA, vectorF);

            Vector f = new Vector(size);
            Vector x = new Vector(size);

            double[][] aElements = new double[][]
            {
                new double[]{ -2, -2, -1 },
                new double[]{ 1, 0, -2},
                new double[]{ 0, 1, 2}
            };
            matrixA.Elem = aElements;

            double[] fElements = new double[]
            {
                -5,
                -1,
                3,
            };

            f.Elem = fElements;
            var resX = new Vector(size);
            resX.Elem[0] = resX.Elem[1] = resX.Elem[2] = 1.0;
            var X = new Vector(size);
            X=GramShmidt.SolverGrammShmidth(matrixA, f, 1);
            //X=GramShmidt.SolverGrammShmidth(matrixA, f, 2);
            //X=Hausholder.Householder_Reflection_Solver(matrixA, f);
            //X=GivensRotation.GivensSolver(matrixA, f);

            //LUDecompositions LU = new LUDecompositions();
            //X=LU.Solve(matrixA, f);

            //GaussMethod solver = new GaussMethod();
            //X=solver.SolveMatrix(matrixA, f);


            Console.WriteLine(Error(X, resX));
        }
    }
}
