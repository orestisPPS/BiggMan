using Discretization;
namespace Meshing
{
    public class DomainBoundary
    {
        public int Id { get; }
       
        public List<Node> Nodes = new List<Node>();
       
        public int NumberOfNodes => Nodes.Count;

        public DomainBoundary(int id)
        {
            this.Id = id;
        }

        public void AssignNodesToBoundary(Node[,] nodes)
        {
            switch (Id)
            {
                case 0:
                    for (int i = 0; i < nodes.GetLength(0); i++)
                    {
                        Nodes.Add(nodes[i, 0]);
                    }
                    break;
                case 1:
                    for (int i = 0; i < nodes.GetLength(0); i++)
                    {
                        Nodes.Add(nodes[i, nodes.GetLength(1) - 1]);
                    }
                    break;
                case 2:
                    for (int i = 0; i < nodes.GetLength(1); i++)
                    {
                        Nodes.Add(nodes[0, i]);
                    }
                    break;
                case 3:
                    for (int i = 0; i < nodes.GetLength(1); i++)
                    {
                        Nodes.Add(nodes[nodes.GetLength(0) - 1, i]);
                    }
                    break;
            }
        }

    }   
}