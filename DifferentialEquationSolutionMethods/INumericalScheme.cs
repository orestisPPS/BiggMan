using Discretization;
using MathematicalProblems;
using prizaLinearAlgebra;

namespace DifferentialEquationSolutionMethods
{
    public interface INumericalScheme
    {        
        IMathematicalProblem Problem { get; }
        
        List<Node> FreeDOF { get; }

        List<Node> BoundedDOF { get; }

        LinearSystem LinearSystem { get; }
    }
}