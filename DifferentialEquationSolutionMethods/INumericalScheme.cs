using Discretization;
using MathematicalProblems;
using prizaLinearAlgebra;

namespace DifferentialEquationSolutionMethods
{
    public interface INumericalScheme
    {
        
        public IMathematicalProblem Problem { get; }
        
        public List<Node> FreeDOF { get; }

        public List<Node> BoundedDOF { get; }

        public LinearSystem LinearSystem { get; }
    }
}