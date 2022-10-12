using System;
using System.Collections.Generic;
using System.Text;
using utility;

namespace prizaLinearAlgebra
{
    public static class DirectSolvers
    {
        public static double[] GaussEliminationPartialPivot(double[,] A, double[] b)
        {
            var n = A.GetLength(0);
            var k = 0;

            //move the RHS Column inside the matrix without change of sign
            double[,] GaussMatrix = new double[n, n + 1];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    GaussMatrix[i, j] = A[i, j];
                }
                GaussMatrix[i, n] = b[i];
            }
            while (k < n)
            {
                var pivot = findPivot(k);

                for (int j = k; j <= n; j++) { GaussMatrix[k, j] = GaussMatrix[k, j] / pivot; }
                for (int i = k + 1; i < n; i++) //maybe = is wrong
                {
                    var t = GaussMatrix[i, k];
                    for (int j = k; j <= n; j++) { GaussMatrix[i, j] += -GaussMatrix[k, j] * t; }
                }
                k++;
            }
            //Back substitution
            double[] x = new double[n];
            x[n - 1] = GaussMatrix[n - 1, n];
            for (int i = n - 1; i >= 0; i--)
            {
                var s = 0d;
                for (int j = i + 1; j < n; j++)
                {
                    s += GaussMatrix[i, j] * x[j];
                }
                x[i] = GaussMatrix[i, n] - s;
            }

            return x;

