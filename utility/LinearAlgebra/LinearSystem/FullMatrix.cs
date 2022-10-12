using System.Threading.Tasks;

namespace prizaLinearAlgebra
{
    public enum MatrixStorageFormat
    {
        Full,
        CSR,
        CSC,
        COO,
        Banded,
        Skyline
    }
    public class Matrix : IMatrix
    {
        public double[,] Elements { get; set; }
        public int Rows { get; }
        public int Columns { get; }
        public int NumberOfElements => Rows * Columns;
        public bool IsSymmetric { get; set; }

        public   Matrix(double [,] elements)
        {
            this.Elements = elements;
            this.Rows = elements.GetLength(0);
            this.Columns = elements.GetLength(1);
        }


        public void DeallocateMatrix()
        {
            Elements = null;
        }

        public Dictionary<Tuple<int, int>, double> AsDictionary()
        {
            var matrix = new Dictionary<Tuple<int, int>, double>();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    matrix.Add(new Tuple<int, int>(i, j), Elements[i,j]);
                }
            }
            return matrix;
        }


        public void CopyFromDictionary(Dictionary<Tuple<int, int>, double> matrix)
        {
            foreach (var matrixCoordinate in matrix.Keys)
            {
                Elements[matrixCoordinate.Item1, matrixCoordinate.Item2] = matrix[matrixCoordinate];
            }
        }


        public bool IsMatrixSymmetric()
        {
            if (Rows != Columns) { return false; }

            if (IsSymmetric == true) { return true; }

            for (int i = 1; i < Rows; i++)
            {
                for (int j = i; j < Columns; j++)
                {
                    if (Elements[i,j] != Elements[i,j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public double[,] Transpose()
        {
            var transpose = new double[Columns, Rows];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    transpose[j, i] = Elements[i,j];
                }
            }
            return transpose;
        }
        
        public double[,] Scale(double scalar)
        {
            var scaled = new double[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    scaled[i, j] = Elements[i,j] * scalar;
                }
            }
            return scaled;
        }

        public double[,] Add(double[,] matrix)
        {
            var added = new double[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    added[i, j] = Elements[i,j] + matrix[i, j];
                }
            }
            return added;
        }

        public double[,] Subtract(double[,] matrix)
        {
            var subtracted = new double[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    subtracted[i, j] = Elements[i,j] - matrix[i, j];
                }
            }
            return subtracted;
        }

        public double[,] MatrixMultiplyRight(double[,] matrix)
        {
            var multiplied = new double[Rows, matrix.GetLength(1)];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    for (int k = 0; k < Columns; k++)
                    {
                        multiplied[i, j] += Elements[i,j] * matrix[k, j];
                    }
                }
            }
            return multiplied;
        }

        public double[,] MatrixMultiplyLeft(double[,] matrix)
        {
            var multiplied = new double[matrix.GetLength(0), Columns];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    for (int k = 0; k < Rows; k++)
                    {
                        multiplied[i, j] += matrix[i, k] * Elements[k, j];
                    }
                }
            }
            return multiplied;
        }

        public double[] VectorMultiplyRight()
        {
            var multiplied = new double[Rows];
            for (int i = 0; i < Rows; i++)
            {
                switch (NumberOfElements)
                {
                    case > 10000:
                        Parallel.For(0, Columns, j =>
                        {
                            multiplied[i] += Elements[i,j];
                        });
                        break;
                        
                    case <= 10000:
                        for (int j = 0; j < Columns; j++)
                        {
                            multiplied[i] += Elements[i,j];
                        }
                        break;
                }

                for (int j = 0; j < Columns; j++)
                {
                    multiplied[i] += Elements[i,j];
                }
            }
            return multiplied;
        }
    }
}