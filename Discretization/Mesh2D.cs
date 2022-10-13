namespace Discretization
{
    public class Mesh2D : IMesh
    {

        public Dictionary<Direction, int> NumberOfNodes { get; internal set; } = new Dictionary<Direction, int>();

        /// <summary>
        ///  The total number of nodes in the mesh.
        /// </summary>
        public int TotalNodes => CalculateTotalNodes();

        /// <summary>
        /// Node distribution in 2d array.
        /// </summary>
        /// <value></value>
        public IEnumerable<Node[,]> NodesArray { get; set; }

        /// <summary>
        /// Key: Global ID of the node. Value: Node     
        /// </summary>
        public Dictionary<int, Node> NodesDictionary { get; set; }
        
        public Mesh2D(int NNOne, int NNTwo)
        {
            NumberOfNodes.Add(IDirectionOne, NNOne);
            NNDirectionTwo = NNTwo;
            NodesArray = new Node[NNOne, NNTwo];
            NodesDictionary = new Dictionary<int, Node>();
        }
        
        private int CalculateTotalNodes()
        {
            var product = 0;
            foreach (var nodesPerDirection in NumberOfNodes.Values)
            {
                if (product == 0)
                {
                    product = nodesPerDirection;
                }
                else
                {
                    product *= nodesPerDirection;
                }
            }
            return product;
        }

    }
}