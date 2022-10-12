using System;
using System.Collections.Generic;
using System.Text;
using utility;

namespace prizaLinearAlgebra
{
    // Tim Davis
    // Tim Davis

    public static class SparseMatrixStorage
    {

        public static (double[], int[], int[]) CSR(double[,] A)
        {
            List<double> values = new List<double>();
            List<int> column = new List<int>();
            List<int> rowOffsets = new List<int>();

            for (int i = 0; i < A.GetLength(0); i++)
            {
                var foundFirstElementOfRow = false;
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    if (A[i, j] != 0d)
                    {
                        values.Add(A[i, j]);
                        var positionInValues = values.Count - 1;

                        column.Add(j);
                        if (foundFirstElementOfRow == false)
                        {
                            foundFirstElementOfRow = true;
                            rowOffsets.Add(positionInValues);
                        }
                    }
                }
            }
            rowOffsets.Add(values.Count);
            return (values.ToArray(), column.ToArray(), rowOffsets.ToArray());
        }

        public static double[] CSRMatrixVectorMultiplication(double[] values, int[] column, int[] rowOffset, double[] vector)
        {
            double[] x = new double[vector.Length];

            for (int i = 0; i < x.Length; i++)
            {
                x[i] = 0d;
                for (int j = rowOffset[i]; j < rowOffset[i + 1]; j++)
                {
                    x[i] += values[j] * vector[column[j]];
                }
            }

            return x;
        }

        public static double[] CSRTransposeMatrixVectorMultiplication(double[] values, int[] column, int[] rowOffset, double[] vector)
        {
            double[] x = new double[vector.Length];

            {
                for (int i = 0; i <= x.Length - 1; i++)
                {
                    x[i] = 0d;
                }

                for (int i = 0; i <= x.Length - 1; i++)
                {
                    for (int k = rowOffset[i]; k <= rowOffset[i + 1] - 1; k++)
                    {
                        x[column[k]] = x[column[k]] + values[k] * vector[i];
                    }
                }
            }
            return x;
        }

        public static (double[], int[], int[]) CSC(double[,] A)
        {
            List<double> values = new List<double>();
            List<int> row = new List<int>();
            List<int> columnOffsets = new List<int>();


            for (int j = 0; j < A.GetLength(1); j++) // Columns marching
            {
                var foundFirstElementOfColumn = false;
                for (int i = 0; i < A.GetLength(0); i++) // Row marching
                {
                    if (A[i, j] != 0d)
                    {
                        values.Add(A[i, j]);
                        var positionInValues = values.Count - 1;

                        row.Add(i);
                        if (foundFirstElementOfColumn == false)
                        {
                            foundFirstElementOfColumn = true;
                            columnOffsets.Add(positionInValues);
                        }
                    }
                }
            }
            columnOffsets.Add(values.Count);
            //Console.WriteLine("CSC");
            //Console.WriteLine("Matrix");
            //utilitiez.matrixPrinter(A);
            //Console.WriteLine("values");
            //utilitiez.vectorPrinter(values.ToArray());
            //Console.WriteLine("row");
            //utilitiez.vectorPrinter(row.ToArray());
            //Console.WriteLine("collumn offsets");
            //utilitiez.vectorPrinter(columnOffsets.ToArray());

            return (values.ToArray(), row.ToArray(), columnOffsets.ToArray());

        }

        public static double[] CSCMatrixVectorMultiplication(double[] values, int[] row, int[] columnOffset, double[] vector)
        {
            double[] x = new double[vector.Length];

            for (int i = 0; i < x.Length; i++)
            {
                for (int k = columnOffset[i]; k <= columnOffset[i + 1] - 1; k++)
                {
                    x[row[k]] += values[k] * vector[i];
                }
            }

            return x;
        }

        public static double[] CSCTransposeMatrixVectorMultiplication(double[] values, int[] row, int[] columnOffset, double[] vector)
        {
            double[] xT = new double[vector.Length];
            for (int i = 0; i <= vector.Length - 1; i++)
            {
                xT[i] = 0d;
                for (int k = columnOffset[i]; k <= columnOffset[i + 1] - 1; k++)
                {
                    xT[i] +=values[k] * vector[row[k]];
                }
            }
            return xT;
        }

        public static (double[], int[], int[]) COO(double[,] A)
        {
            List<double> values = new List<double>();
            List<int> row = new List<int>();
            List<int> column = new List<int>();


            for (int i = 0; i < A.GetLength(0); i++) // Columns marching
            {
                for (int j = 0; j < A.GetLength(1); j++) // Row marching
                {
                    if (A[i, j] != 0d)
                    {
                        values.Add(A[i, j]);
                        row.Add(i);
                        column.Add(j);
                    }
                }
            }
            utilitiez.COOPrinter(A, values.ToArray(), column.ToArray(), row.ToArray());
            return (values.ToArray(), row.ToArray(), column.ToArray());

        }

        public static double[] COOMatrixVectorMultiplication(double[] values, int[] column, int[] row, double[] vector)
        {
            double[] x = new double[vector.Length];

            for (int i = 0; i < values.Length; i++)
            {
                x[row[i]] += values[i] * vector[column[i]];
            }

            return x;
        }

