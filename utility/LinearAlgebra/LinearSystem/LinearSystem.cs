using utility;
namespace prizaLinearAlgebra
{
    public class LinearSystem : ILinearSystem
    {
        public IMatrix Matrix { get; }
        public double[] Vector { get; }
        public double[] Solution { get; set; }
        
        //public List<double[]> PreviousSolutions { get; internal set; } = new List<double[]>();

        public LinearSystem(IMatrix matrix, double[] vector)
        {
            this.Matrix = matrix;
            this.Vector = vector;
            this.Solution = new double[vector.Length];
        }

        public void Solve()
        {
            //var solver = new Solver();
            //Solution = solver.Solve(this.Matrix, this.Vector);
        }


    }

}