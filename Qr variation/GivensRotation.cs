using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com_Methods
{
    class GivensRotation
    {
       

        //классический метод QR-разложения
        public static void ClassicGivensDecomposition(Matrix A, Matrix R, Matrix Q)
        {
            R.Copy(A);
            double sin, cos, tmp;
            double temp1, temp2;

            for (int i = 0; i < Q.M; i++)
                Q.Elem[i][i] = 1;

            for (int j = 0; j < A.N - 1; j++)
            {
                for (int i = j + 1; i < A.M; i++)
                {
                    if (Math.Abs(R.Elem[i][j]) > CONST.EPS)
                    {
                        tmp = Math.Sqrt(R.Elem[i][j] * R.Elem[i][j] + R.Elem[j][j] * R.Elem[j][j]);

                        sin = R.Elem[i][j] / tmp;
                        cos = R.Elem[j][j] / tmp;

                        for (int k = j; k < A.N; k++)
                        {
                            temp1 = cos * R.Elem[j][k] + sin * R.Elem[i][k];
                            temp2 = -sin * R.Elem[j][k] + cos * R.Elem[i][k];

                            R.Elem[j][k] = temp1;
                            R.Elem[i][k] = temp2;
                        }

                        for (int k = 0; k < Q.M; k++)
                        {
                            temp1 = cos * Q.Elem[k][j] + sin * Q.Elem[k][i];
                            temp2 = -sin * Q.Elem[k][j] + cos * Q.Elem[k][i];

                            Q.Elem[k][j] = temp1;
                            Q.Elem[k][i] = temp2;
                        }

                    }
                }
            }

        }
    

        public static Vector GivensSolver (Matrix A, Vector F)
        {
            var X = new Vector(F.N);
            var Y = new Vector(F.N);
            var Q = new Matrix(A.N, A.M);
            var R = new Matrix(A.N, A.M);
            ClassicGivensDecomposition(A, R, Q);

            Y = Q.MultTransMatrixVector(F);

            Substitution_Method.Back_Row_Substitution(R, Y, X);
            return X;
        }

    }
}
