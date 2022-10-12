using DifferentialEquations;
using Discretization;
using MathematicalProblems;
namespace DifferentialEquationSolutionMethods
{
    public enum DifferentialEquationsSolutionMethodType
    {
        FiniteDifferences,
        FiniteElements, 
        IsogeometricAnalysis
    }

    public interface IDifferentialEquationSolutionMethod
    {
        DifferentialEquationsSolutionMethodType Type { get; }

        IMathematicalProblem MathematicalProblem { get; }

        INumericalScheme NumericalScheme { get; }

        List<Node> FreeDOF { get; }

        List<Node> BoundedDOF { get; }
    }
}