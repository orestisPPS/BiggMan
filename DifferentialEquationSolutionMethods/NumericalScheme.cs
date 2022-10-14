using Discretization;
using MathematicalProblems;
using prizaLinearAlgebra;

namespace DifferentialEquationSolutionMethods
{
    public abstract class NumericalScheme : INumericalScheme 
    {        
        public IMathematicalProblem Problem { get; }
        
        public IMesh Mesh { get; }

        public List<Node> FreeDOF { get; } = new List<Node>();

        public List<Node> BoundedDOF { get; } = new List<Node>();

        public LinearSystem LinearSystem { get; }

        public void FindFreeDOF()
        {
            var nn1 = Mesh.NumberOfNodesPerDirection[Direction.One];
            var nn2 = Mesh.NumberOfNodesPerDirection[Direction.Two];
            //Add internal nodes
            for (int j = 1; j < nn2 - 1; j++)
            {
                for (int i = 1; i < nn1 - 1; i++)
                {
                    FreeDOF.Add(Mesh.Node(i, j));
                }
            }
        }

        private void FindBoundedDOF()
        {
            foreach (var boundary in Problem.BoundaryConditions.k)
        }

    }
}