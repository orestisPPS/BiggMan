using DifferentialEquations;
using MathematicalProblems;
using Discretization;
using BoundaryConditions;
using System.Threading;
using System.Threading.Tasks;

namespace DifferentialEquationSolutionMethods
{
    public class FiniteDifferenceMethod : IDifferentialEquationSolutionMethod
    {
        public IMesh Mesh { get; }

        public IMathematicalProblem MathematicalProblem { get; }
        
        public DifferentialEquationsSolutionMethodType Type => DifferentialEquationsSolutionMethodType.FiniteDifferences;
 
        public INumericalScheme NumericalScheme => SchemeSelector();



        
        public FiniteDifferenceMethod(IMesh mesh,  IMathematicalProblem mathematicalProblem)
        {
            this.Mesh = mesh;
            this.MathematicalProblem = mathematicalProblem;
        }

        private INumericalScheme SchemeSelector()
        {
            switch (MathematicalProblem.Equation.DifferentialEquationType)
            {
                case DifferentialEquationType.ConvectionDiffusionReaction:
                    return new ConvectionDiffusionReactionFiniteDifferenceScheme(Nodes, MathematicalProblem);
                default:
                    throw new System.NotImplementedException();
            }
        }



        }
    }


}