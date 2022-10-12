using Discretization;
using prizaLinearAlgebra;
using utility;
namespace Meshing
{
    public class MetricsCalculator
    {
        /// <summary>
        /// A 2d Node Array containing all the nodes of the domain.
        /// </summary>
        /// <value></value>
        public Dictionary<int, Node> Nodes { get;}

        private int nnx { get; }

        private int nny { get; }

        /// <summary>
        /// A dictionary with the metrics of each mesh node as value and the node as key.
        /// </summary>
        public Dictionary<Node, NodeMetrics> MeshMetrics {get; internal set;} = new Dictionary<Node, NodeMetrics>();

        public MetricsCalculator(Dictionary<int, Node> Nodes, int nnx, int nny)
        {
            this.Nodes = Nodes;
            this.nnx = nnx;
            this.nny = nny;
            CalculateMeshMetrics();
        }

        private void CalculateMeshMetrics()
        {
            foreach (var node in Nodes.Values)
            {
                var boundaryId = node.Id.Boundary;
                var metrics = new NodeMetrics();
                switch (boundaryId)
                {
                    //Bottom Left Corner
                    case var BottomLeftCorner when node.Id.Global == 0:
                        metrics = CalculateBottomLeftCornerNodeMetrics(node);
                        break;

                    //Bottom Boundary Internal
                    case var positionInBottomBoundary when boundaryId > 0 && boundaryId < nnx - 1:
                        metrics = CalculateBottomBoundaryInternalNodeMetrics(node);
                        break;

                    //Bottom Right Corner
                    case var bottomRightCorner when boundaryId == nnx - 1:
                        metrics = CalculateBottomRightCornerNodeMetrics(node);
                        break;
                    
                    //Right Boundary Internal
                    case var positionInLeftBoundary when boundaryId > nnx - 1 && boundaryId < (nnx - 1) + (nny - 1):
                        metrics = CalculateRightBoundaryInternalNodeMetrics(node);
                        break;
                    
                    //Top Right Corner
                    case var topRightCorner when boundaryId == (nnx - 1) + (nny - 1):
                        metrics = CalculateTopRightCornerNodeMetrics(node);
                        break;

                    //Top Boundary Internal
                    case var positionInTopBoundary when boundaryId > (nnx - 1) + (nny - 1) && boundaryId < 2 * (nnx - 1) + (nny - 1):
                        metrics = CalculateTopBoundaryInternalNodeMetrics(node);
                        break;
                    
                    //Top Left Corner
                    case var topLeftCorner when boundaryId == 2 * (nnx - 1) + (nny - 1):
                        metrics = CalculateTopLeftCornerNodeMetrics(node);
                        break;

                    //Left Boundary Internal
                    case var positionInRightBoundary when (boundaryId > 2 * (nnx - 1) + (nny - 1)) && (boundaryId < 2 * (nnx - 1) + 2 * (nny - 1)):
                        metrics = CalculateLeftBoundaryInternalNodeMetrics(node);
                        break;
                    //Internal
                    case var internalNode when boundaryId == -1:
                        metrics = CalculateInternalNodeMetrics(node);
                        break;
                    
                    default:
                        throw new Exception("The node is not in the domain.");
                        
                }
                
                    metrics.covariantTensor[0, 0] = Calculators.vectorDotProduct(metrics.covariants1, metrics.covariants1);
                    metrics.covariantTensor[0, 1] = Calculators.vectorDotProduct(metrics.covariants1, metrics.covariants2);
                    metrics.covariantTensor[1, 0] = Calculators.vectorDotProduct(metrics.covariants2, metrics.covariants1);
                    metrics.covariantTensor[1, 1] = Calculators.vectorDotProduct(metrics.covariants2, metrics.covariants2);
        
                    metrics.contravariantTensor[0, 0] = Calculators.vectorDotProduct(metrics.contravariants1, metrics.contravariants1);
                    metrics.contravariantTensor[1, 0] = Calculators.vectorDotProduct(metrics.contravariants1, metrics.contravariants2);
                    metrics.contravariantTensor[0, 1] = Calculators.vectorDotProduct(metrics.contravariants2, metrics.contravariants1);
                    metrics.contravariantTensor[1, 1] = Calculators.vectorDotProduct(metrics.contravariants2, metrics.contravariants2);
        
                    metrics.Jacobian = metrics.covariants1[0] * metrics.covariants2[1] - metrics.covariants1[1] * metrics.covariants2[0];

                    MeshMetrics.Add(node, metrics);
            }
        }
        

