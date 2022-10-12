using DifferentialEquations;
using Meshing;
using Discretization;
using Constitutive;
using BoundaryConditions;
using DifferentialEquationSolutionMethods;
using MathematicalProblems;
using Simulations;
using utility;
internal class Program
{
    private static void Main(string[] args)
    {
        MeshSpecs2D meshSpecs = new MeshSpecs2D(5, 5, 0,1, 0.1, 0, 0);
        MeshGenerator2D meshGenerator = new MeshGenerator2D(meshSpecs);
    }
}