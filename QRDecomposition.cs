using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com_Methods
{
    class QRDecomposition
    {
        public UMatrix R { set; get; }
        public LMatrix Q { set; get; }

        //классический метод QR-разложения
        public void classicDecompos(Matrix m)
        {
            R = new UMatrix(m.N, m.N);
            Q = new LMatrix(m.M, m.M);
            var q = new Vector(m.M);

            for (var j = 0; j < m.N; j++)
            {
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
                    return;

                //формирование унитарной матрицы Q
                for (var i = 0; i < m.M; i++)
                    Q.Elem[i][j] = q.Elem[i] / R.Elem[j][j];
            }
        }

        //модифицированный метод QR-разложения
        public void GSDecompose(Matrix m)
        {
            R = new UMatrix(m.N, m.N);
            Q = new LMatrix(m.M, m.M);
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

        //решение СЛАУ
        public Vector solve(Matrix m, Vector v)
        {
            GSDecompose(m);
            Q.MultTransMatrixVector(v);
            R.Back_Row_Substitution(v);

            return v;
        }

    }
}
