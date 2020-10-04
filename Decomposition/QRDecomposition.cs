using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com_Methods
{

    class QRDecomposition
    {
        public enum QRVariation
        {
            ClassicGramShmidt=1,
            ModifyGramShmidt,
            Givens,
            Hausholder
        }

        public UMatrix R { set; get; }
        public LMatrix Q { set; get; }

        public QRDecomposition (Matrix A, QRVariation method)
        {
            R = new UMatrix(A.M, A.N);
            Q = new LMatrix(A.M, A.M);
            
            switch(method)
            {
                case QRVariation.ClassicGramShmidt:
                    {
                        GramShmidt alghoritm = new GramShmidt();
                        alghoritm.ClassicDecomposition(A);
                        break;
                    }
                case QRVariation.ModifyGramShmidt:
                    {
                        GramShmidt alghoritm = new GramShmidt();
                        alghoritm.ModifyDecomposition(A);
                        break;
                    }
                case QRVariation.Givens:
                    {
                        for (int i = 0; i < A.M; i++)
                        {
                            Q.Elem[i][i] = 1.0;
                        }
                        GivensRotation.ClassicDecomposition(A, Q, R);
                        break;
                    }
                case QRVariation.Hausholder:
                    {
                        for (int i = 0; i < A.M; i++)
                        {
                            Q.Elem[i][i] = 1.0;
                        }
                       // Hausholder(A, Q, R);
                        break;
                    }
            }
        }

        public Vector Solver (Vector F)
        {
            var result = Q.MultTransMatrixVector(F);
            Substitution_Method.Back_Row_Substitution(R, result, result);
            return result;
        }

    }
}
