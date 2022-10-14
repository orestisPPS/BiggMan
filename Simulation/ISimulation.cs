using MathematicalProblems;
using Discretization;
using Constitutive;
using DifferentialEquations;
using BoundaryConditions;
using MeshGeneration;
using DifferentialEquationSolutionMethods;
using utility;

namespace Simulations
{
    public enum SimulationType
    {
        SteadyState,
        Transient
    }
    public interface ISimulation2D
    {
        int Id { get; }

        SimulationType Type { get; }
        
        Mesh Mesh { get; }

        DifferentialEquationsSolutionMethodType SolutionMethodType { get; }

        IDifferentialEquationSolutionMethod SolutionMethod { get; }

        List<Node> FreeDegreesOfFreedom { get; }
        
        ComputationalDomainType ComputationalDomainType { get; }

    }
}