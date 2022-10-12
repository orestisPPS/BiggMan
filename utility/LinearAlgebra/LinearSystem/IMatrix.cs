namespace prizaLinearAlgebra
{
    public interface IMatrix
    {   double[,] Elements {get; set;}
        int Rows {get;}

        int Columns {get;}

        int NumberOfElements => Rows * Columns;

        bool IsSymmetric { get; set; }

        Dictionary<Tuple<int, int>, double> AsDictionary();
        
        //void CopyFromArray(double[,] array);

        void CopyFromDictionary(Dictionary<Tuple<int, int>, double> matrix);

        bool IsMatrixSymmetric();

        double[,] Transpose();

        double[,] Add(double[,] matrix);

        double[,] Subtract(double[,] matrix);

        double[,] MatrixMultiplyRight(double[,] matrix);
        
        double[,] MatrixMultiplyLeft(double[,] matrix);

    }
}