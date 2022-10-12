namespace prizaLinearAlgebra;

public class CSR
{

    /// <summary>
    /// The non zero values of the matrix (m x n)
    /// </summary>
    /// <value></value>
    public List < double > Values { get; internal set; }
    
    /// <summary>
    /// The column where the non zero values belong.
    /// Has lengthq of the number of non zero values
    /// </summary>
    /// <value></value>
    public List < int > ColumnIndices { get; internal set; }

    /// <summary>
    /// rowOffsets[i] is the index in Values of the first non zero value in row i.
    /// Has length of the number of rows + 1. The extra entry is the number of non zero values.
    /// /// </summary>
    /// <value></value>
    public List < int > RowOffsets { get; internal set; }

    public int Rows { get; internal set; }

    public int Columns { get; internal set; }

    /// <summary>
    /// CSR (Compressed Sparse Row) format uses a row major layout and is more effective
    /// for Matrix-Vector multiplication, and matirx - matrix multiplication when it is on the LEFT
    /// Ax, AB. The memory requirements are 2 * nnz + m + 1
    /// </summary>
    /// <param name="matrix"></param>
    public CSR(double [,] matrix)
    {
        (this.Values, this.ColumnIndices, this.RowOffsets) = StoreInSparseFormat(matrix);
        Rows = matrix.GetLength(0);
        Columns = matrix.GetLength(1);
    }
    
    private (List<double> values ,List<int> colIndices, List<int>rowOffsets) StoreInSparseFormat(double [,] A)
    {
        List < double > values = new List < double > ();
        List < int > colIndices = new List < int > ();
        List < int > rowOffsets = new List < int > ();

        for (int i = 0; i < Rows; i++)
        {
            var foundFirstElementOfRow = false;
            for (int j = 0; j < Columns; j++)
            {
                if (A[i, j] != 0d)
                {
                    values.Add(A[i, j]);
                    var positionInValues = Values.Count - 1;

                    colIndices.Add(j);
                    if (foundFirstElementOfRow == false)
                    {
                        foundFirstElementOfRow = true;
                        rowOffsets.Add(positionInValues);
                    }
                }
            }
        }
        rowOffsets.Add(Values.Count);
        return (values, colIndices, rowOffsets);
    }


    public void Transpose()
    {
        
        
    }

    public void Scale(int scalar)
    {
        for (int i = 0; i < Values.Count; i++)
        {
            Values[i] *= scalar;
        }
    }

    /// <summary>
    /// This method performs matrix addition A + B. The two matrices are stored in CSR format.
    ///  The input is transformed from Full Matrix Format to CSR.
    /// </summary>
    public void Add(double[,] matrix)
    {
        var sparseMatrix = StoreInSparseFormat(matrix);
        var values2 = sparseMatrix.values;
        var colIndices2 = sparseMatrix.colIndices;
        var rowOffsets2 = sparseMatrix.rowOffsets;
        if (Rows != matrix.GetLength(0) || Columns != matrix.GetLength(1))
        {
            throw new Exception("The two matrices are not of the same size");
        }
        
    }

    public void Subtract()
    {
        throw new NotImplementedException();
    }

    public void MatrixMultiplyRight(double[,] matrix)
    {
        throw new NotImplementedException();
    }

    public void MatrixMultiplyLeft(double[,] matrix)
    {
        throw new NotImplementedException();
    }

    public void VectorMultiplyRight(double[] vector)
    {
        throw new NotImplementedException();
    }
} 