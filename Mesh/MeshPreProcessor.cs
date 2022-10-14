using Discretization;
using BoundaryConditions;
using DifferentialEquations;
using System.Diagnostics;
using utility;

namespace MeshGeneration
{
    public class Mesh2DPreProcessor
    {
        public MeshSpecs2D Specs { get; }

        public Mesh MeshBeta { get; set; }

        public Dictionary<Node, NodeMetrics> Metrics {get; internal set;} = new Dictionary<Node, NodeMetrics>();

        public DifferentialEquationProperties DomainProperties {get; internal set;}
        
        private NodeFactory NodeFactory;
        public Mesh2DPreProcessor(MeshSpecs2D specs)
        {
            this.Specs = specs;
            InitiateMesh();
            AssingCoordinatesToNodes();
            CalculateMeshMetrix();
            DomainProperties = AssignMeshGenerationProperties();
            ClearMemory();
            //PrintNodes();
        }
        ///This method prints the parametric coordinates of the nodes as well as the node ids
        public void PrintNodes()
        {
            
            for (int j = 0; j < MeshBeta.NumberOfNodesPerDirection[Direction.Two]; j++)
            {
                for (int i = 0; i < MeshBeta.NumberOfNodesPerDirection[Direction.One]; i++)
                {
                    var node = MeshBeta.Node(i, j);
                    Console.WriteLine($"G id: {node.Id.Global}, B id: {node.Id.Boundary}, I id: {node.Id.Internal}, x: {node.Coordinates[CoordinateType.ParametricKsi].Value}, y: {node.Coordinates[CoordinateType.ParametricIta].Value}");
                }
            }
        }
        
        private void InitiateMesh()
        {
            var sw = new Stopwatch(); 
            sw.Start();
            Console.WriteLine("Initiating nodes...");

            NodeFactory = new NodeFactory(numberOfNodesX : Specs.NNDirectionOne, numberOfNodesY : Specs.NNDirectionTwo);
            var nodesArrayDictionary = NodeFactory.MeshConstructorArguments().Item1;
            var nodesIdDictionary = NodeFactory.MeshConstructorArguments().Item2;
            var nodesPerDirection = NodeFactory.MeshConstructorArguments().Item3;
            
            MeshBeta = new Mesh(nodesArrayDictionary, nodesIdDictionary, nodesPerDirection);

            sw.Stop();
            Console.WriteLine($"Nodes initiated in {sw.ElapsedMilliseconds} ms");
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
            var node = MeshBeta.Node(i, j);
            node.Coordinates.Add(CoordinateType.NaturalX, new NaturalX());
            node.Coordinates.Add(CoordinateType.NaturalY, new NaturalY());
        }

        private void AssgignComputationalMeshCoordinatesToNodes(int i, int j)
        {
            var node = MeshBeta.Node(i, j);
            node.Coordinates.Add(CoordinateType.ParametricKsi, new ParametricKsi(i));
            node.Coordinates.Add(CoordinateType.ParametricIta, new ParametricIta(j));
        }

        private void AssignTemplateMeshCoordinatesToNodes(int i, int j)
        {
            var templateCoordinates = Transform(new double[] {i, j}); 
            var node = MeshBeta.Node(i, j);
            node.Coordinates.Add(CoordinateType.TemplateX, new TemplateX(templateCoordinates[0]));
            node.Coordinates.Add(CoordinateType.TemplateY, new TemplateY(templateCoordinates[1]));
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
            var MetricsCalculator = new MetricsCalculator(MeshBeta.NodesDictionary, Specs.NNDirectionOne, Specs.NNDirectionTwo);
            sw.Stop();
            Console.WriteLine($"Mesh metrics calculated in {sw.ElapsedMilliseconds} ms");
            Metrics = MetricsCalculator.MeshMetrics;
        }

        private void ClearMemory()
        {
            foreach (var node in MeshBeta.NodesDictionary.Values)
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
            foreach (var node in MeshBeta.NodesDictionary.Values)
            {
                diffusionCoefficients.Add(node, Metrics[node].contravariantTensor);
                convectionCoefficients.Add(node, new double[] {0, 0});
                dependentReactionCoefficients.Add(node, new double[] {0d, 0d} );
                independentReactionCoefficients.Add(node, new double[] {0d, 0d} );
            }
            return new LocallyAnisotropicConvectionDiffusionReactionEquationProperties(diffusionCoefficients, convectionCoefficients,
                                                                                  dependentReactionCoefficients, independentReactionCoefficients);
        }


    }
}