        public static double[] COOTransposeMatrixVectorMultiplication(double[] values, int[] column, int[] row, double[] vector)
        {
            double[] x = new double[vector.Length];

            {
                for (int i = 0; i < values.Length; i++)
                {
                    x[column[i]] += values[i] * vector[row[i]];
                }
            }
            return x;
        }

        public static double[] Banded(double[,] A, bool isSymmetric)
        {
            List<double> values = new List<double>();

            var n = A.GetLength(0);

            // Each diagonal has a fixed size (n). k diagonal above the main has k zeros added in the end.
            double[] kDiagonalU = new double[n];

            if (isSymmetric == true)
            {
                //Bandwidth is the number of 
                var UBandwidth = 0;

                var firstNZFound = false;


                while (UBandwidth < n)
                {
                    kDiagonalU = new double[n];
                    firstNZFound = false;

                    for (int i = 0; i < n - UBandwidth; i++)
                    {
                        var j = i + UBandwidth;

                        kDiagonalU[i] = A[i, j];
                        if (A[i, j] != 0 && firstNZFound == false) { firstNZFound = true; }

                    }
                    if (firstNZFound == true)
                    {
                        for (int j = 0; j < n; j++) { values.Add(kDiagonalU[j]); }
                    }
                    else
                    {
                        // The diagonal with zeros is marched so the banwith is increased. The real banwith is -1.
                        UBandwidth -= 1;
                        break;
                    }
                    
                    UBandwidth++;
                }
                utilitiez.BandedPrinter(A, values.ToArray(), UBandwidth);

                var I = 0;
                var J = 3;
                var Aij = BandedIndexing(I, J, n, values.ToArray());
                Console.WriteLine("A[" + I.ToString()  + "," + J.ToString() + "]= " + Aij);
                
            }
            return values.ToArray();
        }

        public static double BandedIndexing (int i, int j, int n, double[] values)
        {
            var Aij = 0d;
            //Find the diagonal where Aij belongs
            // diagonal = j - i example (1,2) belongs to the first diagonal above the main, (1,1) belongs to the main diagonal.
            var diagonal = Math.Abs(j - i);

            // Since each diagonal is stored in n consecutive array positions the maximum number of diagonals is  values / n  (k(diagonals) * n(values each) / n) 
            var maxDiagonal = values.Length / n - 1;
            Console.WriteLine(maxDiagonal);

            // First element of diagonal k in array values is in the n * k position
            var firstOfDiagonal = n * diagonal;

            if (Math.Abs(diagonal) > maxDiagonal)
            {
                Aij = 0;
            }
            else
            {
                //i denotes the increasing number of elements inside k diagonal in the upper triangular (i>=1)
                //j denotes the increasing number of elements inside k diagonal in the lower triangular (j<1)
                //The minimum between i, j denotes if we are above or below the main diagonal 
                Aij = values[firstOfDiagonal + Math.Min(i, j)];
            }

             return Aij;
        }


        public static (double[], double[]) SkylineGTR1200PS(double[,] A)
        {
            List<double> values = new List<double>();
            List<double> diagonalOffsets = new List<double>();

            //for (int j = A.GetLength(1) - 1; j >= 0; j--) // column marching
            for (int j = 0; j < A.GetLength(1); j++) // column marching
            {
                // index of row. scans row from top to bottom to find first non-zero entry
                // in order to accelerate the next loop
                var k = 0;
                while (A[k, j] == 0 && k < j) { k++; }

                // Row marching from diagonal to the first non-zero entry
                // from top to bottom
                //for (int i = k; i <= j; i++)
                for (int i = j; i >= k; i--)
                {
                    values.Add(A[i, j]);
                    if (i == j) { diagonalOffsets.Add(values.Count - 1); }
                }
            }
            diagonalOffsets.Add(values.Count);

            //utilitiez.SkylinePrinter(A, values.ToArray(), diagonalOffsets.ToArray());

            return (values.ToArray(), diagonalOffsets.ToArray());
        }
        public static double SkylineIndexing(int i, int j, double[] values, double[] diagonalOffsets)
        {
            double Aij = 0d;
            if (i > j)
            {
                var dummy = i;
                i = j;
                j = dummy;
            }
            // find the height of the column j (excluding the diagonal)
            var hj = diagonalOffsets[j + 1] - diagonalOffsets[j] - 1;

            // Determine if the entry Aij = Aji is inside the acive column.
            // If Aij is inside the active column return value. Else return 0.
            if (j - i <= hj) { Aij = values[(int)diagonalOffsets[j] + j - i]; }
            else { Aij = 0d; }

            return Aij;
        }
        public static void SkylineValueReplace(int i, int j, double newValue, double[] values, double[] diagonalOffsets)
        {
            if (i > j)
            {
                var dummy = i;
                i = j;
                j = dummy;
            }
            // find the height of the column j (excluding the diagonal)
            var hj = diagonalOffsets[j + 1] - diagonalOffsets[j] - 1;

            // Determine if the entry Aij = Aji is inside the acive column.
            // If Aij is inside the active column return value. Else return 0.
            if (j - i <= hj) { values[(int)diagonalOffsets[j] + j - i] = newValue; }

        }
    }
}
