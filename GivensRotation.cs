using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com_Methods
{
    class GivensRotation
    {
        public UMatrix R { set; get; }
        public LMatrix Q { set; get; }

        //классический метод QR-разложения
        public static void ClassicDecomposition(Matrix m, Matrix q, Matrix result)
        {
            double help1;

            double sin = 0, cos = 0;

            //Givens for every column
            for (int j = 0; j < m.N-1; j++)
            {

                //watchig row
                for (int i = j+1; i < m.M; i++)
                {
                    if (Math.Abs(m.Elem[i][j])>CONST.EPS)
                    {
                        help1 = Math.Sqrt(Math.Pow(m.Elem[i][j], 2) + Math.Pow(m.Elem[j][i], 2));
                        sin = m.Elem[i][j] / help1;
                        cos = m.Elem[j][i] / help1;

                        for (int k = j; k < m.N; k++)
                        {
                            help1 = cos * m.Elem[j][k] + sin * m.Elem[i][k];
                            result.Elem[j][k] = help1;
                            m.Elem[j][k] = help1;

                            help1 = cos * m.Elem[j][k] - sin * m.Elem[i][k];
                            result.Elem[i][k] = help1;
                            m.Elem[i][k] = help1;
                        }

                        for (int k = 0; k < q.M; k++)
                        {
                            help1 = cos * q.Elem[j][k] + sin * q.Elem[i][k];
                            q.Elem[k][j] = help1;

                            help1 = cos * m.Elem[j][k] - sin * m.Elem[i][k];
                            q.Elem[k][i] = help1;
                            
                        }
                    }
                }
            }
        }


    }
}
