using BoundaryConditions;
using MathematicalProblems;
using DifferentialEquations;
using Discretization;
using MeshGeneration;
using Constitutive;
using DifferentialEquationSolutionMethods;
using Simulations;
using utility;
using System.Diagnostics;

public class SteadyStateSimulation : ISteadyStateSimulation2D
{
    public int Id { get; }
    public SimulationType Type => SimulationType.SteadyState;
    
    public Mesh Mesh { get; }

    public SteadyStateMathematicalProblem MathProblem { get; }

    public DifferentialEquationsSolutionMethodType SolutionMethodType { get; }

    public IDifferentialEquationSolutionMethod SolutionMethod { get; internal set; }

    public ComputationalDomainType ComputationalDomainType { get; }

    public List<Node> FreeDegreesOfFreedom { get; }

    public SteadyStateSimulation(int id, Mesh mesh, SteadyStateMathematicalProblem mathProblem,
                                 DifferentialEquationsSolutionMethodType solutionMethodType, ComputationalDomainType computationalDomainType)
    {
        Console.WriteLine($"Initiating steady state simulation {id} ...");
        var sw = new Stopwatch();
        sw.Start();
        this.Id = id;
        this.Mesh = mesh;
        this.MathProblem = mathProblem;
        this.SolutionMethodType = solutionMethodType;
        this.ComputationalDomainType = computationalDomainType;
        AssignDegreesOfFreedomToNodes();
        AssignBoundaryValuesToBoundaryNodes();
        SolutionMethod = AssignSolutionMethod();
        sw.Stop();
        Console.WriteLine($"Simulation {Id} initiated in {sw.ElapsedMilliseconds} ms");
    }

    private void AssignDegreesOfFreedomToNodes()
    {
        Console.WriteLine($"Simulation {Id}: Assigning degrees of freedom to nodes...");
        foreach (var node in Mesh.NodesDictionary.Values)
        {
            node.DegreesOfFreedom.Add(MathProblem.DegreeOfFreedom.Type, MathProblem.DegreeOfFreedom);
        }
    }

    private void AssignBoundaryValuesToBoundaryNodes()
    {
        Console.WriteLine($"Simulation {Id}: Assigning boundary values to bounded nodes...");
        foreach (var boundary in MathProblem.BoundaryConditions.Keys)
        {
            var iBoundary = new DomainBoundary(boundary);
            iBoundary.AssignNodesToBoundary(Mesh.NodesArray);

            if (iBoundary.NumberOfNodes != MathProblem.BoundaryConditions[boundary].Count)
            {
                throw new System.Exception("Boundary conditions and boundary nodes of boundary : " + boundary + " do not match");
            }

            for (int i = 0; i < iBoundary.NumberOfNodes; i++)
            {
                var iNode = iBoundary.Nodes[i];
                var iBC = MathProblem.BoundaryConditions[boundary][i];
                switch (ComputationalDomainType)
                {
                    case (ComputationalDomainType.Parametric):
                        if (iBC.Type == BoundaryConditionType.Dirichlet)
                        {
                            iNode.DegreesOfFreedom[MathProblem.DegreeOfFreedom.Type].Value = 
                            iBC.Function2D(iNode.Coordinates[CoordinateType.ParametricKsi].Value, iNode.Coordinates[CoordinateType.ParametricIta].Value);
                        }
                        break;

                    case (ComputationalDomainType.Natural):
                        if (iBC.Type == BoundaryConditionType.Dirichlet)
                        {
                            iNode.DegreesOfFreedom[MathProblem.DegreeOfFreedom.Type].Value = 
                            iBC.Function2D(iNode.Coordinates[CoordinateType.NaturalX].Value, iNode.Coordinates[CoordinateType.NaturalY].Value);
                        }
                        break;
                }
            }
        }
    }

    private void AssignSolutionMethod()
    {
        Console.WriteLine("Assigning solution method...");
        switch (SolutionMethodType)
        {
            case (DifferentialEquationsSolutionMethodType.FiniteDifferences):
                SolutionMethod = new FiniteDifferenceMethod(Mesh, MathProblem);
                break;
        }
    }

    public void InitiateAnalysis()
    {
        throw new System.NotImplementedException();
    } 
}