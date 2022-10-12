using Equations;
using DifferentialEquations;
using Constitutive; 
using BoundaryConditions;
namespace MathematicalProblems
{
    public class SteadyStateMathematicalProblem : IMathematicalProblem
    {
        public MathematicalProblemType Type => MathematicalProblemType.BoundaryValueProblem;
        
        public IDifferentialEquation Equation { get; }

        public Dictionary<int, List<IBoundaryCondition>> BoundaryConditions { get; }

        public DegreeOfFreedom DegreeOfFreedom { get; }
        
        public bool IsTransient => false;

        public SteadyStateMathematicalProblem(IDifferentialEquation equation,
                                              Dictionary<int, List<IBoundaryCondition>> boundaryConditions,
                                              DegreeOfFreedom degreeOfFreedom)
        {
            this.Equation = equation;
            this.BoundaryConditions = boundaryConditions;
            this.DegreeOfFreedom = degreeOfFreedom;
        }


    }
}