namespace Discretization
{
    public interface IMesh
    {
        /// <summary>
        ///  Key: Direction Enum. One, Two, Three, Time
        /// Value the number of nodes at direction_key
        /// </summary>
        /// <value></value>
        Dictionary<Direction, int> NumberOfNodesPerDirection { get; }

        /// <summary>
        /// The total number of nodes in the mesh.
        /// </summary>
        /// <value></value>
        int TotalNodes { get; }

        /// <summary>
        /// Key: Global ID of the node
        /// Value: Node
        /// </summary>
        /// <value></value>
        Dictionary<int, Node> NodesDictionary { get; }

    }
}