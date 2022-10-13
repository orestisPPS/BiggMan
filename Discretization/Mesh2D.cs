namespace Discretization
{
    public class Mesh2D : IMesh2D
    {

        /// <summary>
        ///  Number of nodes in the x,ξ,r direction.
        /// </summary>
        /// <value></value>
        public int NNDirectionOne { get; }

        /// <summary>
        ///  Number of nodes in the y,η,Θ direction.
        /// </summary>
        /// <value></value>
        public int NNDirectionTwo { get; }

        /// <summary>
        ///  The total number of nodes in the mesh.
        /// </summary>
        public int TotalNodes => NNDirectionOne * NNDirectionTwo;

        /// <summary>
        /// Node distribution in 2d array.
        /// </summary>
        /// <value></value>
        public Node[,] NodesArray { get; set; }

        /// <summary>
        /// Key: Global ID of the node. Value: Node     
        /// </summary>
        public Dictionary<int, Node> NodesDictionary { get; set; }
        
        public Mesh2D(int NNOne, int NNTwo)
        {
            NNDirectionOne = NNOne;
            NNDirectionTwo = NNTwo;
            NodesArray = new Node[NNOne, NNTwo];
            NodesDictionary = new Dictionary<int, Node>();
        }

    }
}