        private NodeMetrics CalculateBottomLeftCornerNodeMetrics(Node node)
        {
            var E  = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["E"];
            var EE = new NeighbourFinder(E, Nodes, nnx, nny).NodeHood["E"];
            var N  = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["N"];
            var NN = new NeighbourFinder(N, Nodes, nnx, nny).NodeHood["N"];
            var metrics = new NodeMetrics();
            //[x,ksi, y,ksi]
            metrics.covariants1[0] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.TemplateX].Value,
                                                                           E.Coordinates[CoordinateType.TemplateX].Value,
                                                                           EE.Coordinates[CoordinateType.TemplateX].Value, 1);
            
            metrics.covariants1[1] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.TemplateY].Value,
                                                                            N.Coordinates[CoordinateType.TemplateY].Value,
                                                                            NN.Coordinates[CoordinateType.TemplateY].Value, 1);

            //[x,ita, y,ita]
            metrics.covariants2[0] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.TemplateX].Value,
                                                                            E.Coordinates[CoordinateType.TemplateX].Value,
                                                                            EE.Coordinates[CoordinateType.TemplateX].Value, 1);
            metrics.covariants2[1] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.TemplateY].Value,
                                                                            N.Coordinates[CoordinateType.TemplateY].Value,
                                                                            NN.Coordinates[CoordinateType.TemplateY].Value, 1);
            //[ksi,x, ita,x]
            metrics.contravariants1[0] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                                E.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                                EE.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants1[1] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.ParametricIta].Value,
                                                                                N.Coordinates[CoordinateType.ParametricIta].Value,
                                                                                NN.Coordinates[CoordinateType.ParametricIta].Value, 1);

            //[ksi,y, ita,y]
            metrics.contravariants2[0] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                                E.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                                EE.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants2[1] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.ParametricIta].Value,
                                                                                N.Coordinates[CoordinateType.ParametricIta].Value,
                                                                                NN.Coordinates[CoordinateType.ParametricIta].Value, 1);              

            return metrics;
        }

        private NodeMetrics CalculateBottomRightCornerNodeMetrics(Node node)
        {
            var W = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["W"];
            var WW = new NeighbourFinder(W, Nodes, nnx, nny).NodeHood["W"];
            var N = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["N"];
            var NN = new NeighbourFinder(N, Nodes, nnx, nny).NodeHood["N"];
            var metrics = new NodeMetrics();

            //[x,ksi, y,ksi]
            metrics.covariants1[0] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.TemplateX].Value,
                                                                           W.Coordinates[CoordinateType.TemplateX].Value,
                                                                           WW.Coordinates[CoordinateType.TemplateX].Value, 1);

            metrics.covariants1[1] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.TemplateY].Value,
                                                                            N.Coordinates[CoordinateType.TemplateY].Value,
                                                                            NN.Coordinates[CoordinateType.TemplateY].Value, 1);
            //[x,ita, y,ita]
            metrics.covariants2[0] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.TemplateX].Value,
                                                                            W.Coordinates[CoordinateType.TemplateX].Value,
                                                                            WW.Coordinates[CoordinateType.TemplateX].Value, 1);
            metrics.covariants2[1] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.TemplateY].Value,
                                                                           N.Coordinates[CoordinateType.TemplateY].Value,
                                                                           NN.Coordinates[CoordinateType.TemplateY].Value, 1);
            //[ksi,x, ita,x]
            metrics.contravariants1[0] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                                W.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                                WW.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants1[1] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.ParametricIta].Value,
                                                                                N.Coordinates[CoordinateType.ParametricIta].Value,
                                                                                NN.Coordinates[CoordinateType.ParametricIta].Value, 1);
            //[ksi,y, ita,y]   
            metrics.contravariants2[0] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                                W.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                                WW.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants2[1] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.ParametricIta].Value,
                                                                                N.Coordinates[CoordinateType.ParametricIta].Value,
                                                                                NN.Coordinates[CoordinateType.ParametricIta].Value, 1);
            return metrics;
        }

        private NodeMetrics CalculateTopLeftCornerNodeMetrics(Node node) 
        {
            var E = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["E"];
            var EE = new NeighbourFinder(E, Nodes, nnx, nny).NodeHood["E"];
            var S = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["S"];
            var SS = new NeighbourFinder(S, Nodes, nnx, nny).NodeHood["S"];
            var metrics = new NodeMetrics();
            //[x,ksi, y,ksi]
            metrics.covariants1[0] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.TemplateX].Value,
                                                                           E.Coordinates[CoordinateType.TemplateX].Value,
                                                                           EE.Coordinates[CoordinateType.TemplateX].Value, 1);
            metrics.covariants1[1] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.TemplateY].Value,
                                                                            S.Coordinates[CoordinateType.TemplateY].Value,
                                                                            SS.Coordinates[CoordinateType.TemplateY].Value, 1); 
            //[x,ita, y,ita]
            metrics.covariants2[0] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.TemplateX].Value,
                                                                            E.Coordinates[CoordinateType.TemplateX].Value,
                                                                            EE.Coordinates[CoordinateType.TemplateX].Value, 1);
            metrics.covariants2[1] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.TemplateY].Value,
                                                                            S.Coordinates[CoordinateType.TemplateY].Value,
                                                                            SS.Coordinates[CoordinateType.TemplateY].Value, 1);     
            //[ksi,x, ita,x]
            metrics.contravariants1[0] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                                E.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                                EE.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants1[1] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.ParametricIta].Value,
                                                                                S.Coordinates[CoordinateType.ParametricIta].Value,
                                                                                SS.Coordinates[CoordinateType.ParametricIta].Value, 1);
            //[ksi,y, ita,y]
            metrics.contravariants2[0] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                                E.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                                EE.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants2[1] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.ParametricIta].Value,
                                                                                S.Coordinates[CoordinateType.ParametricIta].Value,
                                                                                SS.Coordinates[CoordinateType.ParametricIta].Value, 1);
            return metrics;
        }

        private NodeMetrics CalculateTopRightCornerNodeMetrics(Node node)
        {
            var W = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["W"];
            var WW = new NeighbourFinder(W, Nodes, nnx, nny).NodeHood["W"];
            var S = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["S"];
            var SS = new NeighbourFinder(S, Nodes, nnx, nny).NodeHood["S"];
            var metrics = new NodeMetrics();
            //[x,ksi, y,ksi]
            metrics.covariants1[0] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.TemplateX].Value,
                                                                           W.Coordinates[CoordinateType.TemplateX].Value,
                                                                           WW.Coordinates[CoordinateType.TemplateX].Value, 1);
            metrics.covariants1[1] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.TemplateY].Value,
                                                                            S.Coordinates[CoordinateType.TemplateY].Value,
                                                                            SS.Coordinates[CoordinateType.TemplateY].Value, 1);
            //[x,ita, y,ita]
            metrics.covariants2[0] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.TemplateX].Value,
                                                                            W.Coordinates[CoordinateType.TemplateX].Value,
                                                                            WW.Coordinates[CoordinateType.TemplateX].Value, 1);
            metrics.covariants2[1] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.TemplateY].Value,
                                                                            S.Coordinates[CoordinateType.TemplateY].Value,
                                                                            SS.Coordinates[CoordinateType.TemplateY].Value, 1);
            //[ksi,x, ita,x]
            metrics.contravariants1[0] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                                W.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                                WW.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants1[1] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.ParametricIta].Value,
                                                                                S.Coordinates[CoordinateType.ParametricIta].Value,
                                                                                SS.Coordinates[CoordinateType.ParametricIta].Value, 1);
            //[ksi,y, ita,y]
            metrics.contravariants2[0] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                                W.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                                WW.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants2[1] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.ParametricIta].Value,
                                                                                S.Coordinates[CoordinateType.ParametricIta].Value,
                                                                                SS.Coordinates[CoordinateType.ParametricIta].Value, 1);
            return metrics;
        }

        private NodeMetrics CalculateBottomBoundaryInternalNodeMetrics(Node node)
        {
            var E = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["E"];
            var W = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["W"];
            var N = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["N"];
            var NN = new NeighbourFinder(N, Nodes, nnx, nny).NodeHood["N"];
            var metrics = new NodeMetrics();
                
            //[x,ksi, y,ksi]
            metrics.covariants1[0] = FirstDerivative.CentralDifference(W.Coordinates[CoordinateType.TemplateX].Value,
                                                                       E.Coordinates[CoordinateType.TemplateX].Value, 1);
            metrics.covariants1[1] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.TemplateY].Value,
                                                                           N.Coordinates[CoordinateType.TemplateY].Value,
                                                                          NN.Coordinates[CoordinateType.TemplateY].Value, 1);
            //[x,ita, y,ita]
            metrics.covariants2[0] = FirstDerivative.CentralDifference(W.Coordinates[CoordinateType.TemplateX].Value,
                                                                       E.Coordinates[CoordinateType.TemplateX].Value, 1);
            metrics.covariants2[1] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.TemplateY].Value,
                                                                           N.Coordinates[CoordinateType.TemplateY].Value,
                                                                          NN.Coordinates[CoordinateType.TemplateY].Value, 1);
            //[ksi,x, ita,x]
            metrics.contravariants1[0] = FirstDerivative.CentralDifference(W.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                           E.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants1[1] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.ParametricIta].Value,
                                                                               N.Coordinates[CoordinateType.ParametricIta].Value,
                                                                              NN.Coordinates[CoordinateType.ParametricIta].Value, 1);
            //[ksi,y, ita,y]
            metrics.contravariants2[0] = FirstDerivative.CentralDifference(W.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                           E.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants2[1] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.ParametricIta].Value,
                                                                               N.Coordinates[CoordinateType.ParametricIta].Value,
                                                                              NN.Coordinates[CoordinateType.ParametricIta].Value, 1);
            return metrics;
        }

        private NodeMetrics CalculateRightBoundaryInternalNodeMetrics(Node node)
        {
            var N = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["N"];
            var S = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["S"];
            var W = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["W"];
            var WW = new NeighbourFinder(W, Nodes, nnx, nny).NodeHood["W"];
            var metrics = new NodeMetrics();

            //[x,ksi, y,ksi]
            metrics.covariants1[0] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.TemplateX].Value,
                                                                            W.Coordinates[CoordinateType.TemplateX].Value,
                                                                           WW.Coordinates[CoordinateType.TemplateX].Value, 1);
            metrics.covariants1[1] = FirstDerivative.CentralDifference(S.Coordinates[CoordinateType.TemplateY].Value,
                                                                       N.Coordinates[CoordinateType.TemplateY].Value, 1);
            //[x,ita, y,ita]
            metrics.covariants2[0] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.TemplateX].Value,
                                                                            W.Coordinates[CoordinateType.TemplateX].Value,
                                                                           WW.Coordinates[CoordinateType.TemplateX].Value, 1);
            metrics.covariants2[1] = FirstDerivative.CentralDifference(S.Coordinates[CoordinateType.TemplateY].Value,
                                                                       N.Coordinates[CoordinateType.TemplateY].Value, 1);                
            //[ksi,x, ita,x]    
            metrics.contravariants1[0] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                            W.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                           WW.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants1[1] = FirstDerivative.CentralDifference(S.Coordinates[CoordinateType.ParametricIta].Value,
                                                                          N.Coordinates[CoordinateType.ParametricIta].Value, 1);
            //[ksi,y, ita,y]
            metrics.contravariants2[0] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                            W.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                           WW.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants2[1] = FirstDerivative.CentralDifference(S.Coordinates[CoordinateType.ParametricIta].Value,
                                                                            N.Coordinates[CoordinateType.ParametricIta].Value, 1);               
            return metrics;
        }

        private NodeMetrics CalculateTopBoundaryInternalNodeMetrics(Node node)
        {
            var S = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["S"];
            var SS = new NeighbourFinder(S, Nodes, nnx, nny).NodeHood["S"];
            var E = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["E"];
            var W = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["W"];
            var metrics = new NodeMetrics();
            
            //[x,ksi, y,ksi]
            metrics.covariants1[0] = FirstDerivative.CentralDifference(W.Coordinates[CoordinateType.TemplateX].Value,
                                                                       E.Coordinates[CoordinateType.TemplateX].Value, 1);
            metrics.covariants1[1] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.TemplateY].Value,
                                                                            S.Coordinates[CoordinateType.TemplateY].Value,
                                                                           SS.Coordinates[CoordinateType.TemplateY].Value, 1);
            //[x,ita, y,ita]
            metrics.covariants2[0] = FirstDerivative.CentralDifference(W.Coordinates[CoordinateType.TemplateX].Value,
                                                                       E.Coordinates[CoordinateType.TemplateX].Value, 1);
            metrics.covariants2[1] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.TemplateY].Value,
                                                                            S.Coordinates[CoordinateType.TemplateY].Value,
                                                                           SS.Coordinates[CoordinateType.TemplateY].Value, 1);
            //[ksi,x, ita,x]
            metrics.contravariants1[0] = FirstDerivative.CentralDifference(W.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                           E.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants1[1] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.ParametricIta].Value,
                                                                            S.Coordinates[CoordinateType.ParametricIta].Value,
                                                                           SS.Coordinates[CoordinateType.ParametricIta].Value, 1);
            //[ksi,y, ita,y]
            metrics.contravariants2[0] = FirstDerivative.CentralDifference(W.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                           E.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants2[1] = FirstDerivative.BackwardDifference2(node.Coordinates[CoordinateType.ParametricIta].Value,
                                                                            S.Coordinates[CoordinateType.ParametricIta].Value,
                                                                           SS.Coordinates[CoordinateType.ParametricIta].Value, 1);
            return metrics;
        }

        private NodeMetrics CalculateLeftBoundaryInternalNodeMetrics(Node node)
        {
            var N = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["N"];
            var S = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["S"];
            var E = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["E"];
            var EE = new NeighbourFinder(E, Nodes, nnx, nny).NodeHood["E"];
            var metrics = new NodeMetrics();

            //[x,ksi, y,ksi]
            metrics.covariants1[0] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.TemplateX].Value,
                                                                        E.Coordinates[CoordinateType.TemplateX].Value,
                                                                       EE.Coordinates[CoordinateType.TemplateX].Value, 1);
            metrics.covariants1[1] = FirstDerivative.CentralDifference(S.Coordinates[CoordinateType.TemplateY].Value,
                                                                       N.Coordinates[CoordinateType.TemplateY].Value, 1);
            //[x,ita, y,ita]
            metrics.covariants2[0] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.TemplateX].Value,
                                                                        E.Coordinates[CoordinateType.TemplateX].Value,
                                                                       EE.Coordinates[CoordinateType.TemplateX].Value, 1);
            metrics.covariants2[1] = FirstDerivative.CentralDifference(S.Coordinates[CoordinateType.TemplateY].Value,
                                                                          N.Coordinates[CoordinateType.TemplateY].Value, 1);
            //[ksi,x, ita,x]
            metrics.contravariants1[0] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                        E.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                       EE.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants1[1] = FirstDerivative.CentralDifference(S.Coordinates[CoordinateType.ParametricIta].Value,
                                                                          N.Coordinates[CoordinateType.ParametricIta].Value, 1);                 
            //[ksi,y, ita,y]
            metrics.contravariants2[0] = FirstDerivative.ForwardDifference2(node.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                        E.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                       EE.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants2[1] = FirstDerivative.CentralDifference(S.Coordinates[CoordinateType.ParametricIta].Value,
                                                                          N.Coordinates[CoordinateType.ParametricIta].Value, 1);
            return metrics;
        }

        private NodeMetrics CalculateInternalNodeMetrics(Node node)
        {
            var N = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["N"];
            var S = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["S"];
            var E = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["E"];
            var W = new NeighbourFinder(node, Nodes, nnx, nny).NodeHood["W"];
            var metrics = new NodeMetrics();
            //[x,ksi, y,ksi]
            metrics.covariants1[0] = FirstDerivative.CentralDifference(W.Coordinates[CoordinateType.TemplateX].Value,
                                                                       E.Coordinates[CoordinateType.TemplateX].Value, 1);
            metrics.covariants1[1] = FirstDerivative.CentralDifference(S.Coordinates[CoordinateType.TemplateY].Value,
                                                                       N.Coordinates[CoordinateType.TemplateY].Value, 1);
            //[x,ita, y,ita]
            metrics.covariants2[0] = FirstDerivative.CentralDifference(W.Coordinates[CoordinateType.TemplateX].Value,
                                                                       E.Coordinates[CoordinateType.TemplateX].Value, 1);
            metrics.covariants2[1] = FirstDerivative.CentralDifference(S.Coordinates[CoordinateType.TemplateY].Value,
                                                                       N.Coordinates[CoordinateType.TemplateY].Value, 1);
            //[ksi,x, ita,x]
            metrics.contravariants1[0] = FirstDerivative.CentralDifference(W.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                           E.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants1[1] = FirstDerivative.CentralDifference(S.Coordinates[CoordinateType.ParametricIta].Value,
                                                                           N.Coordinates[CoordinateType.ParametricIta].Value, 1);
            //[ksi,y, ita,y]
            metrics.contravariants2[0] = FirstDerivative.CentralDifference(W.Coordinates[CoordinateType.ParametricKsi].Value,
                                                                           E.Coordinates[CoordinateType.ParametricKsi].Value, 1);
            metrics.contravariants2[1] = FirstDerivative.CentralDifference(S.Coordinates[CoordinateType.ParametricIta].Value,
                                                                           N.Coordinates[CoordinateType.ParametricIta].Value, 1);
            return metrics;

        }

    }
}  