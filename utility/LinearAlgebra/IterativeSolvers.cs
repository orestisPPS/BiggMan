using System;
using System.Collections.Generic;
using System.Text;
using utility;
using System.Threading;
using System.Threading.Tasks;

namespace prizaLinearAlgebra
{

    public static class IterativeSolvers
    {
        public static double[] JacobiSolver(double[,] A, double[] b, double stopCriterion)
        {
            utilitiez.Title("Jacobi");
            var n = b.Length;
            double[] x = new double[n];
            double[] xOld = new double[n];
            double[] residual = new double[n];

            var Error = 1d;
            var iterations = 1;
            var initialGuess = 0d;


            // Initial guess
            Initialize(initialGuess);

            // Iterative Solution
            IterativeSolution();

            //Residual Calculator
            ResidualCalculator();

            //Print
            //IterativeMethodPrinter(A, b, x, iterations, residual);

            return x;

            void Initialize(double initialGuess)
            {
                for (int i = 0; i < n; i++)
                {
                    x[i] = initialGuess;
                    xOld[i] = initialGuess;
                }
            }

            void IterativeSolution()
            {
                //Iterative solution
                while (Error > stopCriterion && iterations <= Math.Pow(10, 6))
                {
                    Error = 0d;
                    for (int i = 0; i < n; i++)
                    {
                        var sum = 0d;

                        for (int j = 0; j < n; j++)
                        {
                            if (i != j) { sum += A[i, j] * xOld[j]; }
                        }
                        x[i] = (b[i] - sum) / A[i, i];

                        ////Find error
                        Error += Math.Sqrt(Math.Pow(x[i] - xOld[i], 2));

                        ////Update values of x
                        xOld[i] = x[i];
                    }
                    Error /= n;
                    iterations++;


                }
            }

            void ResidualCalculator()
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        residual[i] += A[i, j] * x[j];
                    }
                    residual[i] -= b[i];
                    residual[i] = Math.Abs(residual[i]);
                }
            }
        }
        public static double[] ParallelJacobiSolver(double[,] A, double[] b, double stopCriterion)
        {
            utilitiez.Title("Parallel Jacobi");
            var n = b.Length;
            double[] x = new double[n];
            double[] xOld = new double[n];
            double[] residual = new double[n];

            var Error = 1d;
            var iterations = 1;
            var initialGuess = 0d;


            // Initial guess
            Initialize(initialGuess);

            // Iterative Solution
            IterativeSolution();

            //Residual Calculator
            ResidualCalculator();

            //Print
            //IterativeMethodPrinter(A, b, x, iterations, residual);

            return x;

            void Initialize(double initialGuess)
            {
                for (int i = 0; i < n; i++)
                {
                    x[i] = initialGuess;
                    xOld[i] = initialGuess;
                }
            }

            void IterativeSolution()
            {
                //Iterative solution
                while (Error > stopCriterion && iterations <= Math.Pow(10, 6))
                {
                    Error = 0d;
                    Parallel.For(0, n, i =>
                    {
                        var sum = 0d;

                        for (int j = 0; j < n; j++)
                        {
                            if (i != j) { sum += A[i, j] * xOld[j]; }
                        }
                        x[i] = (b[i] - sum) / A[i, i];

                        ////Find error
                        Error += Math.Sqrt(Math.Pow(x[i] - xOld[i], 2));

                        ////Update values of x
                        xOld[i] = x[i];
                    });
                    Error /= n;
                    iterations++;

                    Console.WriteLine("iteration: " + iterations + "   Error: " + Error.ToString("E10"));
                }
            }

            void ResidualCalculator()
            {
                Parallel.For(0, n - 1, i =>
                {
                    for (int j = 0; j < n; j++)
                    {
                        residual[i] += A[i, j] * x[j];
                    }
                    residual[i] -= b[i];
                    residual[i] = Math.Abs(residual[i]);
                });
            }
        }
    
        public static double[] GaussSeidelSolver(double[,] A, double[] b, double stopCriterion)
        {
            utilitiez.Title("Gauss Seidel");
            var n = b.Length;
            double[] x = new double[n];
            double[] xOld = new double[n];
            double[] residual = new double[n];

            var Error = 1d;
            var iterations = 1;
            var initialGuess = 0d;

            // Initial guess
            Initialize(initialGuess);

            // Iterative Solution
            IterativeSolution();

            //Residual Calculator
            ResidualCalculator();

            //Print
            //IterativeMethodPrinter(A, b, x, iterations, residual);

            return x;

            void Initialize(double initialGuess)
            {
                for (int i = 0; i < n; i++)
                {
                    x[i] = initialGuess;
                    xOld[i] = initialGuess;
                }
            }

            void IterativeSolution()
            {
                //Iterative solution
                while (Error > stopCriterion && iterations <= Math.Pow(10, 6))
                {
                    Error = 0d;
                    for (int i = 0; i < n; i++)
                    {
                        var sum = 0d;

                        for (int j = 0; j < n; j++)
                        {
                            if (i != j)
                            {
                                if (i < j) { sum += A[i, j] * x[j]; }
                                else if (i > j) { sum += A[i, j] * xOld[j]; }
                            }
                        }
                        x[i] = (b[i] - sum) / A[i, i];

                        ////Find error
                        Error += Math.Sqrt(Math.Pow(x[i] - xOld[i], 2));

                        ////Update values of x
                        xOld[i] = x[i];
                    }
                    Error /= n;
                    iterations++;

                    Console.WriteLine("iteration: " + iterations + "   Error: " + Error);
                }
            }

            void ResidualCalculator()
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        residual[i] += A[i, j] * x[j];
                    }
                    residual[i] -= b[i];
                    residual[i] = Math.Abs(residual[i]);
                }
            }
        }

        public static double[] SORSolver(double[,] A, double[] b, double stopCriterion)
        {
            utilitiez.Title("SOR");
            var n = b.Length;
            double[] x = new double[n];
            double[] xOld = new double[n];
            double[] residual = new double[n];

            var Error = 1d;
            var iterations = 1;
            var initialGuess = 0d;

            // Relaxation parameter ω [0,2]
            var w = 1.2;

            // Initial guess
            Initialize(initialGuess);

            // Iterative Solution
            IterativeSolution();

            //Residual Calculator
            ResidualCalculator();

            //Print
            utilitiez.IterativeMethodPrinter(A, b, x, iterations, residual);

            return x;

            void Initialize(double initialGuess)
            {
                for (int i = 0; i < n; i++)
                {
                    x[i] = initialGuess;
                    xOld[i] = initialGuess;
                }
            }

            void IterativeSolution()
            {
                //Iterative solution
                while (Error > stopCriterion && iterations <= Math.Pow(10, 6))
                {
                    Error = 0d;
                    for (int i = 0; i < n; i++)
                    {
                        var sum = 0d;

                        for (int j = 0; j < n; j++)
                        {
                            if (i != j)
                            {
                                if (i < j) { sum += A[i, j] * x[j]; }
                                else if (i > j) { sum += A[i, j] * xOld[j]; }
                            }
                        }
                        x[i] = (1 - w) * xOld[i] + (b[i] - sum) * (w / A[i, i]);

                        ////Find error
                        Error += Math.Sqrt(Math.Pow(x[i] - xOld[i], 2));

                        ////Update values of x
                        xOld[i] = x[i];
                    }
                    Error /= n;
                    iterations++;

                    Console.WriteLine("iteration: " + iterations + "   Error: " + Error);
                }
            }

            void ResidualCalculator()
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        residual[i] += A[i, j] * x[j];
                    }
                    residual[i] -= b[i];
                    residual[i] = Math.Abs(residual[i]);
                }
            }
        }

        public static double[] SSORSolver(double[,] A, double[] b, double stopCriterion)
        {
            utilitiez.Title("SSOR");
            var n = b.Length;
            double[] x = new double[n];
            double[] xOld1 = new double[n];
            double[] xOld2 = new double[n];
            double[] residual = new double[n];

            var Error = 1d;
            var iterations = 1;
            var initialGuess = 0d;

            // Relaxation parameter ω [0,2]
            var w = 0.995;

            // Initial guess
            Initialize(initialGuess);

            // Iterative Solution
            IterativeSolution();

            //Residual Calculator
            ResidualCalculator();

            //Print
            utilitiez.IterativeMethodPrinter(A, b, x, iterations, residual);

            return x;

            void Initialize(double initialGuess)
            {
                for (int i = 0; i < n; i++)
                {
                    x[i] = initialGuess;
                    xOld1[i] = initialGuess;
                    xOld2[i] = initialGuess;
                }
            }

            void IterativeSolution()
            {
                //Iterative solution
                while (Error > stopCriterion && iterations <= Math.Pow(10, 6))
                {
                    Error = 0d;

                    // Forward marching
                    for (int i = 0; i < n; i++)
                    {
                        var sum = 0d;

                        for (int j = 0; j < n; j++)
                        {
                            if (i != j)
                            {
                                if (i < j) { sum += A[i, j] * xOld1[j]; }
                                else if (i > j) { sum += A[i, j] * xOld2[j]; }
                            }
                        }
                        xOld1[i] = xOld2[i] + w * ((b[i] - sum) / A[i, i] - xOld2[i]);
                    }

                    // Backward marching
                    for (int i = n - 1; i >= 0; i--)
                    {
                        var sum = 0d;

                        for (int j = 0; j < n; j++)
                        {
                            if (i != j)
                            {
                                if (i < j) { sum += A[i, j] * xOld1[j]; }
                                else if (i > j) { sum += A[i, j] * x[j]; }
                            }
                        }
                        x[i] = xOld1[i] + w * ((b[i] - sum) / A[i, i] - xOld1[i]);

                        ////Find error
                        Error += Math.Sqrt(Math.Pow(x[i] - xOld1[i], 2));

                        ////Update values of x
                        xOld2[i] = xOld1[i];
                        xOld1[i] = x[i];
                    }
                    Error /= 2 * n;
                    iterations++;

                    Console.WriteLine("iteration: " + iterations + "   Error: " + Error);
                }
            }

            void ResidualCalculator()
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        residual[i] += A[i, j] * x[j];
                    }
                    residual[i] -= b[i];
                    residual[i] = Math.Abs(residual[i]);
                }
            }
        }

        public static double[] ConjugateGradient(double[,] A, double[] vector, double stopCriterion)
        {
            utilitiez.Title("Conjugate Gradient");

            var values = SparseMatrixStorage.CSR(A).Item1;
            var column = SparseMatrixStorage.CSR(A).Item2;
            var rowOffset = SparseMatrixStorage.CSR(A).Item3;


            var n = vector.Length;
            double[] xNew = new double[n];
            double[] xOld = new double[n];

            double[] rNew = new double[n];
            double[] rOld = new double[n];

            double[] dNew = new double[n];
            double[] dOld = new double[n];

            double a;
            double b;

            double rOldrOldT = 0;
            double rrT = 0;

            var Error = 1d;
            var iterations = 1;
            var initialGuess = 100000;

            // Initial guess
            Initialize(initialGuess);

            //Residual Calculator
            rOld = ResidualCalculatorSparse();

            //  Initialize direction vector
            for (int i = 0; i < n; i++)
            {
                dOld[i] = rOld[i];
            }

            // Iterative Solution
            IterativeSolution();

            //Print
            utilitiez.CSRPrinter(A, values, column, rowOffset);
            utilitiez.CompressedIterativeMethodPrinter(vector, xNew, iterations, rNew);

            return xNew;

            void Initialize(double initialGuess)
            {
                for (int i = 0; i < n; i++)
                {
                    xOld[i] = initialGuess;
                }
            }

            void IterativeSolution()
            {

                double dOldAdOld = 0d;

                //Iterative solution
                while (Error > stopCriterion && iterations <= Math.Pow(10, 5))
                {

                    Error = 0d;

                    var AdOld = SparseMatrixStorage.CSRMatrixVectorMultiplication(values, column, rowOffset, dOld);

                    for (int i = 0; i < n; i++)
                    {
                        rOldrOldT += rOld[i] * rOld[i];
                        dOldAdOld += dOld[i] * AdOld[i];
                    }
                    a = rOldrOldT / dOldAdOld;

                    for (int i = 0; i < n; i++)
                    {
                        xNew[i] = xOld[i] + a * dOld[i];
                        rNew[i] = rOld[i] - a * AdOld[i];

                        ////Find error
                        Error += Math.Sqrt(Math.Pow(rNew[i] - rOld[i], 2));
                        //Error += Math.Sqrt(Math.Pow(rNew[i], 2)) / Math.Sqrt(Math.Pow(rOld[i], 2));
                        //Error += Math.Abs(1 - rNew[i] / rOld[i]);
                    }
                    Error /= n;
                    a = 0;

                    iterations++;
                    Console.WriteLine("iteration: " + iterations + "   Error: " + Error);

                    if (Error < stopCriterion) { break; }
                    else
                    {
                        for (int i = 0; i < n; i++)
                        {
                            rrT += rNew[i] * rNew[i];
                        }
                        b = rrT / rOldrOldT;

                        for (int i = 0; i < n; i++)
                        {
                            // New direction vector
                            dNew[i] = rNew[i] + b * dOld[i];

                            ////Update values of x
                            xOld[i] = xNew[i];
                            //Update direction vectors
                            dOld[i] = dNew[i];
                            //Update residuals
                            rOld[i] = rNew[i];
                        }
                    }
                    // Update residual vectors dot product
                    //rOldrOldT = rrT;
                }
            }

            //Print


            double[] ResidualCalculatorSparse()
            {

                var residual = new double[vector.Length];
                var Ax = SparseMatrixStorage.CSRMatrixVectorMultiplication(values, column, rowOffset, xOld);
                for (int i = 0; i < n; i++)
                {
                    residual[i] = vector[i] - Ax[i];
                }
                return residual;
            }
        }

        public static double[] PreconditionedConjugateGradient(double[,] A, double[] vector, double stopCriterion)
        {
            utilitiez.Title("Pre Conditioned Conjugate Gradient / Jacobi Preconditioner");
            


            var n = vector.Length;
            double[] xNew = new double[n];
            double[] xOld = new double[n];

            double[] rNew = new double[n];
            double[] rOld = new double[n];

            double[] dNew = new double[n];
            double[] dOld = new double[n];
            
            double[] sNew = new double[n];
            double[] sOld = new double[n];

            double a;
            double b;

            // Create Preconditioner
            //var MInverse = Preconditioners.JacobiPc.CreatePCMatrix(A);
            var MInverse = Preconditioners.JacobiPc.CreatePCMatrix(A);
            //matrixPrinter(MInverse);

            // Save matrix in sparse form
            var values = SparseMatrixStorage.CSR(A).Item1;
            var column = SparseMatrixStorage.CSR(A).Item2;
            var rowOffset = SparseMatrixStorage.CSR(A).Item3;

            double rTs = 0;

            var Error = 1d;
            var iterations = 1;
            var initialGuess = 0d;

            // Initial guess
            Initialize(initialGuess);

            //Residual Calculator
            rOld = ResidualCalculatorSparse();

            //  Initialize direction vector
            dOld = Calculators.MatrixVectorMultiplication(MInverse, rOld);
            sOld = Calculators.vectorEquals(dOld);

            // Calculate r0T * s0 for error
            //var r0s0 = Calculators.vectorDotProduct(rOld, sOld);

            // Iterative Solution
            IterativeSolution();

            //Print
            //CSRPrinter(A, values, column, rowOffset);
            //CompressedIterativeMethodPrinter(vector, xNew, iterations, rNew);

            return xNew;

            void Initialize(double initialGuess)
            {
                for (int i = 0; i < n; i++)
                {
                    xOld[i] = initialGuess;
                }
            }

            void IterativeSolution()
            {
                while (Error > stopCriterion && iterations <= Math.Pow(10, 5))
                {
                    Error = 0d;

                    var AdOld = SparseMatrixStorage.CSRMatrixVectorMultiplication(values, column, rowOffset, dOld);
                    double rOldTsOld = Calculators.vectorDotProduct(rOld, sOld);
                    double dOldAdOld = Calculators.vectorDotProduct(dOld, AdOld);
                    a = rOldTsOld / dOldAdOld;

                    xNew = Calculators.vectorAddition(xOld, Calculators.vectorScalarMultiplication(a, dOld));
                    rNew = Calculators.vectorSubtraction(rOld, Calculators.vectorScalarMultiplication(a, AdOld));
                    sNew = Calculators.MatrixVectorMultiplication(MInverse, rNew);

                    //Find error
                    for (int i = 0; i < n; i++) { Error += Math.Sqrt(Math.Pow(rNew[i] - rOld[i], 2)); }
                    Error /= n;
                    //Error = Calculators.vectorDotProduct(rNew, sNew) / r0s0;
                    //Error = Calculators.vectorDotProduct(rNew, sNew) / Calculators.vectorDotProduct(rOld, sOld);
                    
                    
                    Console.WriteLine("iteration: " + iterations + "   Error: " + Error);
                    iterations++;

                    if (Error < stopCriterion) { break; }
                    else
                    {
                        rTs = Calculators.vectorDotProduct(rNew, sNew);
                        b = rTs / rOldTsOld;
                        dNew = Calculators.vectorAddition(sNew, Calculators.vectorScalarMultiplication(b, dOld));

                        //update values
                        xOld = Calculators.vectorEquals(xNew);
                        dOld = Calculators.vectorEquals(dNew);
                        rOld = Calculators.vectorEquals(rNew);
                        sOld = Calculators.vectorEquals(sNew);
                    }
                }
            }

            double[] ResidualCalculatorSparse()
            {

                var residual = new double[vector.Length];
                var Ax = SparseMatrixStorage.CSRMatrixVectorMultiplication(values, column, rowOffset, xOld);
                for (int i = 0; i < n; i++)
                {
                    residual[i] = vector[i] - Ax[i];
                }
                return residual;
            }
        }

        public static double[] SteepestDescent(double[,] A, double[] vector, double stopCriterion)
        {
            utilitiez.Title("Steepest Descent");

            var values = SparseMatrixStorage.COO(A).Item1;
            var column = SparseMatrixStorage.COO(A).Item2;
            var rowOffset = SparseMatrixStorage.COO(A).Item3;


            var n = vector.Length;
            double[] xNew = new double[n];
            double[] xOld = new double[n];

            double[] rNew = new double[n];
            double[] rOld = new double[n];

            double a;

            double rOldrOldT = 0;
            double rOldTArOld = 0;

            var Error = 1d;
            var iterations = 1;
            var initialGuess = 0.0;

            // Initial guess
            Initialize(initialGuess);

            //Residual Calculator
            rOld = ResidualCalculatorSparse();

            // Iterative Solution
            IterativeSolution();

            //Print
            //CSRPrinter(A, values, column, rowOffset);
            utilitiez.CompressedIterativeMethodPrinter(vector, xNew, iterations, rNew);

            return xNew;

            void Initialize(double initialGuess)
            {
                for (int i = 0; i < n; i++)
                {
                    xOld[i] = initialGuess;
                }
            }

            void IterativeSolution()
            {
                while (Error > stopCriterion && iterations <= Math.Pow(10, 6))
                {
                    Error = 0d;

                    var ArOld = SparseMatrixStorage.COOTransposeMatrixVectorMultiplication(values, column, rowOffset, rOld);

                    for (int i = 0; i < n; i++)
                    {
                        rOldrOldT += rOld[i] * rOld[i];
                        rOldTArOld += rOld[i] * ArOld[i];
                    }
                    a = rOldrOldT / rOldTArOld;

                    for (int i = 0; i < n; i++)
                    {
                        //New x
                        xNew[i] = xOld[i] + a * rOld[i];

                        //New Residual
                        rNew[i] = rOld[i] - a * ArOld[i];


                        ////Find error
                        Error += Math.Sqrt(Math.Pow(rNew[i] - rOld[i], 2));
                        ////Update values of x
                        xOld[i] = xNew[i];
                        //Update residuals
                        rOld[i] = rNew[i];
                    }
                    Error /= n;
                    a = 0;

                    iterations++;
                    Console.WriteLine("iteration: " + iterations + "   Error: " + Error);
                }
            }

            double[] ResidualCalculatorSparse()
            {

                var residual = new double[vector.Length];
                var Ax = SparseMatrixStorage.COOMatrixVectorMultiplication(values, column, rowOffset, xOld);
                for (int i = 0; i < n; i++)
                {
                    residual[i] = vector[i] - Ax[i];
                }
                return residual;
            }
        }

        public static double NewtonRaphson1(Func<double, double> f)
        {
            utilitiez.Title("Newton - Raphson 1 equation solver");

            double xNew = 0;
            double xOld = 1E-4;
            double c = 0d;

            double relativeError = 1d;       // Relative error
            double stopCriterion = 1E-12;    // The iterrative procedure stops when the difference between two values of the creterion for govergence, reaches this value
            double maxIter = 1E7;            // The iterrative procedure stops when the NR iterations, reaches this value
            int NewtonIterations = 0;

            while (relativeError > stopCriterion & NewtonIterations < maxIter)
            {
                //Differentiate
                var f1x1 = Derivative1_1(f, xOld);

                //calculate new increment
                c = f(xOld) / f1x1;

                //calculate new x
                xNew = xOld - c;

                //calculate error (norm 2)
                relativeError = Math.Sqrt(Math.Pow(xOld - xNew, 2));

                xOld = xNew;
                NewtonIterations++;

                Console.WriteLine("iteration: " + NewtonIterations + "  error: " + relativeError);
            }
            return xNew;
            double Derivative1_1(Func<double, double> f, double x)
            {
                var dx1 = 10E-3;
                var fx1 = f(x + dx1) - f(x - dx1) / (2d * dx1);
                return fx1;
            }
 
        }

        public class NewtonRaphson
        {
            public bool converganceAchieved = true;
            public NewtonRaphson() { }

            public class NR1 : NewtonRaphson
            {
                /// <summary>
                /// The solved equation (linear & non - linear) 
                /// </summary>
                public Func<double, double> F { get; }
                /// <summary>
                /// The updated value of the unknown variable
                /// </summary>
                public double xNew = 0;
                /// <summary>
                /// The old value of the unknown variable
                /// </summary>
                private double xOld = 1E-4;
                /// <summary>
                /// Increment of the Newton - Raphson method f(x)/f'(x)
                /// </summary>
                private double c = 0;
                /// <summary>
                /// Relative error. Is defined by the second norm
                /// </summary>
                private double relativeError = 1;
                /// <summary>
                /// The desired value of relative error
                /// </summary>
                public double StopCriterion { get; }
                /// <summary>
                /// The maximum value of iterations. After that the method does not converge
                /// </summary>
                public double MaxIter { get; }
                /// <summary>
                /// The counter of active Newton - Raphson iterations
                /// </summary>
                public int NRIterations = 0;

                public NR1(Func<double, double> f, double stopCriterion, double maxIter)
                {
                    this.F = f;
                    this.StopCriterion = stopCriterion;
                    this.MaxIter = maxIter;

                    Solve();
                    if (NRIterations >= MaxIter)
                    {
                        converganceAchieved = false;
                        Console.WriteLine("ATTENTION!!!!!      --------METHOD DID NOT CONVERGE-----");
                    }
                    void Solve()
                    {
                        while (relativeError > stopCriterion & NRIterations < maxIter)
                        {
                            //Differentiate
                            var f1x1 = Derivative1_1(F, xOld);

                            //calculate new increment
                            c = F(xOld) / f1x1;

                            //calculate new x
                            xNew = xOld - c;

                            //calculate error (norm 2)
                            relativeError = Math.Sqrt(Math.Pow(xOld - xNew, 2));

                            xOld = xNew;
                            NRIterations++;

                            //Console.WriteLine("iteration: " + NRIterations + "  error: " + relativeError);
                        }
                        double Derivative1_1(Func<double, double> f, double x)
                        {
                            var dx1 = 10E-3;
                            var fx1 = (f(x + dx1) - f(x - dx1)) / (2d * dx1);
                            return fx1;
                        }

                    }
                }
            }

            public class NR2 : NewtonRaphson
            {
                /// <summary>
                /// The first equation of the system(linear & non - linear) 
                /// </summary>
                public Func<double, double, double> F1 { get; }
                /// <summary>
                /// The second equation of the system(linear & non - linear) 
                /// </summary>
                public Func<double, double, double> F2 { get; }
                /// <summary>
                /// The updated value of the  unknown variableS
                /// </summary>
                public double[] xNew = new double[2];
                /// <summary>
                /// The old value of the unknown variableS
                /// </summary>
                private double[] xOld = new double[2];
                /// <summary>
                /// Increment of the Newton - Raphson method f(x)/f'(x)
                /// </summary>
                private double[] c = new double[2];
                /// <summary>
                /// Relative error. Is defined by the second norm
                /// </summary>
                private double relativeError = 1;
                /// <summary>
                /// The desired value of relative error
                /// </summary>
                public double StopCriterion { get; }
                /// <summary>
                /// The maximum value of iterations. After that the method does not converge
                /// </summary>
                public double MaxIter { get; }
                /// <summary>
                /// The counter of active Newton - Raphson iterations
                /// </summary>
                public int NRIterations = 0;

                public NR2(Func<double, double, double> f1, Func<double, double, double> f2, double stopCriterion, double maxIter)
                {
                    this.F1 = f1;
                    this.F2 = f2;
                    this.StopCriterion = stopCriterion;
                    this.MaxIter = maxIter;
                    Initialize(1E-4, 0);
                    Solve();
                    if (NRIterations >= MaxIter)
                    {
                        converganceAchieved = false;
                        Console.WriteLine("--------------------ATTENTION--------------");
                        Console.WriteLine("----------METHOD DID NOT CONVERGE----------");
                    }
                }
                private void Solve()
                {
                    while (relativeError > StopCriterion & NRIterations < MaxIter)
                    {
                        //Differentiate
                        var (f1x1, f1x2) = Derivative1_2(F1, xOld);
                        var (f2x1, f2x2) = Derivative1_2(F2, xOld);

                        //calculate new increment
                        double[,] A = { { f1x1, f1x2 }, { f2x1, f2x2 } };
                        double[] rhs = { F1(xOld[0], xOld[1]), F2(xOld[0], xOld[1]) };
                        c = DirectSolvers.CholeskySolver(A, rhs);

                        //calculate new x
                        xNew = Calculators.vectorSubtraction(xOld, c);

                        //calculate error (norm 2)
                        relativeError = Calculators.Norm2(xNew, xOld);

                        xOld = Calculators.vectorEquals(xNew);
                        NRIterations++;

                        //Console.WriteLine("iteration: " + NRIterations + "  error: " + relativeError);
                    }
                }
                private void Initialize(double initialGuessX, double initialGuessC)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        xOld[i] = initialGuessX;
                        c[i] = initialGuessC;
                    }
                }
                private (double, double) Derivative1_2(Func<double, double, double> f, double[] x)
                {
                    var dx1 = 10E-3;
                    var dx2 = 10E-3;

                    var fx1 = (f(xOld[0] + dx1, xOld[1]) - f(xOld[0] - dx1, xOld[1])) / (2d * dx1);
                    var fx2 = (f(xOld[0], xOld[1] + dx2) - f(xOld[0], xOld[1] - dx2)) / (2d * dx2);

                    return (fx1, fx2);
                }
            }

        }
    }
}
