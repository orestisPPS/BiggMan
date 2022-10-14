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
        private Dictionary<List<int>, Node> NodesArrayDictionary { get; set;} = new Dictionary<List<int>, Node>();

        /// <summary>
        /// The number of nodes in each direction of the mesh
        /// </summary>
        /// <typeparam name="Direction">Direction enum : axis 1,2,3 of an orthonormal basis</typeparam>
        /// <typeparam name="int">Number of nodes at Direction i</typeparam>
        /// <returns></returns>        
        public Dictionary<Direction, int> NumberOfNodesPerDirection { get; } = new Dictionary<Direction, int>();

        /// <summary>
        /// The total number of nodes in the mesh.
        /// </summary>
        /// <value></value>
        public int TotalNodes { get; private set; }
        /// <summary>
        /// Key: Global ID of the node
        /// Value: Node
        /// </summary>
        /// <value></value>
        public Dictionary<int, Node> NodesDictionary { get; set; } = new Dictionary<int, Node>();

        /// <summary>
        /// Dictionary with the domain boundaries of the mesh. Key: RelativePosition enum. Value: List of nodes at the boundary.
        /// </summary>
        /// <returns></returns>
        public Dictionary<RelativePosition, List<Node>> Boundaries { get; set; } = new Dictionary<RelativePosition, List<Node>>();

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
        /// <param name="nodesArrayDictionary"
        /// > Key : i,j,k etc positions of the node in the 1D, 2D, 3D array correspondingly. Value: Node</param>
        public Mesh(Dictionary<List<int>,Node> nodesArrayDictionary, Dictionary<int, Node> nodesDictionary,
                    Dictionary<Direction, int> numberOfNodesPerDirection)
        {
            this.NumberOfNodesPerDirection = numberOfNodesPerDirection;
            InputTestPassed = CheckUnequilityInInputListsLength(nodesArrayDictionary);
            Initialize(InputTestPassed, nodesArrayDictionary, nodesDictionary, numberOfNodesPerDirection);
        }

        private void Initialize(bool inputTestPassed, Dictionary<List<int>,Node> nodesArrayDictionary,
                                Dictionary<int, Node> nodesDictionary, Dictionary<Direction, int> numberOfNodesPerDirection)
        {
            switch (inputTestPassed)
            {
                case true:
                    this.NodesArrayDictionary = nodesArrayDictionary;
                    this.NodesDictionary = nodesDictionary;
                    this.MeshType = DefineMeshType();
                    this.TotalNodes = NodesDictionary.Count;
                    CreateDomainBoundaries();

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

        private void CreateDomainBoundaries()
        {
            var nnx = NumberOfNodesPerDirection[Direction.One];
            var nny = NumberOfNodesPerDirection[Direction.Two];
            var nnz = NumberOfNodesPerDirection[Direction.Three];
            switch (MeshType)
            {
                case MeshType.OneDimensional:
                    Create1DBoundaries();
                    break;
                case MeshType.TwoDimensional:
                    Create2DBoundaries();
                    break;
                case MeshType.ThreeDimensional:
                    Create3DBoundaries();
                    break;
                default:
                    break;
            }
        }
        private void Create1DBoundaries()
        {
            Boundaries.Add(RelativePosition.Left, new List<Node>() { NodesDictionary[0] });
            Boundaries.Add(RelativePosition.Right, new List<Node>() { NodesDictionary[TotalNodes - 1] });
        }

        private void Create2DBoundaries()
        {
            var nnx = NumberOfNodesPerDirection[Direction.One];
            var nny = NumberOfNodesPerDirection[Direction.Two];

            for (int i = 0; i < nnx; i++)
            {
                Boundaries[RelativePosition.Bottom].Add(Node(i, 0));
                Boundaries[RelativePosition.Top].Add(Node(i, nny - 1));
            }

            for (int j = 0; j < nny; j++)
            {
                Boundaries[RelativePosition.Left].Add(Node(0, j));
                Boundaries[RelativePosition.Right].Add(Node(nnx - 1, j));
            }

        }

        private void Create3DBoundaries()
        {
            var nnx = NumberOfNodesPerDirection[Direction.One];
            var nny = NumberOfNodesPerDirection[Direction.Two];
            var nnz = NumberOfNodesPerDirection[Direction.Three];

            for (int i = 0; i < nnx; i++)
            {
                for (int j = 0; j < nny; j++)
                {
                    Boundaries[RelativePosition.Bottom].Add(Node(i, j, 0));
                    Boundaries[RelativePosition.Top].Add(Node(i, j, nnz - 1));
                }
            }

            for (int i = 0; i < nnx; i++)
            {
                for (int k = 0; k < nnz; k++)
                {
                    Boundaries[RelativePosition.Left].Add(Node(i, 0, k));
                    Boundaries[RelativePosition.Right].Add(Node(i, nny - 1, k));
                }
            }

            for (int j = 0; j < nny; j++)
            {
                for (int k = 0; k < nnz; k++)
                {
                    Boundaries[RelativePosition.Front].Add(Node(0, j, k));
                    Boundaries[RelativePosition.Back].Add(Node(nnx - 1, j, k));
                }
            }
        }
        public Node Node (int i)
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

        public Node Node (int i, int j)
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
                        throw new Exception("WANK! Searching a 2D node in a 1D domain.");
                    case MeshType.ThreeDimensional:
                        throw new Exception("WANK! Searching a 2D node in a 3D domain."); 
                    default:
                        throw new Exception("Node does not exist.");
                }
            }

        }

        public Node Node (int i, int j, int k)
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
                        throw new Exception("WANK! Searching a 3D node in an 1D domain.");
                    case MeshType.TwoDimensional:
                        throw new Exception("WANK! Searching a 3D node in a 2D domain");
                    default:
                        throw new Exception("Node does not exist.");
                }
            }

        }

    }
}