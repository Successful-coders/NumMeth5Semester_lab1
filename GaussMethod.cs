using System;
using System.Collections.Generic;
using System.Text;

namespace Com_Methods
{
    class GaussMethod
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

        //Прямой ход, разложение Гаусса матрицы и вектора правой части
        public static void GaussDecomposition(Matrix A, Vector V)
        {
            // коэффициент разложения
            double coef;


            //ведущий элемент индекс
            int leadingElement;

            for (int i = 0; i < A.M - 1; i++)
            {
                //ведущий элемент в столбце
                leadingElement = FindMainElement(A, i);

                if (leadingElement != i)
                {
                    //перставляем элементы в матрице
                    var help = A.Elem[leadingElement];
                    A.Elem[leadingElement] = A.Elem[i];
                    A.Elem[i] = help;


                    //переставляем элементы в векторе
                    help[0] = V.Elem[i];
                    V.Elem[i] = V.Elem[leadingElement];
                    V.Elem[leadingElement] = help[0];
                }

                //обнуляем оставшиеся, приводя к верхне диагональному
                for (var j = i + 1; j < A.M; j++)
                {
                    coef = A.Elem[j][i] / A.Elem[i][i];
                    A.Elem[j][i] = 0;

                    for (var k = i + 1; k < A.N; k++)
                    {
                        A.Elem[j][k] -= A.Elem[i][k] * coef;
                    }

                    V.Elem[j] -= V.Elem[i] * coef;
                }

            }

        }
        //// Преобразовываем правую часть СЛАУ в соответствии с коэффициентами
        //// для умножения строк левой части.
        //public Vector GetRightPart(Matrix A, Vector V)
        //{
        //    for (var i = 0; i < A.M; i++)
        //    {
        //        for (var j = 0; j < i; j++)
        //            V.Elem[i] -= A.Elem[i][j] * V.Elem[j];
        //    }
        //    return V;
        //}


        public Vector SolveMatrix (Matrix A, Vector V)
        {
            Vector result = new Vector(V.N);

            GaussDecomposition(A, V);
            //GetRightPart(A, V);

            result = SubstitutionMethods.BackRowSubstitution(A, V);
            return result;
        }
    }
}
