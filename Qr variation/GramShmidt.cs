using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com_Methods
{
    class GramShmidt
    {
     
        //классический метод QR-разложения
        public static void ClassicDecomposition(Matrix m, Matrix R, Matrix Q)
        {
         
            for (var j = 0; j < m.N; j++)
            {
                var q = new Vector(m.M);
                //формирование верхнетреугольной матрицы R
                for (var i = 0; i < j; i++)
                    for (var k = 0; k < m.M; k++)
                        R.Elem[i][j] += m.Elem[k][j] * Q.Elem[k][i];

                //копирование j-ой строки матрицы A в вектор q
                q.Copy_Column(m, j);

                for (var i = 0; i < j; i++)
                    for (var k = 0; k < q.N; k++)
                        q.Elem[k] -= Q.Elem[k][i] * R.Elem[i][j];

                //запись значения нормы вектора q в Rj,j элемент матрицы R
                R.Elem[j][j] = q.Norma();

                if (R.Elem[j][j] < CONST.EPS)
                {
                    throw new Exception("Division by zero, R[i][i] = 0");
                }
                //формирование унитарной матрицы Q
                for (var i = 0; i < m.M; i++)
                    Q.Elem[i][j] = q.Elem[i] / R.Elem[j][j];
            }
        }

        //модифицированный метод QR-разложения
        public static void ModifyDecomposition(Matrix m, Matrix R, Matrix Q)
        {
           
            var q = new Vector(m.M);

            for (var j = 0; j < m.M; j++)
            {
                //копирование j-ой строки матрицы A в вектор q
                q.Copy_Column(m, j);
               
                //формирование верхнетреугольной матрицы R
                for (var i = 0; i < j; i++)
                {
                    for (var k = 0; k < q.N; k++)
                        // скалярное произведение
                        R.Elem[i][j] += q.Elem[k] * Q.Elem[k][i];

                    for (var k = 0; k < q.N; k++)
                        q.Elem[k] -= R.Elem[i][j] * Q.Elem[k][i];

                }

                //запись значения нормы вектора q_ в Rj,j элемент матрицы R
                R.Elem[j][j] = q.Norma();

                if (R.Elem[j][j] < CONST.EPS)
                    return;

                //формирование унитарной матрицы Q
                for (var i = 0; i < m.M; i++)
                    Q.Elem[i][j] = q.Elem[i] / R.Elem[j][j];
            }

        }

        public static Vector SolverGrammShmidth(Matrix A, Vector F, int mode)
        {
            var X = new Vector(F.N);
            var Y = new Vector(F.N);
            var R = new Matrix(A.N, A.M);
            var Q = new Matrix(A.N, A.M);

            switch (mode)
            {
                case 1:
                    ClassicDecomposition(A,R,Q);
                    break;
                case 2:
                    ModifyDecomposition(A,R,Q);
                    break;
            }

            Y = Q.MultTransMatrixVector(F);

            Substitution_Method.Back_Row_Substitution(R, Y, X);

            return X;
        }
    }
}
