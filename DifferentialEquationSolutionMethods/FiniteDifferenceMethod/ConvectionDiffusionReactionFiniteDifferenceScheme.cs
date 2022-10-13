using DifferentialEquations;
using Discretization;
using MathematicalProblems;
using prizaLinearAlgebra;
using System.Threading;
using System.Threading.Tasks;

namespace DifferentialEquationSolutionMethods
{
    public class ConvectionDiffusionReactionFiniteDifferenceScheme : INumericalScheme
    {
        public List<Node> FreeDOF { get; }

        public List<Node> BoundedDOF { get; }
        
        public IMathematicalProblem Problem { get; }

        public LinearSystem LinearSystem {get; set;}

        public ConvectionDiffusionReactionFiniteDifferenceScheme(List<Node> FreeDOF, List<Node> BoundedDOF, IMathematicalProblem problem)
        {
            this.FreeDOF = FreeDOF;
            this.BoundedDOF = BoundedDOF;
            this.Problem = problem;
            this.LinearSystem = CreateLinearSystem();
        }
        
        private LinearSystem CreateLinearSystem()
        {
            var matrix = CreateMatrix();
            var vector = CreateVector();
            return new LinearSystem(matrix, vector);
        }

        private double[,] CreateMatrix()
        {
            // TODO - Create Linear System
            switch (Problem.Equation.IsTransient)
            {
                case true:
                    return CreateTransientMatix();

                case false:
                    return CreateSteadyStateMatrix();
                
                default:
                    throw new System.NotImplementedException();
            }
        }

        private double[] CreateVector()
        {
            // TODO - Create Linear System
            switch (Problem.Equation.IsTransient)
            {
                case true:
                    return CreateTransientVector();

                case false:
                    return CreateSteadyStateVector();
                
                default:
                    throw new System.NotImplementedException();
            }
        }


        private IMatrix CreateSteadyStateMatrix()
        {
            return new Matrix();
        }

        private double[] CreateSteadyStateVector()
        {
            // TODO - Create Vector
            return new double[1];
        }

        private double[,] CreateTransientMatix()
        {
            // TODO - Create Matrix
            return new double[1, 1];        }

        private double[] CreateTransientVector()
        {
            // TODO - Create Vector
            return new double[1];
        }

        public double CoefficientCalculator(Node node, Node neighbour, int direction)
        {
            // TODO - Create Coefficient Calculator
            return 0;
        }
 

    }

}