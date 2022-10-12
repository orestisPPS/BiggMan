using MathematicalProblems;
using Discretization;
using Constitutive;
using DifferentialEquations;
using BoundaryConditions;
using DifferentialEquationSolutionMethods;
using utility;

namespace Simulations
{

    public interface ISteadyStateSimulation : ISimulation
    {
        public SteadyStateMathematicalProblem MathProblem { get; }
    }

}