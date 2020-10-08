using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com_Methods
{
    class Hausholder
    {
        public static void Reflection(Matrix A, Matrix R, Matrix Q)
        {
            var p = new Vector(A.N);
            double Beta, u, tmp;

            for (int i = 0; i < Q.M; i++)
                Q.Elem[i][i] = 1;

            R.Copy(A);

            for (int i = 0; i < R.N - 1; i++)
            {

                tmp = 0;
                //квадрат нормы столбца для обнуления
                for (int j = i; j < R.N; j++)
                    tmp += Math.Pow(R.Elem[j][i], 2);

                //проверяем, необходимо ли занулять текущий столбец  
                if (Math.Abs(tmp - R.Elem[i][i] * R.Elem[i][i]) > CONST.EPS)
                {
                    //выбор знака для Beta
                    if (R.Elem[i][i] < 0)
                        Beta = Math.Sqrt(tmp);
                    else
                        Beta = -Math.Sqrt(tmp);

                    //Вычисление множителя u = 2 / ||p||^2
                    u = 1.0 / Beta / (Beta - R.Elem[i][i]);


                    //Вычисление вектора p
                    for (int j = 0; j < R.N; j++)
                    {
                        p.Elem[j] = 0;

                        if (j >= i)
                            p.Elem[j] = R.Elem[j][i];
                    }
                    p.Elem[i] -= Beta;


                    for (int j = 0; j < R.N; j++)
                    {
                        tmp = 0;
                        //A^t * p
                        for (int k = i; k < R.N; k++)
                            tmp += R.Elem[k][j] * p.Elem[k];

                        tmp *= u;

                        //A = A - 2 * p * (A^t * p)^t/ ||p||^2
                        for (int k = i; k < R.N; k++)
                            R.Elem[k][j] -= tmp * p.Elem[k];
                    }

                    //нахождение компонент матрицы Q
                    for (int j = 0; j < Q.N; j++)
                    {
                        tmp = 0;
                        for (int k = i; k < R.N; k++)
                            tmp += Q.Elem[j][k] * p.Elem[k];

                        tmp *= u;
                        //Q = Q - p * (Q * p)^t
                        for (int k = i; k < Q.N; k++)
                            Q.Elem[j][k] -= tmp * p.Elem[k];


                    }

                }

            }
        }


        public static Vector Householder_Reflection_Solver(Matrix A, Vector F)
        {
            var X = new Vector(F.N);
            var Y = new Vector(F.N);
            var Q = new Matrix(A.N, A.M);
            var R = new Matrix(A.N, A.M);


            Reflection(A, R, Q);

            Y = Q.MultTransMatrixVector(F);

            Substitution_Method.Back_Row_Substitution(R, Y, X);

            return X;

        }
    }
}
