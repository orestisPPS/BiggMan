using Discretization;
using MathematicalProblems;
using prizaLinearAlgebra;

namespace DifferentialEquationSolutionMethods
{
    public interface INumericalScheme
    {
        
        public IMathematicalProblem Problem { get; }
        
        public LinearSystem LinearSystem { get; }
    }
}