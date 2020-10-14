//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Diagnostics;

//namespace Com_Methods
//{
//    enum Decomposition
//    {
//        Gauss=1,
//        LU,
//        ClassicGrammShmidth,
//        ModifyGrammaShmidth,
//        GivensRotatin,
//        Hausholder
//    }
//    class Program
//    {
//        public static void CreateNonsingularMatrix (Matrix A, Vector F)
//        {
            
//            var rand = new Random();
//            for (int i = 0; i < A.M; i++)
//            {
//                for (int j = 0; j < A.N; j++)
//                {

//                A.Elem[i][j] = i==j ? 1.0 : 0;
//                }
//                F.Elem[i] = 1.0;
//            }
//            for (int i = 0; i < A.N; i++)

//                for (int I = 0; I < A.N; I++)
//                {
//                    double rt = rand.NextDouble() * 100;
//                    A.Elem[I][i] += A.Elem[i][i] * rt;
//                    F.Elem[I] += F.Elem[i] * rt;
//                }
//        }

//        public static double Time(Decomposition mode, Matrix A, Vector F)
//        {
//            Stopwatch sw = new Stopwatch();
//            Vector X;

//            sw.Start();
//            switch (mode)
//            {
//                case Decomposition.Gauss:
//                    {
//                        GaussMethod solver = new GaussMethod();
//                        solver.SolveMatrix(A, F);
//                        break;
//                    }
//                case Decomposition.LU:
//                    {
//                        LUDecompositions LU = new LUDecompositions();
//                        LU.Solve(A, F);
//                        break;
//                    }
//                case Decomposition.ClassicGrammShmidth:
//                    {
//                        GramShmidt.SolverGrammShmidth(A, F, 1);
//                        break;
//                    }
//                case Decomposition.ModifyGrammaShmidth:
//                    {
//                        GramShmidt.SolverGrammShmidth(A, F, 2);
//                        break;
//                    }
//                case Decomposition.GivensRotatin:
//                    {

//                        GivensRotation.GivensSolver(A, F);
//                        break;
//                    }
//                case Decomposition.Hausholder:
//                    {
//                        Hausholder.Householder_Reflection_Solver(A, F);
//                        break;
//                    }
//            }

//            sw.Stop();

//            return sw.Elapsed.TotalSeconds;
//        }

//        public static double Error(Vector X, Vector resX)
//        {
//            double error=0;
//            var help = new Vector(X.N);
//            for (int i = 0; i < X.N; i++)
//            {
//                help.Elem[i] = X.Elem[i] - resX.Elem[i];
//            }
//            error = help.Norma() / resX.Norma();
//            return error;
//        }


//        public static void Main()
//        {
//            int size = 500;
//            Matrix matrixA = new Matrix(size, size);
//            Vector vectorF = new Vector(size);
//            CreateNonsingularMatrix(matrixA, vectorF);

//            double time = 0;

//            for (int j = 0; j < 10; j++)
//            {
//                time += Time(Decomposition.ClassicGrammShmidth, matrixA, vectorF);
//            }

//            Console.WriteLine("Time method\t" + (time));


//        }
//    }
//}
