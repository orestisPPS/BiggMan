using Equations;
using DifferentialEquations;
using BoundaryConditions;
using Discretization;
using Constitutive;
namespace MathematicalProblems
{
    public enum MathematicalProblemType
    {
        BoundaryValueProblem,
        InitialValueProblem,
        BoundaryInitialValueProblem,
    }
    public interface  IMathematicalProblem
    {
        public MathematicalProblemType Type { get; }

        public IDifferentialEquation Equation { get; }

        public Dictionary<int, List<IBoundaryCondition>> BoundaryConditions { get; }

        public DegreeOfFreedom DegreeOfFreedom { get; }

        public bool IsTransient { get; }
    }

}
