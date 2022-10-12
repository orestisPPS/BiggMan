using Equations;
using DifferentialEquations;
using Constitutive; 
using BoundaryConditions;
using InitialConditions;
namespace MathematicalProblems
{
    public class TransientMathematicalProblem : IMathematicalProblem
    {
        public MathematicalProblemType Type => MathematicalProblemType.BoundaryValueProblem;
        
        public IDifferentialEquation Equation { get; }

        public Dictionary<int, List<IBoundaryCondition>> BoundaryConditions { get; }

        public List<Dictionary<string, InitialCondition>> InitialConditions { get; }
        
        public DegreeOfFreedom DegreeOfFreedom { get; }

        public bool IsTransient => true;

        public TransientMathematicalProblem(IDifferentialEquation equation,
                                              Dictionary<int, List<IBoundaryCondition>> boundaryConditions,
                                              List<Dictionary<string, InitialCondition>> initialConditions,
                                              DegreeOfFreedom degreeOfFreedom)
        {
            this.Equation = equation;
            this.BoundaryConditions = boundaryConditions;
            this.InitialConditions = initialConditions;
            this.DegreeOfFreedom = degreeOfFreedom;
        }
    }
}