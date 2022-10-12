using Discretization;
using BoundaryConditions;
using DifferentialEquations;
using System.Diagnostics;
using utility;

namespace Meshing
{
    public class MeshPreProcessor
    {
        public MeshSpecs2D Specs { get; }

        public Node[,] Nodes {get; set;}

        //public Dictionary<Tuple<int, int>, Node> NodesDictionary { get; set; } = new Dictionary<Tuple<int, int>, Node>();
        public Dictionary<int, Node> NodesDictionary { get; set; } = new Dictionary<int, Node>();

        public Dictionary<Node, NodeMetrics> Metrics {get; internal set;} = new Dictionary<Node, NodeMetrics>();

        public DifferentialEquationProperties DomainProperties {get; internal set;}
        
        public NodeFactory NodeFactory {get; internal set;}

        public MeshPreProcessor(MeshSpecs2D specs)
        {
            this.Specs = specs;
            Nodes = InitiateNodes();
            AssingCoordinatesToNodes();
            //PrintNodes();
            CreateNodeIdDictionary();
            CalculateMeshMetrix();
            DomainProperties = AssignMeshGenerationProperties();
            ClearMemory();
        }
        ///This method prints the parametric coordinates of the nodes as well as the node ids
        public void PrintNodes()
        {
            for (int j = 0; j < Nodes.GetLength(1); j++)
            {
                for (int i = 0; i < Nodes.GetLength(0); i++)
                {
                    Console.WriteLine($"G id: {Nodes[i, j].Id.Global}, B id: {Nodes[i, j].Id.Boundary}, I id: {Nodes[i, j].Id.Internal}, x: {Nodes[i, j].Coordinates[CoordinateType.ParametricKsi].Value}, y: {Nodes[i, j].Coordinates[CoordinateType.ParametricIta].Value}");
                }
            }
        }
        
        private Node[,] InitiateNodes()
        {
            var sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Initiating nodes...");
            NodeFactory = new NodeFactory(numberOfNodesX : Specs.NNDirectionOne, numberOfNodesY : Specs.NNDirectionTwo);
            sw.Stop();
            Console.WriteLine($"Nodes initiated in {sw.ElapsedMilliseconds} ms");
            return NodeFactory.Nodes;
        }

        private void AssingCoordinatesToNodes()
        {
            Console.WriteLine("Initiating node coodinates in the physical domain...");
            Console.WriteLine("Initiating and calculating node coodinates in the parametric and template domains...");
            var sw = new Stopwatch();
            sw.Start();
            for (int j = 0; j < Specs.NNDirectionTwo; j++)
            {
                for (int i = 0; i < Specs.NNDirectionOne; i++)
                {
                    AssignNaturalMeshCoordinatesToNodes(i, j);
                    AssgignComputationalMeshCoordinatesToNodes(i, j);
                    AssignTemplateMeshCoordinatesToNodes(i, j);
                }
            }
            sw.Stop();
            Console.WriteLine($"Coordinates assigned to nodes in {sw.ElapsedMilliseconds} ms");
        }

        private void AssignNaturalMeshCoordinatesToNodes(int i, int j)
        {
            Nodes[i, j].Coordinates.Add(CoordinateType.NaturalX, new NaturalX());
            Nodes[i, j].Coordinates.Add(CoordinateType.NaturalY, new NaturalY());
        }

        private void AssgignComputationalMeshCoordinatesToNodes(int i, int j)
        {

            Nodes[i, j].Coordinates.Add(CoordinateType.ParametricKsi, new ParametricKsi(i));
            Nodes[i, j].Coordinates.Add(CoordinateType.ParametricIta, new ParametricIta(j));
        }

        private void AssignTemplateMeshCoordinatesToNodes(int i, int j)
        {
            var templateCoordinates = Transform(new double[] {i, j}); 
            Nodes[i, j].Coordinates.Add(CoordinateType.TemplateX, new TemplateX(templateCoordinates[0]));
            Nodes[i, j].Coordinates.Add(CoordinateType.TemplateY, new TemplateY(templateCoordinates[1]));
        }

        private double[] Transform(double[] initialCoord)
        {
            var transformedCoord = TransformationTensors.Rotate (initialCoord, Specs.TemplateRotAngle);
            transformedCoord = TransformationTensors.Shear(transformedCoord, Specs.TemplateShearX, Specs.TemplateShearY);
            transformedCoord = TransformationTensors.Scale(transformedCoord, Specs.TemplateHx, Specs.TemplateHy);
            return transformedCoord;    
        }

        private void CalculateMeshMetrix()
        {   Console.WriteLine("Calculating mesh metrics...");
            var sw = new Stopwatch();
            sw.Start();
            var MetricsCalculator = new MetricsCalculator(NodesDictionary, Specs.NNDirectionOne, Specs.NNDirectionTwo);
            sw.Stop();
            Console.WriteLine($"Mesh metrics calculated in {sw.ElapsedMilliseconds} ms");
            Metrics = MetricsCalculator.MeshMetrics;
        }

        private void ClearMemory()
        {
            foreach (var node in Nodes)
            {
                //node.Coordinates.Remove(CoordinateType.ParametricKsi);
                //node.Coordinates.Remove(CoordinateType.ParametricIta);
                node.Coordinates.Remove(CoordinateType.TemplateX);
                node.Coordinates.Remove(CoordinateType.TemplateY);
            }
        }
        
        private LocallyAnisotropicConvectionDiffusionReactionEquationProperties AssignMeshGenerationProperties()
        {
            var diffusionCoefficients = new Dictionary<Node, double[,]>();
            var convectionCoefficients = new Dictionary<Node, double[]>();
            var dependentReactionCoefficients = new Dictionary<Node, double[]>();
            var independentReactionCoefficients = new Dictionary<Node, double[]>();
            foreach (var node in Nodes)
            {
                diffusionCoefficients.Add(node, Metrics[node].contravariantTensor);
                convectionCoefficients.Add(node, new double[] {0, 0});
                dependentReactionCoefficients.Add(node, new double[] {0d, 0d} );
                independentReactionCoefficients.Add(node, new double[] {0d, 0d} );
            }
            return new LocallyAnisotropicConvectionDiffusionReactionEquationProperties(diffusionCoefficients, convectionCoefficients,
                                                                                  dependentReactionCoefficients, independentReactionCoefficients);
        }

        private void CreateNodeIdDictionary()
        {
            foreach (var node in Nodes)
            {
                NodesDictionary.Add(node.Id.Global, node);
            }
        }

    }
}
