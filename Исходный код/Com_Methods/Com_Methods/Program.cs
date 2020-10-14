using System;
using System.Diagnostics;
using System.Threading;


namespace Com_Methods
{
    class CONST
    {
        //точность решения
        public static double EPS = 1e-20;
    }

    class Tools
    {
        //замер времени
        public static string Measurement_Time(Thread thread)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            thread.Start();
            while (thread.IsAlive) ;
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            return ("RunTime: " + elapsedTime);
        }

        //относительная погрешность
        public static double Relative_Error(Vector X, Vector x)
        {
            double s = 0.0;
            for (int i = 0; i < X.N; i++)
            {
                s += Math.Pow(X.Elem[i] - x.Elem[i], 2);
            }
            return Math.Sqrt(s) / x.Norma();
        }

        //относительная невязка
        public static double Relative_Discrepancy(Matrix A, Vector X, Vector F)
        {
            var x = A * X;
            for (int i = 0; i < X.N; i++) x.Elem[i] -= F.Elem[i];
            return x.Norma() / F.Norma();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {
                //прямые методы: Гаусс, LU-разложение, QR-разложение
                var T1 = new Thread(() =>
                {
                    int N = 10;
                    var A = new Matrix(N, N);
                    var X_true = new Vector(N);
                    
                    //заполнение СЛАУ
                    for (int i = 0; i < N; i++)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            A.Elem[i][j] = 1.0 / (i + j + 1.0);
                        }
                        X_true.Elem[i] = 1;
                    }

                    //правая часть
                    var F = A * X_true;
                    
                    //решатель
                    //var Solver = new Gauss_Method();
                    //var Solver = new LU_Decomposition(A);
                    var Solver = new QR_Decomposition(A, QR_Decomposition.QR_Algorithm.Householder);

                    var X = Solver.Start_Solver(F);
                    //X.Console_Write_Vector();
                    Console.WriteLine("\nError: {0}\n", Tools.Relative_Error(X, X_true));
                    
                });

                //итерационные блочные методы: Якоби и SOR
                var T2 = new Thread(() =>
                {
                    int N = 10;
                    var A = new Matrix(N, N);
                    var X_true = new Vector(N);

                    //заполнение СЛАУ
                    for (int i = 0; i < N; i++)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            A.Elem[i][j] = 1.0 / (i + j + 1.0);
                        }
                        X_true.Elem[i] = 1;
                    }

                    //правая часть
                    var F = A * X_true;

                    Console.WriteLine("Cond(A) = " + A.Cond_InfinityNorm());
                });

                //время решения
                Console.WriteLine(Tools.Measurement_Time(T2));

            }
            catch (Exception E)
            {
                Console.WriteLine("\n*** Error! ***");
                Console.WriteLine("Method:  {0}",   E.TargetSite);
                Console.WriteLine("Message: {0}\n", E.Message);
            }
        }
    }
}