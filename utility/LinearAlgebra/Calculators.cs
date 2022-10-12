public static class Calculators
{

    public static double[] vectorAddition(double[] a, double[] b)
    {
        var n = a.Length;
        double[] additionResult;
        additionResult = Calculator();

        double[] Calculator()
        {
            if (a.Length != b.Length) throw new ArgumentException("a and b should have the same length");
            var result = new double[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = a[i] + b[i];
            }
            return result;
        }
        return additionResult;
    }
    
    public static double[] vectorSubtraction(double[] a, double[] b)
    {
        var n = a.Length;
        double[] subtractionResult;
        subtractionResult = Calculator();

        double[] Calculator()
        {
            if (a.Length != b.Length) throw new ArgumentException("a and b should have the same length");
            var result = new double[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = a[i] - b[i];
            }
            return result;
        }
        return subtractionResult;
    }

    public static double[] vectorScalarMultiplication(double scalar, double[] a)
    {
        var n = a.Length;
        double[] vectorScalarMultiplicationResult;
        vectorScalarMultiplicationResult = Calculator();

        double[] Calculator()
        {
            var result = new double[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = scalar * a[i];
            }
            return result;
        }
        return vectorScalarMultiplicationResult;
    }

    public static double[] vectorEquals(double[] a)
    {
        var n = a.Length;
        double[] vectorEqResult;
        vectorEqResult = Calculator();

        double[] Calculator()
        {
            var result = new double[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = a[i];
            }
            return result;
        }
        return vectorEqResult;
    }

    public static double[] vectorLinearCombination(double scalar1, double scalar2, double[] a, double[] b)
    {
        var n = a.Length;
        double[] vectorLinearCombinationResult = Calculator();
        double[] Calculator()
        {
            var result = new double[n];
            for (int i = 0; i < n; i++)
            {
                result[i] = scalar1 * a[i] + scalar2 * b[i];
            }
            return result;
        }
        return vectorLinearCombinationResult;
    }

    public static double vectorDotProduct(double[] a, double[] b)
    {
        //ci = ai * bi
        var n = a.Length;
        double dotProductResult;
        dotProductResult = Calculator();

        double Calculator()
        {
            var result = 0d;
            for (int i = 0; i < n; i++)
            {
                result += a[i] * b[i];
            }
            return result;
        }
        return dotProductResult;
    }

    public static double[,] vectorTensorProduct(double[] a, double[] b)
    {
        var n = a.Length;
        var m = b.Length;
        double[,] vectorTensorProductResult = new double[n, m];
        vectorTensorProductResult = Calculator();


        double[,] Calculator()
        {
            var result = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result[i, j] = a[i] * b[j];
                }
            }
            return result;
        }
        return vectorTensorProductResult;
    }

    public static double[,] MatrixTranspose(double[,] A)
    {
        //ATij = Aji
        var n = A.GetLength(0);

        double[,] ATranspose;
        ATranspose = Calculator();
        double[,] Calculator()
        {
            var result = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++) { result[i, j] = A[j, i]; }
            }
            return result;
        }
        return ATranspose;
    }

    public static double[] VectorMatrixMultiplication(double[,] A, double[] a)
    {
        // bi = aj * Aji 
        var n = A.GetLength(0);
        double[] vectorMatrixMultResult;
        vectorMatrixMultResult = Calculator();

        double[] Calculator()
        {
            var result = new double[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++) { result[i] += a[j] * A[j, i]; }
            }
            return result;
        }
        return vectorMatrixMultResult;
    }

    public static double[] MatrixVectorMultiplication(double[,] A, double[] a)
    {
        // bi = Aij *bj  
        var n = A.GetLength(0);
        double[] matrixVectorMultResult;
        matrixVectorMultResult = Calculator();

        double[] Calculator()
        {
            var result = new double[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++) { result[i] += A[i, j] * a[j]; }
            }
            return result;
        }
        return matrixVectorMultResult;
    }

    public static double[,] MatrixMatrixMultiplication(double[,] A, double[,] B)
    {
        //Cij = AikAkj
        var n = A.GetLength(0);
        var MatrixMatrixMult = Calculator();

        double[,] Calculator()
        {
            var sum = 0d;
            var result = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    sum = 0d;
                    for (int k = 0; k < n; k++) { sum += A[i, k] * B[k, j]; }
                    result[i, j] = sum;
                }
            }
            //var result = Matrix<double>.Build.DenseOfArray(A).Multiply(Matrix<double>.Build.DenseOfArray(B)).ToArray();
            return result;
        }
        return MatrixMatrixMult;
    }

    public static double RadToDegrees (double rad)
    {
        double pi = Math.Acos(-1d);
        return  rad * 180d * pi;
    }

    public static double DegreesToRad(double degrees)
    {
        double pi = Math.Acos(-1d);
        return degrees * pi / 180d;
    }

    // public static double[,] MatrixInverse(double[,] A)
    // {
    //     var n = A.GetLength(0);
    //     double[,] AInverse = new double[n, n];
    //     var Inverse = Matrix<double>.Build.DenseOfArray(A).Inverse().ToArray();
    //     return Inverse;
    // }

    public static double Norm2(double[] x, double[] xOld)
    {
        var sum = 0d;
        for (int i = 0; i < x.Length; i++) { sum += Math.Pow((x[i] - xOld[i]), 2); }
        var norm2 = Math.Sqrt(sum) / x.Length;
        return norm2;
    }

    public static bool IsMatrixSymmetric (double[,] A)
    {
        if (A.GetLength(0) != A.GetLength(1))
        {
            throw new Exception("Matrix is not square");
        }

        var n = A.GetLength(0);
        var isSymmetric = true;
        for (int i = 0; i < n; i++)
        {
            for (int j = i; j < n; j++)
            {
                if (A[i, j] != A[j, i])
                {
                    isSymmetric = false;
                    break;    
                }
            }
        }
        return isSymmetric;
    }

}