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

        Dictionary<RelativePosition, List<Node>> Boundaries { get; }
        
        /// <summary>
        /// Returns the i th node of a 1D mesh
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        Node Node (int i);

        /// <summary>
        ///  Returns the i,j th node of a 2D mesh
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        Node Node(int i, int j);
        
        /// <summary>
        ///
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        Node Node(int i, int j, int k);

    }
}