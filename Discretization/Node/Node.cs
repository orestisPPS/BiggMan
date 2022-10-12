using Constitutive;
namespace Discretization 
{
    public class Node : INode
    {   
        public NodeId Id {get; set;} = new NodeId();

        public Dictionary <CoordinateType, ICoordinate> Coordinates {get; set;} = new Dictionary <CoordinateType, ICoordinate>();

        public Dictionary<DegreeOfFreedomType, IDegreeOfFreedom> DegreesOfFreedom {get; set;} = new Dictionary<DegreeOfFreedomType, IDegreeOfFreedom>();
        
        public Node()
        {
        }
    }
}