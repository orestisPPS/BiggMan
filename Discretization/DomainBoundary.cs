using Discretization;
namespace Discretization
{
    public class DomainBoundary
    {
        public int Id { get; }
       
        public List<Node> Nodes { get; } = new List<Node>();
       
        public int NumberOfNodes => Nodes.Count;

        public DomainBoundary(int id, List<Node> nodes)
        {
            Id = id;
            Nodes = nodes;
        }
    }   
}