using System;
using System.Collections.Generic;
using System.Text;
using utility;

namespace prizaLinearAlgebra
{
    public static class Preconditioners
    {

        
        public static class JacobiPc
        {
            public static double[,] CreatePCMatrix(double[,] A)
            {
                var n = A.GetLength(0);
                var MInverse = new double[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j) { MInverse[i, j] = 1d / A[i, j]; }
                        else { MInverse[i, j] = A[i, j]; }
                    }
                }
                return MInverse;
            }
        }

        // public class CholeskyPC
        // {
        //     public CholeskyPC() { }

        //     public static double[,] CreatePCMatrix(double[,] A)
        //     {
        //         var n = A.GetLength(0);
        //         var MInverse = new double[n, n];
        //         var L = DirectSolvers.CholeskylowerDiagonalCalculator(A);
        //         var LInverse = utilitiez.Calculators.MatrixInverse(L);
        //         var LTranspose = utilitiez.Calculators.MatrixTranspose(L);
        //         var LTransposeInverse = utilitiez.Calculators.MatrixInverse(LTranspose);
        //         MInverse = utilitiez.Calculators.MatrixMatrixMultiplication(LInverse, LTransposeInverse);
        //         utilitiez.matrixPrinter(MInverse);
        //         utilitiez.matrixPrinter(utilitiez.Calculators.MatrixInverse(A));
        //         return MInverse;
        //     }

        // }



    }
}
