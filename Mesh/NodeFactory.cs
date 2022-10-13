using utility;
using Discretization;
using Constitutive;
namespace MeshGeneration
{
    public class NodeFactory
    {

        /// <summary>
        /// Input: A 2 dimensional array which maps the nodes in the computational
        /// domain, in the order they they appear in the domain (from left to right)
        /// </summary>
        /// <value></value>
        public Node[,] NodesArray {get; set;}
        
        public Dictionary<int, DomainBoundary> DomainBoundaries {get; set;} = new Dictionary<int, DomainBoundary>();

        private int NumberOfNodesX {get;}

        private int NumberOfNodesY {get;}

        public Dictionary<int, Node> NodesDictionary {get; set;} = new Dictionary<int, Node>();

        public NodeFactory(int numberOfNodesX, int numberOfNodesY)
        {
            this.NumberOfNodesX = numberOfNodesX;
            this.NumberOfNodesY = numberOfNodesY;
            NodesArray = new Node[NumberOfNodesY, NumberOfNodesX];
            CreateDomainBoundaries();
            CreateDomainInternal();
            AssignGlobalIds();
        }

        private void CreateDomainBoundaries()
        {
            var boundaryCounter = 0;
            
            //Bottom Boundary
            var bottomBoundary = new DomainBoundary(0);
            for (int i = 0; i < NumberOfNodesX; i++)
            {
                NodesArray[i, 0] = InitializeBoundaryNode(positionInBoundary : i, nodalBoundaryId: boundaryCounter);
                bottomBoundary.Nodes.Add(NodesArray[0, i]);
                boundaryCounter++;
            }
            DomainBoundaries.Add(0, bottomBoundary);
            
            //Right Boundary
            var rightBoundary = new DomainBoundary(1);
            for (int i = 1; i < NumberOfNodesY; i++)
            {
                NodesArray[NumberOfNodesX - 1, i] = InitializeBoundaryNode(positionInBoundary : i, nodalBoundaryId: boundaryCounter);
                rightBoundary.Nodes.Add(NodesArray[NumberOfNodesX - 1, i]);
                boundaryCounter++;
            }
            DomainBoundaries.Add(1, rightBoundary);
            
            
            //Top Boundary
            var topBoundary = new DomainBoundary(2);
            for (int i = 1; i < NumberOfNodesX; i++)
            {
                NodesArray[NumberOfNodesX - 1 - i, NumberOfNodesY - 1] = InitializeBoundaryNode(positionInBoundary : i, nodalBoundaryId: boundaryCounter);
                topBoundary.Nodes.Add(NodesArray[NumberOfNodesX - 1 - i, NumberOfNodesY - 1]);
                boundaryCounter++;
            }
            DomainBoundaries.Add(2, topBoundary);
            
            //Left Boundary
            var leftBoundary = new DomainBoundary(3);
            for (int i = 1; i < NumberOfNodesY - 1; i++)
            {
                NodesArray[0, NumberOfNodesY - 1 - i] = InitializeBoundaryNode(positionInBoundary : i, nodalBoundaryId: boundaryCounter);
                leftBoundary.Nodes.Add(NodesArray[0, NumberOfNodesY - 1 - i]);
                boundaryCounter++;
            }
            DomainBoundaries.Add(3, leftBoundary);
        }

        private Node InitializeInternalNode(int positionInternal)
        {
            var node = new Node();
            node.Id.Internal = positionInternal;
            return node;
        }

        private void CreateDomainInternal()
        {
            //Internal 
            var internalCounter = 0;
            for (int row = 1; row < NumberOfNodesY - 1; row++)
            {
                for (int column = 1; column < NumberOfNodesX - 1; column++)
                {
                    NodesArray[row, column] = InitializeInternalNode(positionInternal : internalCounter);
                    internalCounter++;
                }
            }
        }
        
        private Node InitializeBoundaryNode(int positionInBoundary, int nodalBoundaryId)
        {
            var node = new Node();
            node.Id.Boundary = nodalBoundaryId;
            return node;
        }

        private void  AssignGlobalIds()
        {
            var k = 0;
            for (int row = 0; row < NumberOfNodesY; row++)
            {
                for (int column = 0; column < NumberOfNodesX; column++)
                {
                    NodesArray[column, row].Id.Global = k;
                    NodesDictionary.Add(k, NodesArray[column, row]);
                    k++;
                }
            }
        }
    }
}