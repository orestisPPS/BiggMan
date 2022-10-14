using Discretization;
using DifferentialEquations;
using DifferentialEquationSolutionMethods;
using Constitutive;
using MathematicalProblems;
using BoundaryConditions;
using Simulations;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MeshGeneration
{
    public class MeshGenerator2D
    {
        public MeshSpecs2D MeshSpecs { get; }
        private Mesh2DPreProcessor PreProcessor;
        private SteadyStateMathematicalProblem MathematicalProblemForX;
        private SteadyStateMathematicalProblem MathematicalProblemForY;
        private SteadyStateSimulation SimulationForX;
        private SteadyStateSimulation SimulationForY;
        private Parallelogram Domain;
    
        public MeshGenerator2D(MeshSpecs2D specs)
        {
            this.MeshSpecs = specs;
            PreProcessor = new Mesh2DPreProcessor(specs);
            Domain = new Parallelogram(specs);
            MathematicalProblemForX = CreateMathematicalProblemForX();
            MathematicalProblemForY = CreateMathematicalProblemForY();
            SimulationForX = CreateSimulationForX();
            SimulationForY = CreateSimulationForY();
        }

        private SteadyStateMathematicalProblem CreateMathematicalProblemForX()
        {
            Console.WriteLine("Initiating mathematical problems ...");
            var equationProperties = PreProcessor.DomainProperties;
            var equationForX = new ConvectionDiffusionReactionEquation(equationProperties);
            var xDOF = new X();
            return new SteadyStateMathematicalProblem(equationForX, Domain.DomainDirichletX, xDOF);
        }

        private SteadyStateMathematicalProblem CreateMathematicalProblemForY()
        {
            var equationProperties = PreProcessor.DomainProperties;
            var equationForY =   new ConvectionDiffusionReactionEquation(equationProperties);
            var yDOF = new Y();
            return new SteadyStateMathematicalProblem(equationForY, Domain.DomainDirichletY, yDOF);
        }

        private SteadyStateSimulation CreateSimulationForX()
        {
            var mesh = PreProcessor.MeshBeta;
            var solutionMethodType = DifferentialEquationsSolutionMethodType.FiniteDifferences;
            var computationalDomainType = ComputationalDomainType.Parametric;
            SimulationForX = new SteadyStateSimulation(0, mesh, MathematicalProblemForX, solutionMethodType, computationalDomainType);
            return SimulationForX;
        }

        private SteadyStateSimulation CreateSimulationForY()
        {
            var mesh = PreProcessor.MeshBeta;
            var solutionMethodType = DifferentialEquationsSolutionMethodType.FiniteDifferences;
            var computationalDomainType = ComputationalDomainType.Parametric;
            SimulationForY = new SteadyStateSimulation(1, mesh, MathematicalProblemForY, solutionMethodType, computationalDomainType);
            return SimulationForY;
        }

        // private void ParallelSimulationInitiation()
        // {
        //     Parallel.Invoke(
        //         () => SimulationForX = CreateSimulationForX(),
        //         () => SimulationForY = CreateSimulationForY()
        //     );
        // }

    }
}