namespace Discretization
{
    public interface IMesh
    {
        /// <summary>
        /// Number of nodes in the x,ξ,r direction.
        /// </summary>
        /// <value></value>
        int NNDirectionOne { get; }

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
        Dictionary<int, Node> NodesDictionary { get; set; }

    }
}