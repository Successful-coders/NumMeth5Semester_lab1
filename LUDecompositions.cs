using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com_Methods
{
    class LUDecompositions
    {
        //поиск ведущего в столбцаз
        public static int FindMainElement(Matrix A, int j)
        {
            int Index = j;
            for (int i = j + 1; i < A.M; i++)
            {
                if (Math.Abs(A.Elem[i][j]) > Math.Abs(A.Elem[Index][j]))
                {
                    Index = i;

                }
            }
            if (Math.Abs(A.Elem[Index][j]) < CONST.EPS)
                throw new Exception("Gauss meth matrix degenerativn");
            return Index;
        }

        // Разложение, где ниже главной диагонали матрица L, а выше и на диагонале - U.
        public static void Decompose(Matrix m)
        {
            // Коэффициент для приведения к верхне-треугольному виду.
            double coef;
            // Индекс ведущего элемента.
            int leadElemIndex;
            double[] tmp = new double[m.N];


            for (var i = 0; i < m.M; i++)
            {
                leadElemIndex = FindMainElement(m, i);

                // Если ведущий элемент не совпадает с диагональным,
                // меняем местам строки для получения верхне-треугольного вида.
                if (leadElemIndex != i)
                {
                    tmp = m.Elem[i];
                    m.Elem[i] = m.Elem[leadElemIndex];
                    m.Elem[leadElemIndex] = tmp;
                }

                // Обнуляем элементы в столбце i, лежащие ниже строки i
                // (приведение к верхе-диагональному виду).
                for (var j = i + 1; j < m.N; j++)
                {
                    coef = m.Elem[j][i] / m.Elem[i][i];
                    // Записываем коэффициент для формирования матрицы L.
                    m.Elem[j][i] = coef;

                    for (var k = i + 1; k < m.N; k++)
                    {
                        m.Elem[j][k] -= m.Elem[i][k] * coef;
                    }
                }
            }
        }

        // Преобразовываем правую часть СЛАУ в соответствии с коэффициентами
        // для умножения строк левой части.
        public Vector GetRightPart(Matrix m, Vector v)
        {
            for (var i = 0; i < m.M; i++)
            {
                for (var j = 0; j < i; j++)
                    v.Elem[i] -= m.Elem[i][j] * v.Elem[j];
            }
            return v;
        }

        //решение СЛАУ.
        public Vector solve(Matrix m, Vector v)
        {
            Decompose(m);
            GetRightPart(m, v);

            for (var i = v.N - 1; i >= 0; i--)
            {
                for (var j = i + 1; j < v.N; j++)
                    v.Elem[i] -= m.Elem[i][j] * v.Elem[j];
                v.Elem[i] /= m.Elem[i][i];
            }

            return v;
        }

    }
}
