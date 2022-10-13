namespace Discretization
{
    public enum MeshType
    {
        OneDimensional,
        TwoDimensional,
        ThreeDimensional
    }
    public class Mesh : IMesh
    {

        /// <summary>
        ///  Key: Direction Enum. One, Two, Three, Time
        /// Value the number of nodes at direction_key
        /// </summary>
        /// <value></value>
        private Dictionary<List<int>, Node> NodesArrayDictionary { get; set;} = new Dictionary<List<int>, Node>();
        
        public Dictionary<Direction, int> NumberOfNodesPerDirection { get;} = new Dictionary<Direction, int>();

        /// <summary>
        /// The total number of nodes in the mesh.
        /// </summary>
        /// <value></value>
        public int TotalNodes => NodesDictionary.Count;

        /// <summary>
        /// Key: Global ID of the node
        /// Value: Node
        /// </summary>
        /// <value></value>
        public Dictionary<int, Node> NodesDictionary { get; internal set; } = new Dictionary<int, Node>();

        /// <summary>
        /// The type of the mesh according to the number of dimensions. (1D/2D/3D)
        /// </summary>
        /// <value></value>
        public MeshType MeshType { get; internal set;}

        /// <summary>
        /// It is determined after the mesh nodes are initiated. 
        /// True if all array coordinates lists in NodesArrayDictionary have the same lenght 
        /// </summary>
        /// <value></value>
        private bool InputTestPassed  = false;

        /// <summary>
        /// A computational mesh
        /// </summary>
        /// <param name="nodes"
        /// > Key : i,j,k etc positions of the node in the 1D, 2D, 3D array correspondingly. Value: Node</param>
        public Mesh(Dictionary<List<int>,Node> nodes, Dictionary<Direction, int> numberOfNodesPerDirection)
        {
            this.NumberOfNodesPerDirection = numberOfNodesPerDirection;
            InputTestPassed = CheckUnequilityInInputListsLength(nodes);
            Initialize(InputTestPassed, nodes);

        }
        private void Initialize( bool inputTestPassed, Dictionary<List<int>, Node> nodes)
        {
            switch (inputTestPassed)
            {
                case true:
                    this.NodesArrayDictionary = nodes;
                    this.NodesDictionary = CreateNodesDictionary();
                    this.MeshType = DefineMeshType();
                    break;
                case false:
                    MeshType = MeshType.TwoDimensional;
                    break;
            }
        }

        //Checks unequilities in  input lists lenghth
        private bool CheckUnequilityInInputListsLength(Dictionary<List<int>,Node> nodes)
        {
            var firstListLength = nodes.Keys.First().Count;
            foreach (var list in nodes.Keys)
            {
                if (list.Count != firstListLength)
                {
                   throw new Exception($"Unequility in input node :" + nodes[list].Id.Global + "lists length");
                }
            }
            return true;
        }

        private Dictionary<int, Node> CreateNodesDictionary()
        {
            foreach (var node in NodesArrayDictionary.Values)
            {
                NodesDictionary.Add(node.Id.Global, node);
            }
            return NodesDictionary;
        }

        //Defines the MeshType (1D/2D/3D) according to the number of the nodes in the input dictionary.
        private MeshType DefineMeshType()
        {
            var firstListLength = NodesArrayDictionary.Keys.First().Count;
            switch (firstListLength)
            {
                case 1:
                    return MeshType.OneDimensional;
                case 2:
                    return MeshType.TwoDimensional;
                case 3:
                    return MeshType.ThreeDimensional;
                default:
                    throw new Exception("The number of the nodes in the input dictionary is not 1, 2 or 3");
            }
        }

        public Node NodeI (int i)
        {
            if (NodesArrayDictionary.ContainsKey(new List<int>{i}))
            {
                return NodesArrayDictionary[new List<int>{i}];
            }
            else
            {
                switch (MeshType)
                {
                    case MeshType.TwoDimensional:
                        throw new Exception("The node searched belongs to 1D domain. This mesh is 2D.");
                    case MeshType.ThreeDimensional:
                        throw new Exception("The node searched belongs to 1D domain. This mesh is 3D.");
                    default:
                        throw new Exception("Node does not exist.");
                }
            }

        }

        public Node NodeIJ (int i, int j)
        {
            if (NodesArrayDictionary.ContainsKey(new List<int>{i,j}))
            {
                return NodesArrayDictionary[new List<int>{i,j}];
            }
            else
            {
                switch (MeshType)
                {
                    case MeshType.OneDimensional:
                        throw new Exception("The node searched belongs to 2D domain. This mesh is 1D.");
                    case MeshType.ThreeDimensional:
                        throw new Exception("The node searched belongs to 2D domain. This mesh is 3D.");
                    default:
                        throw new Exception("Node does not exist.");
                }
            }

        }

        public Node NodeIJK (int i, int j, int k)
        {
            if (NodesArrayDictionary.ContainsKey(new List<int>{i,j,k}))
            {
                return NodesArrayDictionary[new List<int>{i,j,k}];
            }
            else
            {
                switch (MeshType)
                {
                    case MeshType.OneDimensional:
                        throw new Exception("The node searched belongs to 3D domain. This mesh is 1D.");
                    case MeshType.TwoDimensional:
                        throw new Exception("The node searched belongs to 3D domain. This mesh is 2D.");
                    default:
                        throw new Exception("Node does not exist.");
                }
            }

        }

    }
}