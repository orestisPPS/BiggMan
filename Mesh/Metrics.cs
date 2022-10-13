using prizaLinearAlgebra;
using utility;
using Discretization;
namespace MeshGeneration
{
    public class NodeMetrics
    {
        public double[] covariants1 {get; internal set;}
        public double[] covariants2 {get; internal set;}
        public double[] contravariants1 {get; internal set;}
        public double[] contravariants2 {get; internal set;}
        public double[,] covariantTensor {get; internal set;}
        public double[,] contravariantTensor {get; internal set;}
        public double Jacobian {get; internal set;}

        public NodeMetrics()
        {
            covariants1 = new double[2];
            covariants2 = new double[2];
            contravariants1 = new double[2];
            contravariants2 = new double[2];
            covariantTensor = new double[2, 2];
            contravariantTensor = new double[2, 2];
        } 
    }
}