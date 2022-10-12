using MathematicalProblems;
using Discretization;
using Constitutive;
using DifferentialEquations;
using BoundaryConditions;
using DifferentialEquationSolutionMethods;
using utility;

namespace Simulations
{
    public enum SimulationType
    {
        SteadyState,
        Transient
    }
    public interface ISimulation
    {
        int Id { get; }

        SimulationType Type { get; }
        
        Node[,] Nodes { get; }

        DifferentialEquationsSolutionMethodType SolutionMethodType { get; }

        IDifferentialEquationSolutionMethod SolutionMethod { get; }

        List<Node> FreeDegreesOfFreedom { get; }
        
        ComputationalDomainType ComputationalDomainType { get; }

    }
}