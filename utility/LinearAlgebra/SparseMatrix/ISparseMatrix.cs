namespace prizaLinearAlgebra
{
    public interface ISparseMatrix
    {
        void Transpose();

        void Scale(double scalar);

        void Add(double[,] matrix);

        void Subtract(double[,] matrix);

        void MatrixMultiplyRight(double[,] matrix);

        void MatrixMultiplyLeft(double[,] matrix);

        void VectorMultiplyRight(double[] vector);

    }
}