using System;
using System.Collections.Generic;
using System.Text;

namespace Com_Methods
{
    class GaussMethod
    {
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
    }
}
