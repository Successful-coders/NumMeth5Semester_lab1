using System;
using System.Collections.Generic;
using System.Text;

namespace Com_Methods
{
    public static class SubstitutionMethods
    {
        public static Vector DirectRow(Matrix matrix, Vector f)
        {
            Vector result = new Vector(f.N);

            //скопируем по значениям вектор F в RES
            result.Copy(f);
            //проход по строкам
            for (int i = 0; i < f.N; i++)
            {
                if (Math.Abs(matrix.Elem[i][i]) < CONST.EPS)
                    throw new Exception("Direct Row Substitution: division by 0...");
                for (int j = 0; j < i; j++)
                {
                    result.Elem[i] -= matrix.Elem[i][j] * result.Elem[j];
                }
                result.Elem[i] /= matrix.Elem[i][i];
            }

            return result;
        }

        public static Vector DirectColumnSubstitution(Matrix matrix, Vector f)
        {
            Vector result = new Vector(f.N);

            //скопируем вектор F в RES
            result.Copy(f);
            //проход по столбцам
            for (int j = 0; j < f.N; j++)
            {
                if (Math.Abs(matrix.Elem[j][j]) < CONST.EPS)
                    throw new Exception("Direct Column Substitution: division by 0...");
                result.Elem[j] /= matrix.Elem[j][j];
                for (int i = j + 1; i < f.N; i++)
                {
                    result.Elem[i] -= matrix.Elem[i][j] * result.Elem[j];
                }
            }

            return result;
        }

        public static Vector BackRowSubstitution(Matrix A, Vector F)
        {
            Vector result = new Vector(F.N);

            //скопируем вектор F в RES
            result.Copy(F);
            //начинаем с последней строки, двигаясь вверх
            for (int i = F.N - 1; i >= 0; i--)
            {
                if (Math.Abs(A.Elem[i][i]) < CONST.EPS)
                    throw new Exception("Back Row Substitution: division by 0...");
                //двигаемся по столбцам
                for (int j = i + 1; j < F.N; j++)
                {
                    result.Elem[i] -= A.Elem[i][j] * result.Elem[j];
                }
                result.Elem[i] /= A.Elem[i][i];
            }


            return result;
        }

        //обратная подстановка по столбцам (А - верхняя треугольная матрица)
        public static Vector Back_Column_Substitution(Matrix A, Vector F)
        {
            Vector result = new Vector(F.N);
            //скопируем вектор F в RES
            result.Copy(F);
            //начинаем с последнего столбца, сдвигаясь влево
            for (int j = F.N - 1; j >= 0; j--)
            {
                if (Math.Abs(A.Elem[j][j]) < CONST.EPS)
                    throw new Exception("Back Column Substitution: division by 0...");
                result.Elem[j] /= A.Elem[j][j];
                //двигаемся по строкам
                for (int i = j - 1; i >= 0; i--)
                {
                    result.Elem[i] -= A.Elem[i][j] * result.Elem[j];
                }
            }
            return result;
        }

       
    }
}
