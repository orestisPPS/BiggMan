using Discretization;
using MathematicalProblems;
using prizaLinearAlgebra;

namespace DifferentialEquationSolutionMethods
{
    public abstract class NumericalScheme : INumericalScheme 
    {        
        public IMathematicalProblem Problem { get; }
        
        public List<Node> FreeDOF { get; }

        public List<Node> BoundedDOF { get; }

        public LinearSystem LinearSystem { get; }

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