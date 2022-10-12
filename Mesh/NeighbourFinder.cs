using Discretization;
using utility;
namespace Meshing
{
    public class NeighbourFinder
    {
        /// <summary>
        /// (Input) The node whose neighbours are to be found.
        /// </summary>
        /// <value></value>
        public Node node {get;}

        /// <summary>
        /// (Input) A dictionary with mapping the mesh.
        /// </summary>
        /// <value></value>
        public Dictionary<int, Node> Nodes {get;}

        /// <summary>
        /// (Input) The dimensions of the mesh in direction x.
        /// </summary>
        /// <value></value>
        public int nnx {get;}

        /// <summary>
        /// (Input) The dimensions of the mesh in direction y.
        /// </summary>
        /// <value></value>
        public int nny {get;}

        /// <summary>
        /// An integer represetning the total number of nodes in the mesh.
        /// </summary>
        public int TotalNodes => Nodes.Count;

        /// <summary>
        /// A dictionary that maps the neighbouring nodes of each node.
        /// The key is a string with the geographical location ("SW", "N", etc) of each neighbour relatively to the node
        /// and the values are the nodes.
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <typeparam name="Node"></typeparam>
        /// <returns></returns>
        public Dictionary<string, Node> NodeHood {get; internal set;} = new Dictionary<string, Node>();

        public NeighbourFinder(Node node, Dictionary<int, Node> nodes, int nnx, int nny)
        {
            this.node = node;
            this.Nodes = nodes;
            this.nnx = nnx;
            this.nny = nny;

            if (MeshUtility.NodeExists(node.Id.Global, nodes) == false)
            {
                throw new Exception("The node does not exist in the mesh.");
            }
            else
            {
                FindNeighbours();
            }

        }

        private void FindNeighbours()
        {
            NorthWest();
            North();
            NorthEast();
            East();
            SouthEast();
            South();
            SouthWest();
            West();
        }

        public void NorthWest()
        {
            var exists = MeshUtility.NodeExists(node.Id.Global + nnx - 1, Nodes);
            if (exists == true)
            {
                NodeHood.Add("NW", Nodes[node.Id.Global + nnx - 1]);
            }
        }

        public void North()
        {
            var exists = MeshUtility.NodeExists(node.Id.Global + nnx, Nodes);
            if (exists == true)
            {
                NodeHood.Add("N", Nodes[node.Id.Global + nnx]);
            }
        }
        public void NorthEast()
        {
            var exists = MeshUtility.NodeExists(node.Id.Global + nnx + 1, Nodes);
            if (exists == true)
            {
                NodeHood.Add("NE", Nodes[node.Id.Global + nnx + 1]);
            }
        }
        public void West()
        {
            var exists = MeshUtility.NodeExists(node.Id.Global - 1, Nodes);
            if (exists == true)
            {
                NodeHood.Add("W", Nodes[node.Id.Global - 1]);
            }
        }

        public void East()
        {
            var exists = MeshUtility.NodeExists(node.Id.Global + 1, Nodes);
            if (exists == true)
            {
                NodeHood.Add("E", Nodes[node.Id.Global + 1]);
            }
        }

        public void SouthWest()
        {
            var exists = MeshUtility.NodeExists(node.Id.Global - nnx - 1, Nodes);
            if (exists == true)
            {
                NodeHood.Add("SW", Nodes[node.Id.Global - nnx - 1]);
            }
        }

        public void South()
        {
            var exists = MeshUtility.NodeExists(node.Id.Global - nnx, Nodes);
            if (exists == true)
            {
                NodeHood.Add("S", Nodes[node.Id.Global - nnx]);
            }
        }

        public void SouthEast()
        {
            var exists = MeshUtility.NodeExists(node.Id.Global - nnx + 1, Nodes);
            if (exists == true)
            {
                NodeHood.Add("SE", Nodes[node.Id.Global - nnx + 1]);
            }
        }   
    }
}