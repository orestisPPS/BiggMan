namespace utility
{

    public static class TransformationTensors
    {



        static TransformationTensors()
        { }
        public static  double[] Rotate(double[] array, double theta)
        {
            theta = Calculators.DegreesToRad(theta);
            var rotationTensor2D = new double[,] { { Math.Cos(theta), -Math.Sin(theta)},
                                                   { Math.Sin(theta),  Math.Cos(theta)}};

            var result = Calculators.MatrixVectorMultiplication(rotationTensor2D, array);
            return result;
        }

        public static  double[] Shear(double[] array, double shearAngleX, double shearAngleY)
        {
            var shearX = Calculators.DegreesToRad(shearAngleX);
            var shearY = Calculators.DegreesToRad(shearAngleY);

            var shearTensor2D    = new double[,] { { 1d,               Math.Tan(shearX)},
                                                   { Math.Tan(shearY),               1d}};    

            var result = Calculators.MatrixVectorMultiplication(shearTensor2D, array);
            return result;
        }

        public static double[] Scale(double[] array, double stepX, double stepY)
        {
            var scaleTensor2D    = new double[,] { { stepX,     0},
                                                   { 0,     stepY}};

            var result = Calculators.MatrixVectorMultiplication(scaleTensor2D, array);
            return result;
        }

        public static double[] MirrorX(double[] array)
        {
            var MirrorTensor2D    = new double[,] { { 1d,  0d},
                                                   { 0d, -1d}};
            var result = Calculators.MatrixVectorMultiplication(MirrorTensor2D, array);
            return result;
        }

        public static double[] MirrorY(double[] array)
        {
            var MirrorTensor2D    = new double[,] { { -1d, 0d},
                                                   {  0d, 1d}};
            var result = Calculators.MatrixVectorMultiplication(MirrorTensor2D, array);
            return result;
        }

        public static double[] Translate(double[] array, double hx, double hy)
        {
            var TranslationalTensor    = new double[,] { { array[0] + hx, 0d           },
                                                         { 0d,            array[1] + hy}};
            var result = Calculators.MatrixVectorMultiplication(TranslationalTensor, array);
            return result;
        }
    }



}
