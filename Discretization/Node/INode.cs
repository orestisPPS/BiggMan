using Constitutive;
namespace Discretization
{
    public interface INode  
    {
        /// <summary>
        /// A class containg the global, internal and boundary Ids of the node
        /// If a node is internal BoundaryId = -1, or boundary InternalId = -1 
        /// </summary>
        /// <value></value>
        public NodeId Id {get; set;}

        /// <summary>
        /// A dictionary with coordinate objects as key and coordinate value as value.
        /// The coordinates can be on the natural, template or computational domain.
        /// </summary>
        /// <value></value>
        public Dictionary <CoordinateType, ICoordinate> Coordinates {get; set;} 
        

        /// <summary>
        /// A dictionary containing a DOF type object as a key (temperature, displacement) 
        /// </summary>
        /// <value></value>
        public Dictionary<DegreeOfFreedomType, IDegreeOfFreedom> DegreesOfFreedom {get; set;}

    }
}