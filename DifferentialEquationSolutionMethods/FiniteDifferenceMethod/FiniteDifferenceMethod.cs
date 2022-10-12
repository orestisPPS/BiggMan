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
        public Node[,] Nodes { get; }

        //public override MathematicalProblem MathematicalProblem { get; }
        public IMathematicalProblem MathematicalProblem { get; }
        
        public DifferentialEquationsSolutionMethodType Type => DifferentialEquationsSolutionMethodType.FiniteDifferences;
 
        public INumericalScheme NumericalScheme => SchemeSelector();

        public List<Node> FreeDOF { get; internal set; }

        public List<Node> BoundedDOF { get; }

        
        public FiniteDifferenceMethod(Node[,] domainNodes,  IMathematicalProblem mathematicalProblem)
        {
            this.Nodes = domainNodes;
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

        private void FindFreeDOF()
        {
            FreeDOF = new List<Node>();

            //Add internal nodes
            for (int i = 1; i < Nodes.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < Nodes.GetLength(1) - 1; j++)
                {
                    FreeDOF.Add(Nodes[i, j]);
                }
            }

            foreach (var boundaryCondition in MathematicalProblem.BoundaryConditions.Values)
            {
                switch (boundaryCondition.)
                {
                    case BoundaryConditionType.Dirichlet:
                        foreach (var node in boundaryCondition.Nodes)
                        {
                            FreeDOF.Remove(node);
                        }
                        break;
                    case BoundaryConditionType.Neumann:
                        break;
                    default:
                        break;
                }
            }

        }
    }


}