            double findPivot(int k)
            {
                var max = GaussMatrix[k, k];
                var maxi = k;
                for (int i = k; i < n; i++)
                {
                    if (Math.Abs(GaussMatrix[i, k]) > Math.Abs(max))
                    {
                        max = GaussMatrix[i, k];
                        maxi = i;
                    }
                    if (maxi != k)
                    {
                        for (int j = 0; j <= n; j++)
                        {
                            var temp = GaussMatrix[k, j];
                            GaussMatrix[k, j] = GaussMatrix[maxi, j];
                            GaussMatrix[maxi, j] = temp;
                        }
                    }
                }
                var pivot = max;
                return pivot;
            }
        }

        public static double[] LUSolver(double[,] A, double[] b)
        {
            var n = A.GetLength(0);
            double[,] L = new double[n, n];
            double[,] U = new double[n, n];
            double[] y = new double[n];
            double[] x = new double[n];

            (double[,], double[,]) LUecomposition(double[,] A)
            {
                for (int i = 0; i < n; i++)
                {
                    //U
                    for (int j = i; j < n; j++)
                    {
                        var s = 0d;
                        for (int k = 0; k < i; k++) { s += L[i, k] * U[k, j]; }
                        U[i, j] = (A[i, j] - s);
                    }

                    //L
                    for (int j = i; j < n; j++)
                    {
                        if (i == j)
                            L[i, i] = 1; // Diagonal as 1
                        else
                        {
                            var s = 0d;
                            for (int k = 0; k < i; k++) { s += L[j, k] * U[k, i]; }
                            L[j, i] = (A[j, i] - s) / U[i, i];
                        }

                    }
                }

                return (L, U);
            }

            L = LUecomposition(A).Item1;
            //Console.WriteLine("lower triangular");
            //utilitiez.matrixPrinter(L);
            Console.WriteLine("");
            U = LUecomposition(A).Item2;
            //Console.WriteLine("upper triangular");
            //utilitiez.matrixPrinter(U);
            Console.WriteLine("");

            // ForwardSubstitution for L * y = b
            for (int i = 0; i < n; i++)
            {
                y[i] = b[i];
                for (int j = 0; j < i; j++) { y[i] -= (L[i, j] * y[j]); }
                y[i] /= L[i, i];
            }

            // Backsubstitution for U * x = y
            for (int i = n - 1; i >= 0; i--)
            {
                x[i] = y[i];
                for (int j = i + 1; j < n; j++) { x[i] -= U[i, j] * x[j]; }
                x[i] /= U[i, i];
            }

            Console.WriteLine("x");
            //utilitiez.vectorPrinter(x);
            Console.WriteLine("");
            return x;
        }

        public static double[] LUSolverOnMatrix(double[,] A, double[] b)
        {
            var n = A.GetLength(0);
            double[] y = new double[n];
            double[] x = new double[n];

            void LUDecomposition(double[,] A)
            {
                for (int i = 0; i < n; i++)
                {
                    //U
                    for (int j = i; j < n; j++)
                    {
                        var s = 0d;
                        for (int k = 0; k < i; k++) { s += A[i, k] * A[k, j]; }
                        A[i, j] = (A[i, j] - s);
                    }

                    //L
                    for (int j = i + 1; j < n; j++)
                    {
                        var s = 0d;
                        for (int k = 0; k < i; k++) { s += A[j, k] * A[k, i]; }
                        A[j, i] = (A[j, i] - s) / A[i, i];

                    }
                }
            }
            LUDecomposition(A);
            Console.WriteLine("LU");
            //utilitiez.matrixPrinter(A);


            // ForwardSubstitution for L * y = b
            for (int i = 0; i < n; i++)
            {
                y[i] = b[i];
                for (int j = 0; j < i; j++) { y[i] -= (A[i, j] * y[j]); }
            }

            // Backsubstitution for U * x = y
            for (int i = n - 1; i >= 0; i--)
            {
                x[i] = y[i];
                for (int j = i + 1; j < n; j++) { x[i] -= A[i, j] * x[j]; }
                x[i] /= A[i, i];
            }

            //Console.WriteLine("x");
            //utilitiez.vectorPrinter(x);
            //Console.WriteLine("");
            return x;
        }

        public static double[,] CholeskylowerDiagonalCalculator(double[,] A)
        {
            var sum = 0d;
            double[,] L = new double[A.GetLength(0), A.GetLength(0)];
            L[0, 0] = Math.Sqrt(A[0, 0]);
            var n = A.GetLength(0);
            for (int i = 1; i < n; i++)
            {
                //Computations for the diagonal
                for (int j = 0; j <= i; j++)
                {
                    sum = 0d;
                    if (j == i)
                    {
                        for (int k = 0; k < j; k++)
                        {
                            sum += Math.Pow(L[j, k], 2);
                        }
                        L[j, j] = Math.Sqrt(A[j, j] - sum);
                    }
                    else
                    {
                        for (int k = 0; k < j; k++)
                        {
                            sum += L[i, k] * L[j, k];
                        }
                        L[i, j] = 1d / L[j, j] * (A[i, j] - sum);
                    }
                }
            }
            return L;
        }

        public static double[] CholeskySolver(double[,] A, double[] b)
        {
            var n = (int)Math.Sqrt(A.Length);
            double[,] L = new double[n, n];
            double[] y = new double[n];
            double[] x = new double[n];

            L = CholeskylowerDiagonalCalculator(A);
            //Console.WriteLine("Cholesky Lower Diagonal");
            //utilitiez.matrixPrinter(L);
            //Console.WriteLine("");

            // ForwardSubstitution for L * y = b
            for (int i = 0; i < n; i++)
            {
                y[i] = b[i];
                for (int j = 0; j < i; j++) { y[i] -= (L[i, j] * y[j]); }
                y[i] /= L[i, i];
                //Console.WriteLine(y[i]);
            }

            // Backsubstitution for U * x = y
            for (int i = n - 1; i >= 0; i--)
            {
                x[i] = y[i];
                for (int j = i + 1; j < n; j++) { x[i] -= L[j, i] * x[j]; }
                x[i] /= L[i, i];
            }

            //Console.WriteLine("x");
            //utilitiez.vectorPrinter(x);
            //Console.WriteLine("");
            return x;
        }

        public static double[] CholeskySolverOnMatrix(double[,] A, double[] b)
        {
            var n = (int)Math.Sqrt(A.Length);
            double[,] L = new double[n, n];
            double[] y = new double[n];
            double[] x = new double[n];
            double sum = 0d;

            void lowerDiagonalCalculator(double[,] A)
            {

                A[0, 0] = Math.Sqrt(A[0, 0]);
                for (int i = 1; i < n; i++)
                {
                    //Computations for the diagonal
                    for (int j = 0; j <= i; j++)
                    {
                        sum = 0d;
                        if (j == i)
                        {
                            for (int k = 0; k < j; k++)
                            {
                                sum += Math.Pow(A[j, k], 2);
                            }
                            A[j, j] = Math.Sqrt(A[j, j] - sum);
                        }
                        else
                        {
                            for (int k = 0; k < j; k++)
                            {
                                sum += A[i, k] * A[j, k];
                            }
                            A[i, j] = 1d / A[j, j] * (A[i, j] - sum);
                        }
                    }
                }
            }

            lowerDiagonalCalculator(A);
            Console.WriteLine("Cholesky Lower Diagonal on matrix");
            utilitiez.matrixPrinter(A);
            Console.WriteLine("");

            // ForwardSubstitution for L * y = b
            for (int i = 0; i < n; i++)
            {
                y[i] = b[i];
                for (int j = 0; j < i; j++) { y[i] -= (L[i, j] * y[j]); }
                y[i] /= L[i, i];
            }

            // Backsubstitution for U * x = y
            for (int i = n - 1; i >= 0; i--)
            {
                x[i] = y[i];
                for (int j = i + 1; j < n; j++) { x[i] -= L[j, i] * x[j]; }
                x[i] /= L[i, i];
            }

            Console.WriteLine("x");
            utilitiez.vectorPrinter(x);
            Console.WriteLine("");
            return x;
        }

        public static double[] CholeskySkylineSolver(double[,] A, double[] b)
        {
            var n = b.Length;
            var x = new double[n];

            //Store initial matrix in skyline form
            var (values, diagonalOffsets) = SparseMatrixStorage.SkylineGTR1200PS(A);

            // Initialize L Matrix
            double[] LValues = new double[values.Length];
            double[] LDiagonalOffsets= new double[diagonalOffsets.Length];
            for (int i = 0; i < diagonalOffsets.Length; i++) { LDiagonalOffsets[i] = diagonalOffsets[i]; }

            // column marching
            for (int i = 0; i < n; i++)
            {
                // Height of column i exluding the diagonal
                var hI = (int)diagonalOffsets[i + 1] - (int)diagonalOffsets[i] - 1;
                
                // Find for each column i the first (top) row with a non-zero entry
                var mI = i - hI;

                for (int j = mI; j < i; j++)
                {
                    // Height of column j exluding the diagonal
                    var hJ = (int)diagonalOffsets[j + 1] - (int)diagonalOffsets[j] - 1;

                    // Find for each column i the first (top) row with a non-zero entry
                    var mJ = j - hJ;

                    var sum = 0d;
                    for (int k = Math.Max(mI,mJ) ; k < j; k++)
                    {
                        var Lki = SparseMatrixStorage.SkylineIndexing(k, i, LValues, LDiagonalOffsets);
                        var Lkj = SparseMatrixStorage.SkylineIndexing(k, j, LValues, LDiagonalOffsets);
                        sum += Lki * Lkj;
                    }
                    var Aij = SparseMatrixStorage.SkylineIndexing(i, j, values, diagonalOffsets);
                    var Ljj = SparseMatrixStorage.SkylineIndexing(j, j, LValues, LDiagonalOffsets);
                    var Lji = (1d / Ljj) * (Aij - sum);
                    SparseMatrixStorage.SkylineValueReplace(j, i, Lji, LValues, LDiagonalOffsets);

                }

                var sum2 = 0d;
                for (int k = mI; k < i; k++)
                {
                    var Lki = SparseMatrixStorage.SkylineIndexing(k, i, LValues, LDiagonalOffsets);
                    sum2 += Math.Pow(Lki, 2);
                }
                var Aii = SparseMatrixStorage.SkylineIndexing(i, i, values, diagonalOffsets);
                var Lii = Math.Sqrt(Aii - sum2);
                SparseMatrixStorage.SkylineValueReplace(i, i, Lii, LValues, LDiagonalOffsets);
            }
            var y = SkylineForwardSubstitutiond(LValues, LDiagonalOffsets, b);
            x = SkylineBackSubstitutiond(LValues, LDiagonalOffsets, y);

            //utilitiez.CholeskySkylinePrinter(LValues, LDiagonalOffsets, x);

            return x;
        }

        public static double[] SkylineForwardSubstitutiond(double[] LValues, double[] LDiagonalOffsets, double[] b)
        {
            //Forward Substitution for L* y = b
            var n = LDiagonalOffsets.Length - 1;
            var y = new double[n];
            for (int i = 0; i < n; i++)
            {
                y[i] = b[i];
                for (int j = 0; j < i; j++)
                {
                    var Lji = SparseMatrixStorage.SkylineIndexing(j, i, LValues, LDiagonalOffsets);
                    y[i] -= Lji * y[j];
                }
                var Lii = SparseMatrixStorage.SkylineIndexing(i, i, LValues, LDiagonalOffsets);
                y[i] /= Lii;
            }
            return y;
        }

        public static double[] SkylineBackSubstitutiond(double[] LValues, double[] LDiagonalOffsets, double[] y)
        {
            // Back substitution for U * x = y
            var n = LDiagonalOffsets.Length - 1;
            var x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                x[i] = y[i];
                for (int j = i + 1; j < n; j++)
                {
                    var Lij = SparseMatrixStorage.SkylineIndexing(i, j, LValues, LDiagonalOffsets);
                    x[i] -= Lij * x[j];
                }
                var Lii = SparseMatrixStorage.SkylineIndexing(i, i, LValues, LDiagonalOffsets);
                x[i] /= Lii;
            }
            return x;
        }



        static DirectSolvers()
        {

        }
    }
}
